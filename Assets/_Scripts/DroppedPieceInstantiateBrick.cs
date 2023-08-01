using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DroppedPieceInstantiateBrick : MonoBehaviour
{
    public GameObject brick;

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: Instantiate olucak randPos1 veya randPos2ye force alıcak

        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-GameManager.Instance.player.transform.forward * 500);
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.up * 200);
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            for (int i = 0; i < 5; i++)
            {
                var a = Instantiate(brick, gameObject.transform.position, Quaternion.identity);
                
                a.GetComponent<Rigidbody>().AddForce(-GameManager.Instance.player.transform.forward * 600);
                a.GetComponent<Rigidbody>().AddForce(a.transform.up * 300);

                TimeManager.Instance.transform.DOMoveX(0, 1f).OnComplete(() =>
                {
                    a.GetComponent<CollectableBrickScript>().canGo = true;
                    GameManager.Instance.testBricks++;
                });
                
                Destroy(gameObject);
            }
        }
    }
}
