using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class RaycastMaster : MonoBehaviour
{
    [Header("Raycast References")]
    public GameObject interactKey;
    public PlayerMovementSM playsm;

    // Update is called once per frame
    void Update()
    {
        EvidenceCollecting();
        DoorHandling();
        GEvidenceCollect();
        PlaceEvidenceOnBoard();
    }

    public void DoorHandling()
    {

    }

    public void EvidenceCollecting()
    {
        Ray evidenceRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
        float rayLength = 4;
        if (Physics.Raycast(evidenceRay, out RaycastHit evidenceHit, rayLength))
        {
            if (evidenceHit.collider.gameObject.tag == "Evidence")
            {
                CollectEvidence collectEvidence = evidenceHit.collider.gameObject.GetComponent<CollectEvidence>();  
                Debug.Log("HIT THE EVIDENCE!");
                interactKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && !collectEvidence.reading)
                {
                    collectEvidence.PickUp();
                    collectEvidence.reading = true;
                }
                else if (Input.GetKeyDown(KeyCode.E) && collectEvidence.reading)
                {
                    collectEvidence.CloseWindow();
                }
            }
        }
        else
        {
            interactKey.SetActive(false);
        }
    }

    public void GEvidenceCollect()
    {
        Ray gEvidenceRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
        float gRayLength = 4;
        if (Physics.Raycast(gEvidenceRay, out RaycastHit gEvidencehit, gRayLength))
        {
            if (gEvidencehit.collider.gameObject.tag == "GEvidence")
            {
                GangEvidenceCollect gECollect = gEvidencehit.collider.gameObject.GetComponent<GangEvidenceCollect>();
                Debug.Log("Evidence hit!");
                interactKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && !gECollect.isgReading)
                {
                    gECollect.GEPickup();
                    gECollect.isgReading = true;
                }
                else if (Input.GetKeyDown(KeyCode.E) && gECollect.isgReading)
                {
                    gECollect.GECloseWindow();
                }
            }
        }
    }

    public void PlaceEvidenceOnBoard()
    {
        Ray placeRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        float placeLength = 8;
        if (Physics.Raycast(placeRay, out RaycastHit placeHit, placeLength))
        {
            if (placeHit.collider.gameObject.tag == "EvidenceBoard")
            {
                EvidencePlace placeEvidence = placeHit.collider.gameObject.GetComponent<EvidencePlace>();
                Debug.Log("Board hit!");
                interactKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && !placeEvidence.EvidencePlaced)
                {
                    placeEvidence.StartCoroutine(placeEvidence.EvidenceSwap());
                    placeEvidence.EvidencePlaced = true;
                    interactKey.SetActive(false);
                }
            }
        }

    }
}
