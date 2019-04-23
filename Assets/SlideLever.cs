using UnityEngine;
using Valve.VR;

public class SlideLever : MonoBehaviour
{

    //public EVRButtonId buttonToTrigger = EVRButtonId.k_EButton_SteamVR_Trigger;
    //public  clickGrab;
    //public Transform controller;
    public Transform lever;

    [SerializeField] Transform maxPoint;
    float maxZ;

    [SerializeField] Transform minPoint;
     float minZ;

    float[] anchorPoints = new float[3];
    public float snapDistance = 0.05f;
    [SerializeField] protected Transform controllerTransform;
    public delegate void SlideLeverEvent(int position);
    public static event SlideLeverEvent OnLeverSnap;
    //----------------------------------------------

    [Header("Maximo y minimo")]
    [Range(-1,1)]
    public float valueLever = 0;
    public float position = 0;

    private void Start()
    {
        controllerTransform = null;
        maxZ = maxPoint.localPosition.z;
        minZ = minPoint.localPosition.z;
        anchorPoints[0] = minZ;
        anchorPoints[1] = 0f;
        anchorPoints[2] = maxZ;
        
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
        
        position = valueLever * maxZ;
        lever.transform.localPosition = new Vector3(lever.transform.localPosition.x, lever.transform.localPosition.y,position);
     
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

            valueLever = (position.z / maxZ) * -1;
            //valueLever es el valor que tenemos que pasar para 
            //hacer el movimiento de la demoledora
        }
        
        
    }

     
    private void OnTriggerStay(Collider other){

        if(other.gameObject.tag == "Hand"){
            
            if(InputManager.Instance.GetTriggerVRRightHand()){
                controllerTransform = other.gameObject.transform;
                //controllerTransform.position = other.gameObject.transform.position;
                //controllerTransform.rotation = other.gameObject.transform.rotation;
            }
            if(InputManager.Instance.GetTriggerVRLeftHand()){
                controllerTransform = other.gameObject.transform;
                //controllerTransform.position = other.gameObject.transform.position;
                //controllerTransform.rotation = other.gameObject.transform.rotation;
            }
        }
    }

     void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Hand"){
            SnapToPosition();
            controllerTransform = null;
            
            
        }
    }
}
