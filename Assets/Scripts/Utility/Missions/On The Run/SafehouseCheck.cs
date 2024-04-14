using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafehouseCheck : MonoBehaviour
{
    public OnTheRun OTR;
    public bool inSafehouse = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && OTR.Escaped)
        {
            inSafehouse = true;
            OTR.objective.text = "Place the evidence on the wall in the evidence room.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!inSafehouse && OTR.Escaped)
        {
            inSafehouse = false;
            OTR.objective.text = "Go back inside the safehouse";
        }
    }
}
