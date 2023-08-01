using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListerScript : MonoBehaviour
{
    public DestructibleScript ds;
    
    public enum Type
    {
        bottom,
        mid,
        up
    }

    public Type type;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dest"))
        {
            if (type == Type.bottom)
            {
                ds.lowPieces.Add(other.gameObject);
            }

            if (type == Type.mid)
            {
                ds.midPieces.Add(other.gameObject);
            }

            if (type == Type.up)
            {
                ds.upPieces.Add(other.gameObject);
            }
        }
    }
}
