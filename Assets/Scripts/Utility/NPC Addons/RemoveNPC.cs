using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNPC : MonoBehaviour
{
    public void DestroyNPC()
    {
        Destroy(gameObject);
    }
}