using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;

    void Start()
    {
     
    }

    void Update()
    {
        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");

        if (!gameObject.GetComponent<PlayerHealth>().playerDead)
        {
            Vector3 newPosition = new Vector3(moveH, 0.0f, moveV);
            //transform.LookAt(newPosition + transform.position);
            transform.Translate(newPosition * speed * Time.deltaTime, Space.World);
        }
    }


}
