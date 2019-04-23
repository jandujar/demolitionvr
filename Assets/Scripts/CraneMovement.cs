//#define TEMP_INPUTS
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CraneMovement : MonoBehaviour
{


    [SerializeField] Transform craneArm;
    [SerializeField] Transform upperCrane, Crane, mesh_Arm;

    [SerializeField] GameObject leverRightMovement, leverLeftMovement, leverCraneHeight, leverCraneTrasversalMovement;

    public float speed = 5;
    public float rotationSpeed = 10;
    public float armCraneSpeed = 8;

    public bool resetRotation = false;


    float leftAxis, rightAxis, armCraneLateralRotationAxis, armCraneAxis;
    float leftWheelMovement, rightWheelMovement;

    AudioSource engineAudio;
    // Start is called before the first frame update
    void Start()
    {
        leftAxis = rightAxis = armCraneAxis = armCraneLateralRotationAxis = 0;
        leftWheelMovement = rightWheelMovement =  0;

        if (craneArm == null)
            craneArm = transform.Find("Arm").transform;

        if (upperCrane == null)
            upperCrane = transform.Find("UpperCrane").transform;

        if (Crane == null)
            Crane = transform.Find("JanduCrane").transform;


        if (mesh_Arm == null)
            Crane = transform.Find("Arm_mesh").transform;

        engineAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLeverInputs();
        UpdateMovements();
        if(leftAxis == 0 && rightAxis == 0)
        {
            if (armCraneLateralRotationAxis == 0 && armCraneAxis == 0)
                engineAudio.pitch = 1;
            else
                engineAudio.pitch = 1.05f;
        }
        else if((leftAxis == 0 && rightAxis != 0) || rightAxis == 0 && leftAxis != 0)
        {
            if(armCraneLateralRotationAxis == 0 && armCraneAxis == 0)
            engineAudio.pitch = 1.05f; 
            else
                engineAudio.pitch = 1.1f;

        }
        else
        {
            if (armCraneLateralRotationAxis == 0 && armCraneAxis == 0)
                engineAudio.pitch = 1.1f;
            else
                engineAudio.pitch = 1.15f;
        }
    }

    private void UpdateMovements()
    {
        //MOVEMENT LEVERS
        leftWheelMovement = speed * leftAxis;
        rightWheelMovement = speed * rightAxis;
        Crane.Rotate(Vector3.up * (leftWheelMovement - rightWheelMovement) * Time.deltaTime * 2);
        if (leftAxis != 0 && rightAxis != 0)
        {
            Crane.Translate(Vector3.right * (leftWheelMovement * Time.deltaTime) / 10);
            Crane.Translate(Vector3.right * (rightWheelMovement * Time.deltaTime) / 10);
        }

        //Crane LEVER
        mesh_Arm.Rotate(Vector3.forward * (armCraneAxis * armCraneSpeed) * Time.deltaTime * 2);


        if (mesh_Arm.transform.rotation.eulerAngles.z > 336)
            mesh_Arm.transform.rotation = Quaternion.Euler(new Vector3(mesh_Arm.transform.rotation.eulerAngles.x, mesh_Arm.transform.eulerAngles.y, 336));
        else if (mesh_Arm.transform.rotation.eulerAngles.z < 289)
            mesh_Arm.transform.rotation = Quaternion.Euler(new Vector3(mesh_Arm.transform.rotation.eulerAngles.x, mesh_Arm.transform.rotation.eulerAngles.y, 289));

        //Debug.Log(craneArm.transform.localRotation.eulerAngles);


        //Upper Crane rotation
        craneArm.Rotate(Vector3.forward * (armCraneLateralRotationAxis * rotationSpeed) * Time.deltaTime * 2);

        if (craneArm.transform.localRotation.eulerAngles.z > 135 && craneArm.transform.localRotation.eulerAngles.z < 180)
            craneArm.transform.localRotation = Quaternion.Euler(new Vector3(craneArm.localRotation.eulerAngles.x, craneArm.localRotation.eulerAngles.y, 135));
        else if (craneArm.transform.localRotation.eulerAngles.z < 344 && craneArm.transform.localRotation.eulerAngles.z > 180)
            craneArm.transform.localRotation = Quaternion.Euler(new Vector3(craneArm.transform.localRotation.eulerAngles.x, craneArm.transform.localRotation.eulerAngles.x, 344));

    }

    private void UpdateLeverInputs()
    {
        leftAxis = rightAxis = 0;
        armCraneLateralRotationAxis = 0;
        armCraneAxis = 0;

        if(InputManager.Instance.GetButtonDown(InputManager.MiniGameButtons.BUTTON_START))
        {
            SceneManager.LoadScene("Menu");
        }

        //TEMP INPUTS, here we will manage with the levers the value of each axis variable
        if(!InputManager.Instance.IsXRConnected())
        {
            leftAxis = InputManager.Instance.GetAxisVertical();
            rightAxis = InputManager.Instance.GetAxisVertical2();

            if (InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON1))
                armCraneLateralRotationAxis = 1;
            else if (InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON2))
                armCraneLateralRotationAxis = -1;

            if (InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON3))
                armCraneAxis = 1;
            else if (InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON4))
                armCraneAxis = -1;
        }

        else
        {
            rightAxis = leverRightMovement.GetComponent<SlideLever>().valueLever;
            leftAxis = leverLeftMovement.GetComponent<SlideLever>().valueLever;
            armCraneLateralRotationAxis = leverCraneTrasversalMovement.GetComponent<SlideLever>().valueLever;
            armCraneAxis = leverCraneHeight.GetComponent<SlideLever>().valueLever;
        }

        if (rightAxis > 1.0f)
            rightAxis = 1.0f;
        else if (rightAxis < -1.0f)
            rightAxis = -1.0f;
        if (leftAxis > 1.0f)
            rightAxis = 1.0f;
        else if (leftAxis < -1.0f)
            rightAxis = -1.0f;

    }
}
