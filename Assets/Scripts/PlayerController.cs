using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
    Animator anim;
    Camera cam;

    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    void Update()
    {
        if (!gameObject.GetComponent<PlayerHealth>().playerDead)
        {
            float distFromCamera = Mathf.Abs(transform.position.y - cam.transform.position.y);
            float zMin = cam.ViewportToWorldPoint(new Vector3(0, 0, distFromCamera)).z;
            float zMax = cam.ViewportToWorldPoint(new Vector3(0, 1, distFromCamera)).z;

            float xMin = -5;
            float xMax = 5;

            float moveV = Input.GetAxis("Vertical");
            float moveH = Input.GetAxis("Horizontal");

            if (moveV != 0 || moveH != 0)
            {
                anim.SetInteger("animState", 1);
            }
            else
            {
                anim.SetInteger("animState", 0);
            }
            
            Vector3 newPosition = new Vector3(moveH, 0.0f, moveV);
            //transform.LookAt(newPosition + transform.position);
            transform.Translate(newPosition * speed * Time.deltaTime, Space.World);

            if (transform.position.z > zMax - 2 || transform.position.z < zMin + 2)
            {
                float zPos = Mathf.Clamp(transform.position.z, zMin + 2, zMax - 2);
                transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
            }

            if (transform.position.x > xMax || transform.position.x < xMin)
            {
                float xPos = Mathf.Clamp(transform.position.x, xMin, xMax);
                transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            }


        }
    }


}
