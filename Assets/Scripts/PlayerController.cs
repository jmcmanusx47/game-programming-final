using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!gameObject.GetComponent<PlayerHealth>().playerDead)
        {
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

            if (!gameObject.GetComponent<PlayerHealth>().playerDead)
            {
                Vector3 newPosition = new Vector3(moveH, 0.0f, moveV);
                //transform.LookAt(newPosition + transform.position);
                transform.Translate(newPosition * speed * Time.deltaTime, Space.World);
            }
        }
    }


}
