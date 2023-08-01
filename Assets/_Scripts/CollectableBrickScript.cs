using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectableBrickScript : MonoBehaviour
{
    public Rigidbody rb;

    public bool canGo,canMagnet,canSort;

    public float speed;
    private void Update()
    {
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));

        if (GameManager.Instance.gameState == GameManager.GameState.Play)
        {
            if (!canGo) return;
            var distance = Vector3.Distance(transform.position, GameManager.Instance.player.transform.position);

            if (distance <= 5 && canMagnet)
            {
                // force += Time.deltaTime * Random.Range(speed - 0.5f, speed + 12f);
                // transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.player.transform.GetChild(1).position, speed);

                transform.position = Vector3.Lerp(transform.position, GameManager.Instance.player.transform.position, speed);
            }


            if (GameManager.Instance.listNumber != GameManager.Instance.maxBrick)
            {
                if (distance <= 4 && !canSort)
                {
                    canSort = true;
                    canMagnet = false;

                    gameObject.transform.DOJump(GameManager.Instance.collectedBricks[GameManager.Instance.listNumber].transform.position,
                            1, 1, 0.3f).OnComplete(
                            () =>
                            {
                                Destroy(gameObject);
                                OpenBrick();
                            });

                    //OBJE PLAYER'DA
                }
            }
        }
    }

    private void OpenBrick()
    {
        GameManager.Instance.TakeBrick(1);
    }
}
