using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class EnemyMove : MonoBehaviour {
    [SerializeField]
    int count;
    GameObject[] enemy;
    [SerializeField]
    string prePath;

    // Start is called before the first frame update
    void Start() {
        //string controllerName = "UnityEngine.EnemyMove";
        //string actionName = "TestMethod";
        //var instance = Activator.CreateInstance(Type.GetType(controllerName));
        //var result = (string)Type.GetType(controllerName).GetMethod(actionName).Invoke(instance, new object[] { 1, 10 });



        GameObject pre = Resources.Load<GameObject>(prePath);
        for (int i = 0; i < count; i++) {

        }
    }

    void TestMethod() {
        Debug.Log("aaaaa");
    }

    // Update is called once per frame
    void Update() {

    }
}
