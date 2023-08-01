using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public float speed;
    public float rotSpeed;
    public float moveForce;
    public VariableJoystick _joystick;

    public Transform asd;
    
    private void Update()
    {
        asd.transform.DOShakeScale(0.1f, 0.02f).OnComplete(() =>
        {
            asd.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        });
        
        speed = _rigidbody.velocity.magnitude;

        asd.localRotation = Quaternion.Slerp( asd.localRotation, Quaternion.Euler(0,-90,0), Time.deltaTime * 2f);
        
        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
            {
                if (speed > 5)
                {
                    asd.Rotate((Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime), 0, (Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime), Space.World);
                }
            }
        }

        var localEulerAngles = asd.localEulerAngles;
        localEulerAngles = new Vector3(GameManager.ClampAngle(localEulerAngles.x, -8, 8), localEulerAngles.y, GameManager.ClampAngle(localEulerAngles.z, -5, 5));
        asd.localEulerAngles = localEulerAngles;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Play)
        {
            _rigidbody.velocity = new Vector3(_joystick.Horizontal * moveForce, _rigidbody.velocity.y, _joystick.Vertical * moveForce);

            if (_rigidbody.velocity.magnitude > 0.5f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z)), Time.deltaTime * 6f);
                
                DOTween.Play("Moving");
            }
            else
            {
                DOTween.Pause("Moving");
            }
        }
    }
}
