using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTree : MonoBehaviour
{
    [SerializeField]
    public int treeCount;
    // Start is called before the first frame update
    void Start()
    {
        GameObject trees = GameObject.Find("Trees");
        for (int i = 0; i < treeCount; i++)
        {
            GameObject instObj = Instantiate(Resources.Load<GameObject>("Tree"), trees.transform);
            instObj.name = "Tree_" + treeCount;
            float randY = Random.Range(10f, 25f);
            instObj.transform.localScale = new Vector3(Random.Range(12f, 15f), randY, Random.Range(10f, 15f));
            instObj.transform.position = new Vector3(Random.Range(-50f, 50f), 50, Random.Range(-50f, 50f));
            instObj.transform.rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);
            RaycastHit hit;
            if (Physics.Raycast(instObj.transform.FindChild("Stump").transform.position, -this.transform.up, out hit,100,1))
            {
                instObj.transform.position -= new Vector3(0, hit.distance, 0);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
