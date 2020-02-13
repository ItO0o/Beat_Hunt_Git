using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTree : MonoBehaviour
{
    GameObject[] natureObj;
    [SerializeField]
    public int treeCount;
    // Start is called before the first frame update
    void Start()
    {
        GameObject trees = GameObject.Find("Trees");
        natureObj = new GameObject[3];
        natureObj[0] = Resources.Load<GameObject>("Fir_1");
        natureObj[1] = Resources.Load<GameObject>("Fir_2");
        natureObj[2] = Resources.Load<GameObject>("Grass_1");
        //natureObj[3] = Instantiate(Resources.Load<GameObject>("Fir_1"), trees.transform);
        for (int i = 0; i < treeCount; i++)
        {
            int natureRand = Random.Range(0, 100);
            GameObject instObj;
            if (natureRand > 5) {
                float max = 0.5f, min= 1f;
                instObj = Instantiate(natureObj[2], trees.transform);
                instObj.transform.localScale = new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
                instObj.transform.position = new Vector3(Random.Range(-50f, 50f), 50, Random.Range(-50f, 50f));
                instObj.transform.rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);
                instObj.name += treeCount;
                RaycastHit hit;
                if (Physics.Raycast(instObj.transform.position, -this.transform.up, out hit, 100, 1)) {
                    instObj.transform.position -= new Vector3(0, hit.distance, 0);
                }

            } else if (natureRand <= 5) {
                float randY = Random.Range(1f, 2f);
                instObj = Instantiate(natureObj[Random.Range(0,2)], trees.transform);
                instObj.transform.localScale = new Vector3(Random.Range(1f, 2f), randY, Random.Range(1f, 2f));
                instObj.transform.position = new Vector3(Random.Range(-50f, 50f), 50, Random.Range(-50f, 50f));
                instObj.transform.rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);
                instObj.name += treeCount;
                RaycastHit hit;
                if (Physics.Raycast(instObj.transform.position, -this.transform.up, out hit, 100, 1)) {
                    instObj.transform.position -= new Vector3(0, hit.distance, 0);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
