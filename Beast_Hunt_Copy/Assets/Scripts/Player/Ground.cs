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
        if (c.name.Equals("Floor_Terrain"))
        {
            this.transform.parent.GetComponent<Animator>().SetBool("Ground",true);
            this.transform.parent.GetComponent<PlayerMove>().currentAction = PlayerMove.Action.non;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        this.transform.parent.GetComponent<Animator>().SetBool("Ground", false);
    }
}
