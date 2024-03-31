
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class Gun : MonoBehaviour
{
    [Header("Gun Statistics")]
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, totalAmmo, bullet;
    public bool allowButtonHold, aiming;
    int bulletsLeft, bulletsShot;
    public int bulletSpeed;

    [Header("Gun allowed actions")]
    public int pressCount;
    float aimSens = 100f;
    float xRot = 0f;

    [Header("Booleans")]
    bool shooting, readyToShoot, reloading;
    public bool gunEquipped;
    bool pistol, rifle, shotgun;

    [Header("Gun References")]
    public GameObject fpsCam;
    public GameObject aimCam;
    public GameObject gun;
    public GameObject reticle;
    public Transform attackPoint;
    RaycastHit hit;
    public GameObject weapBullet;
    public LayerMask Enemy;
    public PlayerMovementSM playsm;
    public TextMeshProUGUI ammoText;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        aiming = false;
    }

    private void Update()
    {
        InputCheck();

        ammoText.SetText(bulletsLeft + " / " + totalAmmo);
    }

    private void InputCheck()
    {
        if (allowButtonHold && gunEquipped) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading || Input.GetKey(KeyCode.Mouse0) && bulletsLeft == 0 && !reloading)
        {
            // Reloads the gun, takes the totalAmmo away from how many shots were fired, and resets the bullet and bulletsShot count to zero.
            ReloadGun();
            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("reloading");
            totalAmmo -= bulletsShot;
            bulletsShot = 0;
            bullet = 0;
        }

        if (readyToShoot && gunEquipped && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            AudioManager.manager.Play("shootGun");
            ShootGun();
        }

        if (Input.GetMouseButton(1) && !aiming && gunEquipped)
        {
            // Aims the gun.
            Aiming();
            aiming = true;
        }

        if (Input.GetKey(KeyCode.Mouse0) && shooting && aiming && gunEquipped)
        {
            playsm.anim.SetBool("shoot", true);
        } 

        if (!Input.GetMouseButton(1) && aiming && gunEquipped)
        {
            aiming = false;
            fpsCam.gameObject.SetActive(true);
            aimCam.gameObject.SetActive(false);
            playsm.anim.SetBool("aiming", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gunEquipped && pressCount == 1)
        {
            gun.SetActive(false);
            reticle.SetActive(false);
            gunEquipped = false;
            pressCount = 0;
            ammoText.gameObject.SetActive(false);
        }

        if (aiming && gunEquipped)
        {
            float mouseX = Input.GetAxis("Mouse X") * aimSens * Time.deltaTime;
            playsm.transform.Rotate(Vector3.up * mouseX);

            float mouseY = Input.GetAxis("Mouse Y") * aimSens * Time.deltaTime;
            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -90, 90);

            aimCam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        }
    }

    private void ShootGun()
    {
        readyToShoot = false;

        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Direction of spread
        Vector3 direction = aimCam.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(aimCam.transform.position, direction, out hit, range, Enemy) || (Physics.Raycast(aimCam.transform.position, direction, out hit, range)))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
                hit.collider.GetComponent<EnemyHealth>().LoseHealth(damage);
        }
        GameObject newBullet =  Instantiate(weapBullet, attackPoint.position,  Quaternion.identity);
        Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

        Vector3 bulletVel = direction.normalized * bulletSpeed;
        bulletRB.velocity = bulletVel;

        bulletsLeft--;

        bulletsShot = bullet;
        bulletsShot++;
        bullet = bulletsShot;


        Invoke("ResetShot", timeBetweenShooting);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void ReloadGun()
    {
        playsm.anim.SetBool("reloading", true);
        reloading = true;
        readyToShoot = false;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        playsm.anim.SetBool("reloading", false);
        AudioManager.manager.Stop("reloading");
        reloading = false;
        readyToShoot = true;
    }

    private void Aiming()
    {
        fpsCam.gameObject.SetActive(false);
        aimCam.gameObject.SetActive(true);
        playsm.anim.SetBool("aiming", true);
    }
}
