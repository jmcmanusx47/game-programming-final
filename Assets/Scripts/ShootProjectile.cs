using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float speed = 100;
    public float fireRate = .5f;

    float cd;
    GameObject projectile;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cd = fireRate;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("space") && !gameObject.GetComponent<PlayerHealth>().playerDead && cd <= 0)
        {
            anim.SetInteger("animState", 2);
            var pos = transform.position;
            pos.y += 1.5f;
            projectile = 
                Instantiate(projectilePrefab, pos + transform.forward, transform.rotation) as GameObject;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

            cd = fireRate;
        }

        if (cd > 0)
        {
            cd -= Time.deltaTime;
        }


    }

}
