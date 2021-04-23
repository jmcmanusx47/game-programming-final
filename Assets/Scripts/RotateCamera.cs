using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float xRotation = 62;
    bool hasRotatedCamera = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasRotatedCamera)
        {
            if (other.CompareTag("MainCamera"))
            {
                //print("Stopped Camera, called from: " + transform.position);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehavior>().SetRotation(new Vector3(xRotation, 0, 0));
                hasRotatedCamera = true;

            }
        }
    }
}
