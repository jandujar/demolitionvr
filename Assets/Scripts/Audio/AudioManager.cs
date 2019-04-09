using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager:Singleton<AudioManager>
{
    public void PlayAudio(AudioClip _clip, Transform _position){
        Debug.Log("HEY MAN");
        GameObject tempObj = Instantiate(Resources.Load("AudioInstance") as GameObject, _position);
        
        tempObj.GetComponent<AudioSource>().clip = _clip;
        tempObj.GetComponent<AudioInstanceManager>().PlaySound();
    }
}
