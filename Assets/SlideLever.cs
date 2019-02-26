using UnityEngine;
using Valve.VR;

public class SlideLever : MonoBehaviour
{

    //public EVRButtonId buttonToTrigger = EVRButtonId.k_EButton_SteamVR_Trigger;
    public SteamVR_Action_Boolean clickGrab;
    public Transform controller;
    public Transform lever;

    [SerializeField] Transform maxPoint;
    float maxZ;

    [SerializeField] Transform minPoint;
     float minZ;

    float[] anchorPoints = new float[3];
    public float snapDistance = 0.05f;
    protected Transform controllerTransform;

    public delegate void SlideLeverEvent(int position);
    public static event SlideLeverEvent OnLeverSnap;
    //----------------------------------------------


    [Header("Maximo y minimo")]
    [Range(-1,1)]
    public float valueLever = 0;

    public float position = 0;


    private void Start()
    {
        maxZ = maxPoint.localPosition.z;
        minZ = minPoint.localPosition.z;
        anchorPoints[0] = minZ;
        anchorPoints[1] = 0f;
        anchorPoints[2] = maxZ;
        
    }

    public bool isclickGrab()
    {
        Debug.Log("GRAB!");
        return clickGrab.GetState(SteamVR_Input_Sources.Any);
    }
    //----------------------------------------------

    public void ButtonPressDown(SteamVR_Skeleton_Pose_Hand controller)//(EVRButtonId button, VRControllerInput controller)
    {
        //If button pressed is desired "trigger" button
        if (isclickGrab())
        {
            //controllerTransform = controller.gameObject.transform;
            controllerTransform.position = controller.position;
            controllerTransform.rotation = controller.rotation;
        }
    }

    public void ButtonPressUp(SteamVR_Skeleton_Pose_Hand controller)//(EVRButtonId button, VRControllerInput controller)
    {
        //If button pressed is desired "trigger" button
        if (isclickGrab())
        {
            controllerTransform = null;

            //Attempt to snap lever into a slot
            SnapToPosition();
        }
    }



    protected void SnapToPosition()
    {
        //Cycle through each predefined anchor point
        for (int i = 0; i < anchorPoints.Length; i++)
        {
            //If lever is within "snapping distance" of that anchor point
            if (Mathf.Abs(lever.localPosition.z - anchorPoints[i]) < snapDistance)
            {
                //Get current lever position and update z pos to anchor point
                Vector3 position = lever.transform.localPosition;
                position.z = anchorPoints[i];
                lever.transform.localPosition = position;

                //Break so it stops checking other anchor points
                break;
            }
        }
    }
    
       
    public void Update()
    {
        /*
        position = valueLever * maxZ;
        lever.transform.localPosition = new Vector3(lever.transform.localPosition.x, lever.transform.localPosition.y,position);
        */

        //If the user is "grabbing" the lever
        if (controllerTransform != null)
        {
            //Get the controller's position relative to the lever (lever's local position)
            float zPos = transform.InverseTransformPoint(controllerTransform.position).z;

            //Get the lever's current local position
            Vector3 position = lever.transform.localPosition;

            //Set lever's z position to the Z of the converted controller position
            //Clamp it so the lever doesn't go too far either way
            position.z = Mathf.Clamp(zPos, maxZ, minZ);

            //Set lever to new position
            lever.transform.localPosition = position;

            valueLever = position.z / maxZ;
            //valueLever es el valor que tenemos que pasar para 
            //hacer el movimiento de la demoledora
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
