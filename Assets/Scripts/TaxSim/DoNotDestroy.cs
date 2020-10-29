using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TaxSim;

public class DoNotDestroy : MonoBehaviour
{
    Wallet wallet = new Wallet();
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("noDestroy");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        foreach (GameObject obj in objs)
        {
            DontDestroyOnLoad(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
