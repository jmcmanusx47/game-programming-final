using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float speed = 100;
    GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && !gameObject.GetComponent<PlayerHealth>().playerDead)
        {
            var pos = transform.position;
            pos.y += 1.5f;
            projectile = 
                Instantiate(projectilePrefab, pos + transform.forward, transform.rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

        }

        
    }

}
