using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstanceManager : MonoBehaviour
{
    public AudioSource audioSource;
    bool start;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(start);
        if(!audioSource.isPlaying && start){
            Destroy(gameObject);
        }
    }
    public void PlaySound(){
        if(audioSource == null) audioSource = GetComponent<AudioSource>();
        start = true;
        audioSource.Play();
    }
}
