using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Vector3 vel = Vector3.zero;
    private Vector3 rot = Vector3.zero;
    private float camRotX = 0f;
    private float currentCamRotation = 0f;
    private Vector3 thrusterForce = Vector3.zero;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(Vector3 _vel)
    {

       vel = _vel;

    }

    public void Rotate(Vector3 _rot)
    {

        rot = _rot;

    }

    public void RotateCamera(float _camRotX)
    {

        camRotX = _camRotX;

    }

    public void ApplyThruster(Vector3 _thrusterForce)
    {

        thrusterForce = _thrusterForce;

    }

    private void FixedUpdate()
    {

        PerformMovement();
        PerformRotation();

    }

    public void PerformMovement()
    {

        if(vel != Vector3.zero)
        {

            rb.MovePosition(transform.position + vel * Time.fixedDeltaTime);

        }

        if (thrusterForce != Vector3.zero)
        {

            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);

        }

    }

    public void PerformRotation()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(rot));

        if (cam != null)
        {

            // cam.transform.Rotate(-camRot);
            currentCamRotation -= camRotX;
            currentCamRotation = Mathf.Clamp(currentCamRotation, -cameraRotationLimit, cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCamRotation, 0f, 0f);
        }

    }


}
