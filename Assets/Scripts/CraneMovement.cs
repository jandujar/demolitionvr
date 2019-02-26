using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMovement : MonoBehaviour
{


    [SerializeField] Transform craneArm;
    [SerializeField] Transform upperCrane, Crane;

    public float speed = 5;
    public float rotationSpeed = 10;
    public float armCraneSpeed = 8;

    public bool resetRotation = false;


    float leftAxis, rightAxis;
    float leftWheelMovement, rightWheelMovement, upperCraneRotationAxis, armCraneAxis;
    

    // Start is called before the first frame update
    void Start()
    {
        leftAxis = rightAxis = armCraneAxis = upperCraneRotationAxis = 0;
        leftWheelMovement = rightWheelMovement =  0;

        if (craneArm == null)
            craneArm = GameObject.Find("arm").transform;

        if (upperCrane == null)
            upperCrane = GameObject.Find("UpperCrane").transform;

        if (Crane == null)
            Crane = GameObject.Find("JanduCrane").transform;
    }

    // Update is called once per frame
    void Update()
    {
        leftAxis = rightAxis = 0;
        upperCraneRotationAxis = 0;
        armCraneAxis = 0;


        //TEMP INPUTS, here we will manage with the levers the value of each axis variable
        leftAxis = InputManager.Instance.GetAxisVertical();
        rightAxis = InputManager.Instance.GetAxisVertical2();

        if (InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON1))
            upperCraneRotationAxis = 1;
        else if(InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON2))
            upperCraneRotationAxis = -1;

        if (InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON3))
            armCraneAxis = 1;
        else if (InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON4))
            armCraneAxis = -1;


        if (rightAxis > 1.0f)
            rightAxis = 1.0f;
        else if(rightAxis < -1.0f)
            rightAxis = -1.0f;
        if (leftAxis > 1.0f)
            rightAxis = 1.0f;
        else if (leftAxis < -1.0f)
            rightAxis = -1.0f;


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
        craneArm.Rotate(Vector3.forward * (armCraneAxis * armCraneSpeed) * Time.deltaTime * 2);
        
        
        if (craneArm.transform.rotation.eulerAngles.z > 336)
            craneArm.transform.rotation = Quaternion.Euler(new Vector3 (craneArm.transform.rotation.eulerAngles.x, craneArm.transform.eulerAngles.y, 336));
        else if (craneArm.transform.rotation.eulerAngles.z < 289)
            craneArm.transform.rotation = Quaternion.Euler(new Vector3(craneArm.transform.rotation.eulerAngles.x, craneArm.transform.rotation.eulerAngles.y, 289));
       

        //Upper Crane rotation
        upperCrane.Rotate(Vector3.forward * (upperCraneRotationAxis * rotationSpeed) * Time.deltaTime * 2);

    }
}
