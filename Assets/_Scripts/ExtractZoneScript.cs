using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ExtractZoneScript : MonoBehaviour
{
    public int brickToBuild;
    public int done;

    public GameObject brick;

    public DOTweenAnimation anim;

    public int forFactor;

    public DestructibleScript ds1,ds2,ds3,ds4;
    public ExtractObjects eo1, eo2, eo3, eo4;

    public bool stop;
    [Space(20)] public bool newMode;

    [Space(20)] public float stackCounter;
    public float staticStacker;

    public enum Type
    {
        None,One,Two,Three,Four
    }

    public Type type;

    public void WhichType()
    {
        if (type == Type.One)
        {
            if (!newMode)
                OpenBrick(ds1, null);
            
            if(newMode)
                OpenBrick(null,eo1);
        }
        
        if (type == Type.Two)
        {
            if (!newMode)
                OpenBrick(ds2, null);
            
            if(newMode)
                OpenBrick(null,eo2);
        }
        
        if (type == Type.Three)
        {
            if (!newMode)
                OpenBrick(ds3, null);
            
            if(newMode)
                OpenBrick(null,eo3);
        }
        
        if (type == Type.Four)
        {
            if (!newMode)
                OpenBrick(ds4, null);
            
            if(newMode)
                OpenBrick(null,eo4);
        }
    }
    
    public void OpenBrick(DestructibleScript ds,ExtractObjects eo)
    {
        if (!stop)
        {
            if (!newMode)
            {
                done++;
                if (ds.lowPieces.Count != 0)
                {
                    var rand = Random.Range(0, ds1.lowPieces.Count);
                    ds.lowPieces[rand].SetActive(true);
                    ds1.lowPieces.Remove(ds1.lowPieces[rand]);
                }

                if (ds.lowPieces.Count == 0 && ds1.midPieces.Count != 0)
                {
                    var rand = Random.Range(0, ds1.midPieces.Count);
                    ds.midPieces[rand].SetActive(true);
                    ds.midPieces.Remove(ds1.midPieces[rand]);
                }

                if (ds.lowPieces.Count == 0 && ds.midPieces.Count == 0 && ds.upPieces.Count != 0)
                {
                    var rand = Random.Range(0, ds.upPieces.Count);
                    ds.upPieces[rand].SetActive(true);
                    ds.upPieces.Remove(ds.upPieces[rand]);
                }
            }

            if (newMode)
            {
                eo.pieces[0].SetActive(true);
                eo.pieces.Remove(eo.pieces[0]);
            }
        }
    }

    private void Update()
    {
        if (done >= brickToBuild)
        {
            stop = true;
            if (anim != null)
                anim.DORewind();
        }
    }

    public void TriggeredMethod()
    {
        if (!stop)
        {
            // if (GameManager.Instance._listNumber > 30)
            // {
            //     forFactor = 10;
            // }
            //     
            // if (GameManager.Instance._listNumber < 30)
            // {
            //     forFactor = 1;
            // }
            forFactor = 1;

            for (int i = 0; i < forFactor; i++)
            {
                if (anim != null)
                    anim.DOPlay();

                print("Dizmeye başla");

                var a = GameManager.Instance.storageBricks[GameManager.Instance.storageBricks.Count - 1];

                var b = Instantiate(brick, a.transform.position, Quaternion.identity);

                if (gameObject.transform.parent != null)
                {
                    WhichType();
                    a.SetActive(false);
                    b.transform.DOJump(gameObject.transform.parent.position, 5, 1, 0.03f).OnComplete(() =>
                    {
                        Destroy(b);

                        GameManager.Instance.listNumber--;
                    }).OnStart(() => { });

                    GameManager.Instance.storageBricks.Remove(a);
                }
                else
                {
                    WhichType();
                    a.SetActive(false);
                    b.transform.DOJump(gameObject.transform.position, 5, 1, 0.1f).OnComplete(() =>
                    {
                        Destroy(b);

                        GameManager.Instance.listNumber--;
                    });
                    GameManager.Instance.storageBricks.Remove(a);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggeredMethod();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (anim != null)
                anim.DORewind();
        }
    }
}
