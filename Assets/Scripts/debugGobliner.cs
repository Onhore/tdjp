using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class debugGobliner : MonoBehaviour
{
    public LayerMask debugs;
    void OnCollisionEnter(Collision collision)
    {
        if(debugs.Contains(collision.gameObject.layer))
        {
            Destroy(collision.gameObject);
        }
    }
}
