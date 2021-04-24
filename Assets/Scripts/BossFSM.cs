using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    public enum FSMStates
    {
        Patrol,
        AggressivePatrol,
        Charge,
        Dead
    }

    public FSMStates currentState;
    public GameObject spellProjectile;
    public GameObject player;
    public float enemySpeed = 5f;
    public float chargeSpeed = 10f;
    public float chargeLength = 10f;
    public bool onScreen;
    public float shootCooldown = 2.0f;
    float shootCD;
    public float shootSpeed = 100;
    public float phaseChangeTime = 5.0f;
    public float elapsedTime;
    public float chargeCooldown = 2.0f;
    float chargecd;
    public int chargeDamage = 50;

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    int currentDestinationIndex = 0;

    public EnemyHealth enemyhealth;
    int health;
    public bool isCharging;
    bool isDead;

    
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        
        enemyhealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();

        chargecd = chargeCooldown;
        shootCD = shootCooldown;
        elapsedTime = phaseChangeTime;
        health = enemyhealth.currentHealth;
        isDead = false;
        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (onScreen)
        {

            

            health = enemyhealth.currentHealth;

            if (health <= 0)
            {
                currentState = FSMStates.Dead;
            }

            switch (currentState)
            {
                case FSMStates.Patrol:
                    UpdatePatrolState();
                    break;
                case FSMStates.AggressivePatrol:
                    UpdateAggressivePatrolState();
                    break;
                case FSMStates.Charge:
                    UpdateChargeState();
                    break;
                case FSMStates.Dead:
                    UpdateDeadState();
                    break;
            }

            if (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;
            }
            

            
        }


    }

    void UpdatePatrolState()
    {
        //print("Patrolling!");

        anim.SetInteger("animState", 1); //walk animation


        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }

        if (elapsedTime <= 0)
        {
            currentState = FSMStates.AggressivePatrol;
            elapsedTime = phaseChangeTime;
        }
        
        if (shootCD <= 0)
        {
            GameObject projectile = Instantiate(spellProjectile, transform.position + transform.forward + new Vector3(0, 1.5f, 0), transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * shootSpeed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

            shootCD = shootCooldown;
        }

        if (shootCD > 0)
        {
            shootCD -= Time.deltaTime;
        }


        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);

    }

    void UpdateAggressivePatrolState()
    {
        //print("Aggressively Patrolling!");
        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (elapsedTime <= 0)
        {
            currentState = FSMStates.Patrol;
            elapsedTime = phaseChangeTime;
        }

        RaycastHit hit;

        if (chargecd <= 0 && Physics.Raycast(transform.position, new Vector3(0, 0, -1), out hit, 100f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                print("Player in sight!");
                currentState = FSMStates.Charge;

            }
        }

        if (!isCharging && chargecd > 0)
        {
            chargecd -= Time.deltaTime;
        }



        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    //private Vector3 startChargePosition;
    private Vector3 endChargePosition;
    void UpdateChargeState()
    {
        //print("Charging!");
        if (!isCharging && chargecd <= 0.0f)
        {
            isCharging = true;
            //startChargePosition = transform.position;
            endChargePosition = transform.position + new Vector3(0, 0, -chargeLength);
            nextDestination = endChargePosition;
            chargecd = chargeCooldown;
        } 
        else if (chargecd > 0 && Vector3.Distance(transform.position, endChargePosition) < 1)
        {
            isCharging = false;
            currentState = FSMStates.AggressivePatrol;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextDestination, chargeSpeed * Time.deltaTime);

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isCharging && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Charge hit player");
            player.GetComponent<PlayerHealth>().TakeDamage(chargeDamage);
            
        }

    } 

    void UpdateDeadState()
    {

        if (!isDead)
        {
            GameObject.FindObjectOfType<LevelManager>().LevelWon();
            isDead = true;
        }

        //Enemy health deals with death stuff.
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

        //agent.SetDestination(nextDestination);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("On screen!");
        if (other.gameObject.CompareTag("MainCamera"))
        {
            
            onScreen = true;
        }
    }

}
