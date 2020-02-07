using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditFloor : MonoBehaviour {
    Terrain floor;
    GameObject cube;
    float height = 0;
    float[,] heights;
    // Start is called before the first frame update
    void Start() {
        floor = GameObject.Find("Floor_Terrain").GetComponent<Terrain>();
        cube = GameObject.Find("Cube");
        //Terrain terrain = floor;
        //Vector3 position = this.transform.position;     //現在の位置を取得
        heights = new float[floor.terrainData.heightmapWidth, floor.terrainData.heightmapHeight];
        //  posisionのx,z座標に対応する、terrainの高さ（y座標）を取得
        //float h = terrain.terrainData.GetInterpolatedHeight(position.x / terrain.terrainData.size.x, position.z / terrain.terrainData.size.z);
        //this.transform.position = new Vector3(position.x, h, position.z);

        //Debug.Log(floor.terrainData.heightmapWidth);
        for (int i = 0; i < floor.terrainData.heightmapWidth; i++) {
            for (int j = 0; j < floor.terrainData.heightmapHeight; j++) {
                heights[i, j] = height;
                //height += 1f / (float)floor.terrainData.heightmapHeight;
                //height = 0;
                //Debug.Log(height);
            }
            //height = 0;
        }

        floor.terrainData.SetHeights(0, 0, heights);
        //height++;
        //GetComponent<Rigidbody>().freezeRotation = true
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetKeyDown(KeyCode.A)) {
        //    int[] temp = WorldToっHeights(cube.transform.position);
        //    Debug.Log(temp[0] + "," + temp[1]);
        //    heights[temp[0], temp[1]] = 0.1f;
        //    floor.terrainData.SetHeights(0, 0, heights);
        //}
    }

    void Footprint() {
        //floor.
    }

    private void OnCollisionEnter(Collision collision) {
        //if (collision.transform.tag.Equals("Foot")) {
        //    float h = floor.terrainData.GetInterpolatedHeight(collision.transform.position.x / floor.terrainData.size.x, collision.transform.position.z / floor.terrainData.size.z);
        //    h -= 1;
        //    floor.terrainData.SetHeights((int)(collision.transform.position.x / floor.terrainData.size.x), (int)(collision.transform.position.z / floor.terrainData.size.z),new float[,]);
        //}
    }

    private int[] WorldToHeights(Vector3 worldPos) {
        //int x = 0, y = 0;
        ////x = (int)(worldPos.x + floor.terrainData.heightmapWidth) / 2;
        ////Debug.Log((worldPos.x + (float)floor.terrainData.heightmapWidth) % 2);
        //if ((worldPos.x + (float)floor.terrainData.heightmapWidth) % 2 > 1f) {
        //    x = (int)(worldPos.x + (float)floor.terrainData.heightmapWidth) / 2 + 1;
        //    Debug.Log("aaaaa");
        //} else if ((worldPos.x + (float)floor.terrainData.heightmapWidth) % 2 <= 1f) {
        //    x = (int)(worldPos.x + (float)floor.terrainData.heightmapWidth) / 2;
        //    Debug.Log("bbbb");
        //}

        //if ((worldPos.z + (float)floor.terrainData.heightmapHeight) % 2 > 1f) {
        //    y = (int)(worldPos.z + (float)floor.terrainData.heightmapHeight) / 2 + 1;
        //} else if ((worldPos.z + (float)floor.terrainData.heightmapHeight) % 2 <= 1f) {
        //    y = (int)(worldPos.z + (float)floor.terrainData.heightmapHeight) / 2;
        //}
        ////y = (int)(worldPos.z + floor.terrainData.heightmapHeight) / 2;
        int[] heightIndex = new int[2];
        //heightIndex[0] = y;
        //heightIndex[1] = x;
        return heightIndex;
    }
}
