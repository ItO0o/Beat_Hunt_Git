using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointerMove : MonoBehaviour {

    private GameObject player;
    private float sin;
    private Vector3 tmp;
    private Vector3 playerMoveVec;
    private Vector3 target;
    public float max, min;
    public float rotationSpeed;

    // Use this for initialization
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = GameObject.Find("Player");
        tmp = player.transform.position;
        target = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z + 4);
    }

    // Update is called once per frame
    private void Update() {
        PosSync();
        PointerRange();
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        if (Input.GetKey(KeyCode.UpArrow) && sin > min) {
            this.transform.RotateAround(target, Vector3.Cross(player.transform.position - this.transform.position, this.transform.up), rotationSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow) && sin < max) {
            this.transform.RotateAround(target, -Vector3.Cross(player.transform.position - this.transform.position, this.transform.up), rotationSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            this.transform.RotateAround(target, new Vector3(0, 1, 0), rotationSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.RotateAround(target, new Vector3(0, -1, 0), rotationSpeed);
        }


        if (sin > min && y > 0 || sin < max && y < 0) {
            this.transform.RotateAround(target, Vector3.Cross(player.transform.position - this.transform.position, this.transform.up), y);
        }

        this.transform.RotateAround(target, new Vector3(0, 1, 0), x);
    }
    private void PosSync() {
        this.transform.position += player.transform.position - target;
        target = player.transform.position;
    }

    private void PointerRange() {
        sin = (this.transform.position.y - player.transform.position.y) / (player.transform.position - this.transform.position).magnitude;
    }
}
