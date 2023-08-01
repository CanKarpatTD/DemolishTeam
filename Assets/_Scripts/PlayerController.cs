using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float freq,staticFreq;

    public float damage;
    
    public bool leftHit,rightHit;

    public ParticleSystem leftSlash, rightSlash;


    private void Start()
    {
        staticFreq = freq;
    }

    public void OpenRightHit()
    {
        rightHit = true;
        leftHit = false;
    }

    public void OpenLeftHit()
    {
        leftHit = true;
        rightHit = false;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            DOTween.Restart("LeftHitHammer");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DOTween.Restart("RightHitHammer");
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Destructible"))
        {
            //TODO: Vurmaya başla
            print("Vurmaya başla");

            freq -= 1 * Time.deltaTime;

            if (freq <= 0)
            {
                freq = staticFreq;

                if (leftHit)
                {
                    DOTween.Restart("LeftHitHammer");
                    leftSlash.Play();
                }

                if (rightHit)
                {
                    DOTween.Restart("RightHitHammer");
                    rightSlash.Play();
                }
            }
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Destructible"))
        {
            //TODO: Vurmayı durdur
            print("Vurmayı durdur");
            freq = 0.5f;
            DOTween.Pause("Hitting");
        }

        if (other.gameObject.CompareTag("ExtractZone"))
        {
            //TODO: İnşaatı durdur
            print("İnşaatı durdur");
            freq = 3;
        }
    }
}
