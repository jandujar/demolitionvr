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
            StartCoroutine(AudioTimer(0));
        }
        if ((collision.gameObject.name.Contains("Valla") || collision.gameObject.name.Contains("Viga") 
            || collision.gameObject.name.Contains("Farola") || collision.gameObject.name.Contains("Barrel") 
            || collision.gameObject.name.Contains("Pipe")) && !audioIsPlaying)
        {
            audioIsPlaying = true;
            AudioManager.Instance.PlayAudio(ballAudios[1], transform);
            StartCoroutine(AudioTimer(1));
        }
    }
    IEnumerator AudioTimer(int _audio)
    {
        yield return new WaitForSeconds(ballAudios[_audio].length);
        audioIsPlaying = false;
    }
}
