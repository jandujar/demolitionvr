using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] ballAudios;
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "BuildingObject"){
            AudioManager.Instance.PlayAudio(ballAudios[0], transform);
        }
        
    }
}
