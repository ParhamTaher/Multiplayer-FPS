using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
    [SerializeField]
    private Camera cam;
    private Vector3 vel = Vector3.zero;
    private Vector3 rot = Vector3.zero;
    private Vector3 camRot = Vector3.zero;
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

        Debug.Log("here!");
        vel = _vel;

    }

    public void Rotate(Vector3 _rot)
    {

        rot = _rot;

    }

    public void RotateCamera(Vector3 _camRot)
    {

        camRot = _camRot;

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

    }

    public void PerformRotation()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(rot));

        if (cam != null)
        {

            cam.transform.Rotate(-camRot);

        }

    }


}
