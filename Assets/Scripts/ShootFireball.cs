using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float cooldown = 3;

    public float cd;


    void Start()
    {
        cd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && cd <= 0 && !gameObject.GetComponent<PlayerHealth>().playerDead)
        {
            GameObject projectile = Instantiate(fireballPrefab,
                transform.position + transform.forward, transform.rotation) as GameObject;

            //Rigidbody rb = projectile.GetComponent<Rigidbody>();

            //rb.AddForce(transform.forward, ForceMode.VelocityChange);

            projectile.transform.SetParent(
                GameObject.FindGameObjectWithTag("ProjectileParent").transform);

            cd = cooldown;
        }

        if (cd > 0)
        {
            cd -= Time.deltaTime;
        }
    }
}
