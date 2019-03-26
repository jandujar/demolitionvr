using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void PlayAudio(AudioClip _clip, Transform _position){
        GameObject tempObj = Instantiate(Resources.Load("AudioInstance") as GameObject, _position);
        tempObj.GetComponent<AudioSource>().clip = _clip;
        tempObj.GetComponent<AudioInstanceManager>().PlaySound();
    }
}
