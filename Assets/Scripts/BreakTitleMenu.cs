using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakTitleMenu : MonoBehaviour {

    [SerializeField] GameObject wall;
    private bool moveBall;

    // Start is called before the first frame update
    void Start() {
        moveBall = false;
    }

    // Update is called once per frame
    void Update() {
        if (moveBall) {
            breakWall();
        }
        if (Input.anyKey)
        {
            moveBall = true;
        }
    }

    public void breakWall() {
        foreach (Transform child in wall.transform)
        {
            child.GetComponent<Rigidbody>().isKinematic = false;
        }
        Invoke("goToGameScene", 3);
    }

    private void goToGameScene() {
        SceneManager.LoadScene("Game");
    }

}
