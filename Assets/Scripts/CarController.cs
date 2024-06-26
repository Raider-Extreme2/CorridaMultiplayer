using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Netcode;

public class CarController : NetworkBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBreakForce;
    [SerializeField] private bool isBreaking;
    [SerializeField] private bool isFlipped;
    [SerializeField] private bool isFlipped2;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    [SerializeField] GameObject player;
    [SerializeField] Camera playerCamera;

    //vira pro lado certo o tempo todo mas eu n�o gosto
    //private void Update()
    //{
    //    if (player.transform.eulerAngles.z > 90 || player.transform.eulerAngles.z < -90)
    //    {
    //        isFlipped = true;
    //    }
    //}
    private void Start()
    {
        if (IsOwner)
        {
            playerCamera.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    private void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        FailSafe();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
        isFlipped = Input.GetKey(KeyCode.R);
        isFlipped2 = Input.GetKey(KeyCode.E);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider WheelCollider, Transform WheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        WheelCollider.GetWorldPose(out pos, out rot);
        WheelTransform.rotation = rot;
        WheelTransform.position = pos;
    }

    private void FailSafe() 
    {
        Vector3 correct = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y, 0f);
        Vector3 correct2 = new Vector3(0f, player.transform.eulerAngles.y, player.transform.eulerAngles.z);

        if (isFlipped)
        {
            player.transform.localEulerAngles = correct;
        }
        if (isFlipped2)
        {
            player.transform.localEulerAngles = correct2;
        }
    }
}
