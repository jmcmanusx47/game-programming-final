using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFollowBullet : MonoBehaviour
{

    public GameObject projectilePrefab;
    //public float speed = 100;
    GameObject projectile;

    public float cooldown = 3;

    public float cd;

    bool onScreen = false;
    void Start()
    {
        cd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cd <= 0 && !gameObject.GetComponent<EnemyHealth>().enemyDead &&
            onScreen)
        {
            projectile =
                Instantiate(projectilePrefab, transform.position + transform.forward + new Vector3(0, 1.5f, 0), transform.rotation) as GameObject;

            //Rigidbody rb = projectile.GetComponent<Rigidbody>();

            //rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

            cd = cooldown;
        }


        if (cd > 0)
        {
            cd -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            onScreen = true;
        }
    }
}
