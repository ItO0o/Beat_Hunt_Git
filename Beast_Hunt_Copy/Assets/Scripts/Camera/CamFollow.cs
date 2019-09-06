using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    GameObject player;
    GameObject pointer;

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player");
        pointer = GameObject.Find("CameraPointer");
    }

    // Update is called once per frame
    void LateUpdate() {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z), pointer.transform.position - new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z), out hit, (player.transform.position - pointer.transform.position).magnitude, 1)) {
            if (hit.transform.tag.Equals("Wall")) {
                this.transform.position = hit.point;
                this.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z));
            } else {
                this.transform.position = GameObject.Find("CameraPointer").transform.position;
                this.transform.LookAt(GameObject.Find("Player").transform.position); this.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z));
            }
        } else {
            this.transform.position = GameObject.Find("CameraPointer").transform.position;
            this.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z));
        }
        Debug.DrawRay(player.transform.position, pointer.transform.position - player.transform.position, Color.red, 0.1f);
    }

}
