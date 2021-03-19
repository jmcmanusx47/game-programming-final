using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform player;
    public float speed = 10;
    public float minDistance = 2;
    public int damageAmount = 20;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > player.transform.position.z && !gameObject.GetComponent<EnemyHealth>().enemyDead)
        {
            float step = speed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > minDistance)
            {
                transform.LookAt(player);
                transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
