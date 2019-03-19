using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCalculator : MonoBehaviour
{
    public Transform[] cylinders;
    public Transform[] joints;
    Vector3[] cylindersRotation;
    Vector3[] cylindersPositions;

    Vector3 tempDistance;

    // Start is called before the first frame update
    void Start()
    {
        cylindersPositions = new Vector3[cylinders.Length];
        cylindersRotation = new Vector3[cylinders.Length];
    }

    void CalculateCylindersPosition()
    {
        for(int i = 0; i < cylinders.Length; i++)
        {
            tempDistance = joints[i + 1].position - joints[i].position;
            cylindersPositions[i] = joints[i].position + tempDistance / 2;
            cylindersRotation[i] = new Vector3(Mathf.Rad2Deg * Mathf.Atan2(tempDistance.z, tempDistance.y) , 0, Mathf.Rad2Deg * Mathf.Atan2(tempDistance.x, tempDistance.y));
            cylinders[i].position = cylindersPositions[i];
            cylinders[i].rotation = Quaternion.Euler(cylindersRotation[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCylindersPosition();
    }
}
