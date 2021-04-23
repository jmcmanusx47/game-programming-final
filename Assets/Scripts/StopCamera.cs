using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCamera : MonoBehaviour
{
    private bool hasStoppedCamera;

    void Start()
    {
        hasStoppedCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!hasStoppedCamera)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.up, out hit, 100f))
            {
                if (hit.collider.CompareTag("MainCamera"))
                {
                    print("Stopped Camera, called from: " + transform.position);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehavior>().SetVelocity(0);
                    hasStoppedCamera = true;

                }
            }
        } 
        */
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasStoppedCamera)
        {
            if (other.CompareTag("MainCamera"))
            {
                //print("Stopped Camera, called from: " + transform.position);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehavior>().SetVelocity(0);
                hasStoppedCamera = true;

            }
        }

        if(GlobalControl.Instance.currentSceneIndex == 4)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ToggleOnDragonBoss();
        }
    }
}
