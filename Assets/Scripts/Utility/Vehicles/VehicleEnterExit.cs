using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VehicleEnterExit : MonoBehaviour
{
    [Header("Vehicle References")]
    public GameObject vehicleCam;
    public GameObject playerCam;
    public GameObject TPCam;
    public GameObject player;
    public GameObject vehicle;
    public Transform carSeat;
    public bool canEnter = false;
    public bool canExit = false;
    public bool inVehicle = false;
    public Collider vehicleCol;
    public PlayerMovementSM playsm;
    public Animator carDoorAnim;
    public RaycastMaster rMaster;

    private void Start()
    {
        vehicle.GetComponent<CarController>().enabled = false;
        vehicleCam.SetActive(false);
    }

    private void Update()
    {
        EnteringVehicle();
        ExitingVehicle();
    }

    public void EnterVehicle()
    {
        if (canEnter == true)
        {
            StartCoroutine(EnteringVehicle());
        }
    }

    IEnumerator EnteringVehicle()
    {
        rMaster.interactKey.SetActive(false);
        vehicleCol.GetComponent<Collider>().enabled = false;
        vehicleCam.SetActive(true);
        playerCam.SetActive(false);
        TPCam.SetActive(false);
        playsm.anim.SetBool("enteringCar", true);
        carDoorAnim.Play("CarDoor");
        yield return new WaitForSeconds(5);
        playsm.anim.SetBool("enteringCar", false);
        player.transform.parent = carSeat.transform;
        player.transform.rotation = carSeat.transform.rotation;
        player.transform.position = carSeat.transform.position;
        player.GetComponent<PlayerMovementSM>().enabled = false;
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<ThrowGrenade>().enabled = false;
        vehicle.GetComponent<CarController>().speedometer.SetActive(true);
        vehicle.GetComponent<CarController>().enabled = true;
        inVehicle = true;
        playsm.inVehicle = true;
        canEnter = false;
        canExit = true;
    }

    public void ExitVehicle()
    {
        if (canExit == true)
        {
            rMaster.interactKey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && inVehicle)
            {
                StartCoroutine(ExitingVehicle());
            }
        }
    }

    IEnumerator ExitingVehicle()
    {
        vehicleCol.GetComponent<Collider>().enabled = true;
        vehicleCam.SetActive(false);
        playerCam.SetActive(true);
        TPCam.SetActive(true);
        vehicle.GetComponent<CarController>().speedometer.SetActive(false);
        playsm.anim.SetBool("exitingCar", true);
        carDoorAnim.Play("CarDoor");
        yield return new WaitForSeconds(5);
        playsm.anim.SetBool("exitingCar", false);
        player.transform.parent = null;
        player.GetComponent<PlayerMovementSM>().enabled = true;
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<ThrowGrenade>().enabled = true;
        vehicle.GetComponent<CarController>().enabled = false;
        inVehicle = false;
        playsm.inVehicle = false;
        canEnter = true;
        canExit = false;
        rMaster.interactKey.SetActive(false);
    }
}

