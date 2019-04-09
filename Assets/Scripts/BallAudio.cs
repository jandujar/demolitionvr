using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    float audioTimer;
    bool audioIsPlaying;
    private void Start()
    {
        audioIsPlaying = false;
    }
    [SerializeField] AudioClip[] ballAudios;
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "BuildingObject" && !audioIsPlaying)
        {
            audioIsPlaying = true;
            AudioManager.Instance.PlayAudio(ballAudios[0], transform);
            StartCoroutine(AudioTimer());
        }
    }
    IEnumerator AudioTimer()
    {
        yield return new WaitForSeconds(ballAudios[0].length);
        audioIsPlaying = false;
    }
}
