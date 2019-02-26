using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField]GameObject wall = null;
    [SerializeField]GameObject bricks = null;
     bool moving=false;
     float counter=10;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag!="BuildingObject"){
            wall.SetActive(false);
        bricks.SetActive(true);

        }
        
    }

}
