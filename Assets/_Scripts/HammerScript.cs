using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HammerScript : MonoBehaviour
{
    public PlayerController pc;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destructible"))
        {
            other.gameObject.GetComponent<DestructibleScript>().DestroyTest(pc.damage);
        }
    }
}
