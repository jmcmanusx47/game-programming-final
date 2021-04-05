using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : MonoBehaviour
{
    public Transform player;
    public float delay = 3;
    public float speed = 3;
    public int projectileDamage = 20;
    public bool onScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onScreen)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }


    }

    /*private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("I collided with the player");
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);

        }
        if (other.gameObject.CompareTag("MainCamera"))
        {
            onScreen = true;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        //MAKE SURE THE ENEMIES ARE ACTUALLY TAGGED OR ELSE IT WONT DO DAMAGE!!!!!
        if (other.gameObject.CompareTag("Player"))
        {
            //Deal damage to player.
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(projectileDamage);
        }
        else if (other.gameObject.CompareTag("MainCamera"))
        {
            onScreen = true;
        }
    }

}
