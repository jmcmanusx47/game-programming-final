using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCamera : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, new Vector3(0, 1, 0), out hit, 100f))
        {
            if (hit.collider.CompareTag("MainCamera"))
            {
                print("Stopped Camera");
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehavior>().SetVelocity(0);

            }
        }
    }
}
