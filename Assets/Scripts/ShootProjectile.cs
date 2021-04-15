using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float speed = 100;
    public float fireRate = .5f;

    float saveFire;
    bool scatter = false;
    bool lockOn = false;
    float cd;
    GameObject projectile;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cd = fireRate;
        saveFire = fireRate;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("space") && !gameObject.GetComponent<PlayerHealth>().playerDead && cd <= 0)
        {
            anim.SetInteger("animState", 2);
            var pos = transform.position;
            pos.y += 1.5f;
            if (scatter)
            {
                GameObject leftProjectile =
                    Instantiate(projectilePrefab, pos + transform.forward, transform.rotation) as GameObject;
                GameObject rightProjectile =
                    Instantiate(projectilePrefab, pos + transform.forward, transform.rotation) as GameObject;

                Rigidbody rbl = leftProjectile.GetComponent<Rigidbody>();

                Vector3 leftDirection = Quaternion.AngleAxis(60, Vector3.up) * Vector3.left;
                rbl.AddForce(leftDirection * speed, ForceMode.VelocityChange);

                leftProjectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

                Rigidbody rbr = rightProjectile.GetComponent<Rigidbody>();

                Vector3 rightDirection = Quaternion.AngleAxis(-60, Vector3.up) * Vector3.right;
                rbr.AddForce(rightDirection * speed, ForceMode.VelocityChange);
                
                rightProjectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

            }
            projectile = 
                Instantiate(projectilePrefab, pos + transform.forward, transform.rotation) as GameObject;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (lockOn)
            {
                var projectileLock = projectile.GetComponent<LockOn>();
                projectileLock.lockOn = true;
            }

            else
            {
                rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
            }
          
            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

            cd = fireRate;
        }

        if (cd > 0)
        {
            cd -= Time.deltaTime;
        }


    }

    public void Frenzy(float fire)
    {
        fireRate *= fire;
        Invoke("ResetFire", 3f);

    }

    public void ResetFire()
    {
        fireRate = saveFire;
    }

    public void ScatterShot()
    {
        scatter = true;
        Invoke("ResetScatter", 3f);
    }

    void ResetScatter()
    {
        scatter = false;
    }

    public void LockOn()
    {
        lockOn = true;
        Invoke("ResetLockOn", 3f);
    }

    private void ResetLockOn()
    {
        lockOn = false;
    }

}
