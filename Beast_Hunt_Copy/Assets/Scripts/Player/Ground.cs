using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public void Update()
    {
        
    }
    public void OnTriggerEnter(Collider c)
    {
        if (c.name.Equals("Plane"))
        {
            this.transform.parent.GetComponent<Animator>().SetBool("Ground",true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        this.transform.parent.GetComponent<Animator>().SetBool("Ground", false);
    }
}
