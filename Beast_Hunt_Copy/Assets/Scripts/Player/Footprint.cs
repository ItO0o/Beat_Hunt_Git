using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour {
    GameObject player;
    List<GameObject> footPrints;
    GameObject parent;

    [SerializeField]
    int maxPrint;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
        footPrints = new List<GameObject>();
        parent = GameObject.Find("FootPrints");
    }

    // Update is called once per frame
    void Update() {
        if (footPrints.Count >= maxPrint) {
            DestroyImmediate(footPrints[0]);
            footPrints.RemoveAt(0);
        }
    }
    Color c = new Color();
    private void FixedUpdate() {
        for (int i = 0; i < footPrints.Count; i++) {
            c = footPrints[i].GetComponent<Renderer>().material.color;
            if (c.a > 0) {
                footPrints[i].GetComponent<Renderer>().material.color = new Color(c.r, c.g, c.b, c.a - 0.002f);
            }
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.transform.name.Equals("Floor_Terrain")) {
            GameObject temp = Instantiate<GameObject>(Resources.Load<GameObject>("Footprint_Plane"));
            temp.transform.position = this.transform.position;
            temp.transform.rotation = player.transform.rotation;
            temp.transform.Rotate(0, 180, 0);
            temp.transform.parent = parent.transform;
            footPrints.Add(temp);
        }
    }
}
