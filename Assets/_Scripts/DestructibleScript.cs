using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestructibleScript : MonoBehaviour
{
    public float hitCount;
    public float staticHit;

    [Space(10)] public GameObject extractZone;
    public BoxCollider bc;

    [Space(20)] public List<GameObject> pieces = new List<GameObject>();
    
    [Space(20)]public List<GameObject> upPieces = new List<GameObject>();
    public List<GameObject> midPieces = new List<GameObject>();
    public List<GameObject> lowPieces = new List<GameObject>();

    public bool test;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //BrokePiece(10,10);
        }

        if (test)
        {
            print("Bitti la vurma");
            bc.size = new Vector3(0, 0, 0);
            
            
            
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces.Count > 0)
                {
                    pieces[i].GetComponent<Rigidbody>().isKinematic = false;
                    //pieces[i].GetComponent<Rigidbody>().AddForce(pieces[i].transform.forward * 400);
                    pieces[i].GetComponent<Rigidbody>().AddForce(pieces[i].transform.up * 400);
                    pieces[i].transform.DOScale(1, 0.1f);
                    
                }
                
            }
            test = false;
        }

        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i] == null)
            {
                pieces.Remove(pieces[i]);
            }
        }
    }

    public void DestroyTest(float hitter)
    {
        Camera.main.transform.DOShakePosition(0.1f, 0.1f, 1, 10);

        gameObject.transform.DOPunchScale(new Vector3(0.01f, 0.01f, 0.01f), 0.3f);
        
        hitCount += hitter;

        if (hitCount >= staticHit)
        {
            if (hitter >= pieces.Count)
            {
                test = true;
            }
            
            if(hitter <= staticHit)
            {
                if (!test)
                {
                    for (int i = 0; i < hitCount; i++)
                    {
                        if (upPieces.Count > 0)
                        {
                            upPieces[i].GetComponent<Rigidbody>().isKinematic = false;
                            upPieces[i].GetComponent<Rigidbody>().AddForce(upPieces[i].transform.up * 200);
                            upPieces.Remove(upPieces[i]);
                        }

                        if (upPieces.Count == 0 && midPieces.Count > 0)
                        {
                            midPieces[i].GetComponent<Rigidbody>().isKinematic = false;
                            midPieces[i].GetComponent<Rigidbody>().AddForce(midPieces[i].transform.up * 200);
                            midPieces.Remove(midPieces[i]);
                        }

                        if (upPieces.Count == 0 && midPieces.Count == 0 && lowPieces.Count > 0)
                        {
                            lowPieces[i].GetComponent<Rigidbody>().isKinematic = false;
                            lowPieces[i].GetComponent<Rigidbody>().AddForce(lowPieces[i].transform.up * 200);
                            lowPieces.Remove(lowPieces[i]);
                        }

                        if (upPieces.Count == 0 && midPieces.Count == 0 && lowPieces.Count == 0)
                        {
                            print("Bitti la vurma");
                            bc.size = new Vector3(0, 0, 0);
                        }
                    }
                    hitCount = 0;
                }
            }

            
        }

        // if (hitCount >= staticHit)
        // {
        //     if (upPieces.Count > 0)
        //     {
        //         upPieces[0].GetComponent<Rigidbody>().isKinematic = false;
        //         upPieces[0].GetComponent<Rigidbody>().AddForce(upPieces[0].transform.up * 200);
        //         upPieces.Remove(upPieces[0]);
        //     }
        //
        //     if (upPieces.Count == 0 && midPieces.Count > 0)
        //     {
        //         midPieces[0].GetComponent<Rigidbody>().isKinematic = false;
        //         midPieces[0].GetComponent<Rigidbody>().AddForce(midPieces[0].transform.up * 200);
        //         midPieces.Remove(midPieces[0]);
        //     }
        //
        //     if (upPieces.Count == 0 && midPieces.Count == 0 && lowPieces.Count > 0)
        //     {
        //         lowPieces[0].GetComponent<Rigidbody>().isKinematic = false;
        //         lowPieces[0].GetComponent<Rigidbody>().AddForce(lowPieces[0].transform.up * 200);
        //         lowPieces.Remove(lowPieces[0]);
        //     }
        //
        //     if (upPieces.Count == 0 && midPieces.Count == 0 && lowPieces.Count == 0)
        //     {
        //         print("Bitti la vurma");
        //         bc.size = new Vector3(0, 0, 0);
        //     }
        //
        //     hitCount = 0;
        // }
    }
}
