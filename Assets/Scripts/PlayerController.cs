using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSpeed = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring Settings:")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;
    

	// Use this for initialization
	void Start () {

        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);

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

        float camerRotX = xRot * lookSpeed;

        motor.RotateCamera(camerRotX);

        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {

            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);

        } else
        {

            SetJointSettings(jointSpring);

        }

        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {

        joint.yDrive = new JointDrive { positionSpring = _jointSpring, maximumForce = jointMaxForce };

    }
}
