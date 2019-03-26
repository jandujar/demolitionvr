using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstanceManager : MonoBehaviour
{
    AudioSource audioSource;
    bool start;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying && start){
            Destroy(this);
        }
    }
    public void PlaySound(){
        audioSource.Play();
        start = true;
    }
}
