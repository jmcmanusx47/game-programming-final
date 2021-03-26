using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float speed = 100;
    GameObject projectile;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && !gameObject.GetComponent<PlayerHealth>().playerDead)
        {
            anim.SetInteger("animState", 2);
            var pos = transform.position;
            pos.y += 1.5f;
            projectile = 
                Instantiate(projectilePrefab, pos + transform.forward, transform.rotation) as GameObject;

            var destroyProjectile =
                projectile.GetComponent<DestroyProjectile>();
            destroyProjectile.player = gameObject;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

        }

        
    }

}
