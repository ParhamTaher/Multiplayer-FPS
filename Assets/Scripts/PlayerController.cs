﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSpeed = 3f;
    private PlayerMotor motor;

	// Use this for initialization
	void Start () {

        motor = GetComponent<PlayerMotor>();

	}
	
	// Update is called once per frame
	void Update () {

        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 vel = (movHorizontal + movVertical).normalized * speed;

        motor.Move(vel);

        // Turning left and right
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rot = new Vector3(0f, yRot, 0f) * lookSpeed;

        motor.Rotate(rot);

        // Camera Rotation up and down
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 camerRot = new Vector3(xRot, 0f, 0f) * lookSpeed;

        motor.RotateCamera(camerRot);
    }
}
