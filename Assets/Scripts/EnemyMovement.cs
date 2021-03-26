using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform player;
    public float speed = 0;
    public float minDistance = 2;
    public int damageAmount = 20;
    public bool onScreen = false;
    public bool track = true;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        anim = GetComponent<Animator>();
        if (speed > 0)
        {
            anim.SetInteger("animState", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<EnemyHealth>().enemyDead && onScreen)
        {
            float step = speed * Time.deltaTime;
            if (transform.position.z <= player.position.z)
            {
                transform.position = Vector3.MoveTowards(transform.position, Vector3.up, step);
            }
            else
            {
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance > minDistance && track)
                {
                    transform.LookAt(player);
                    transform.position = Vector3.MoveTowards(transform.position, player.position, step);
                    anim.SetInteger("animState", 1);
                }
                else if (distance < minDistance && track && anim.GetInteger("animState") != 2) {
                    var playerHealth = player.gameObject.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(damageAmount);
                    anim.SetInteger("animState", 2);
                }
            }
        }

        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("I collided with the player");
            //var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            //playerHealth.TakeDamage(damageAmount);
            
        }
        if (other.gameObject.CompareTag("MainCamera"))
        {
            onScreen = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            onScreen = true;
        }
    }
}
