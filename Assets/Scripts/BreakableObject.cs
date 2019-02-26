using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    Rigidbody rb;
    bool moving=false;

    Transform lastPosition;
     float counter=10;
    private void Start() {
        rb=gameObject.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag!="BuildingObject"){
            rb.constraints=RigidbodyConstraints.None;
            moving=true;
            lastPosition=gameObject.GetComponent<Transform>();
            
        }
        
    }

    // Optimizacion
        private void Update() {
        if(moving){
        counter-=Time.deltaTime;
        if(counter<=0){
            counter=10;
            if(lastPosition.position==gameObject.GetComponent<Transform>().position){
                Destroy(gameObject);
            }else{
                lastPosition=lastPosition=gameObject.GetComponent<Transform>();
            }
        }
        }
        
    }
}
