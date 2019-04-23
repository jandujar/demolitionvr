using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCamera : MonoBehaviour
{
    [SerializeField] bool isXRCamera;

    // Start is called before the first frame update
    void Awake()
    {
        if(isXRCamera && !InputManager.Instance.IsXRConnected() ||
            !isXRCamera && InputManager.Instance.IsXRConnected())
        {
            gameObject.SetActive(false);
        }
    }
}
