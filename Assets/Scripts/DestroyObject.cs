using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float destroyWait = 1;

    void Start()
    {
        Destroy(gameObject, destroyWait);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
