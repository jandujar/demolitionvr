using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakTitleMenu : MonoBehaviour {

    [SerializeField] GameObject wall;
    Vector3 finalPosition;
    private bool moveBall;

    // Start is called before the first frame update
    void Start() {
        finalPosition = new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z);
        moveBall = false;
    }

    // Update is called once per frame
    void Update() {
        if (moveBall) {
            transform.position = Vector3.Lerp(this.transform.position, finalPosition, Time.deltaTime * 3);
        }
    }

    public void breakWall() {
        moveBall = true;
        Invoke("goToGameScene", 3);
    }

    private void goToGameScene() {
        SceneManager.LoadScene("CraneTest");
    }

    private void OnCollisionEnter(Collision collision) {
        foreach (Transform child in wall.transform) {
            child.GetComponent<Rigidbody>().isKinematic = false;
        }
        moveBall = false;
    }
}
