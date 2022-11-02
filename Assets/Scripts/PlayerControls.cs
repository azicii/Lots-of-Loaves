using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] InputAction movement;
    [SerializeField] GameObject moose;
    [SerializeField] float controlSpeed = 10f;

    [SerializeField] float positionYawFactor = 2f;

    float xThrow;
    float zThrow;

    //hello
    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }


    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    public void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        zThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float zOffset = zThrow * Time.deltaTime * controlSpeed;

        float newXPos = transform.position.x + xOffset;
        float newZPos = transform.position.z + zOffset;
        float newYPos = transform.position.y;

        transform.position = new Vector3(newXPos, newYPos, newZPos);
    }

    private void ProcessRotation()
    {

        float pitch = 0f; ;
        float yaw = xThrow * positionYawFactor;
        float roll = 1f;

        moose.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
        

    }

}
