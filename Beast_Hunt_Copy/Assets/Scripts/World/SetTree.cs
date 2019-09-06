using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTree : MonoBehaviour
{
    [SerializeField]
    private int treeCount;
    // Start is called before the first frame update
    void Start()
    {
        GameObject trees = GameObject.Find("Trees");
        for (int i = 0; i < treeCount; i++)
        {
            GameObject instObj = Instantiate(Resources.Load<GameObject>("Tree"), trees.transform);
            instObj.name = "Tree_" + treeCount;
            instObj.transform.localScale = new Vector3(Random.Range(1f, 1.5f), Random.Range(0.5f, 2f), Random.Range(1f, 1.5f));
            instObj.transform.position = new Vector3(Random.Range(0f, 50f), 0, Random.Range(0f, 50f));
            instObj.transform.rotation = Quaternion.Euler(0,Random.Range(0,180),0);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
