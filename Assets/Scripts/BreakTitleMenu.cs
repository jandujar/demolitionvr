using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakTitleMenu : MonoBehaviour {

    [SerializeField] GameObject wall;
    private bool moveBall;
    [SerializeField] Animator fadeOutAnim;

    // Start is called before the first frame update
    void Start() {
        moveBall = false;
    }

    // Update is called once per frame
    void Update() {
        if (moveBall) {
            breakWall();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (InputManager.Instance.GetAnyButtonDown())
        {
            moveBall = true;
        }
    }

    public void breakWall() {
        foreach (Transform child in wall.transform)
        {
            child.GetComponent<Rigidbody>().isKinematic = false;
        }
        Invoke("fadeOut", 3);
        Invoke("goToGameScene", 5);
        
    }

    private void goToGameScene() {
        SceneManager.LoadScene("Game");
    }

    private void fadeOut()
    {
        fadeOutAnim.SetBool("fade", true);
    }

}
