using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public int cameraVelocity = 4;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, cameraVelocity);
    }

    public void SetRotation(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    public void SetVelocity(int newVelocity)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, newVelocity);
    }
}
