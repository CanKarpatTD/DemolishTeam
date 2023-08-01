using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTriggerScript : MonoBehaviour
{
    public ExtractZoneScript ezs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ezs.TriggeredMethod();
        }
    }
}
