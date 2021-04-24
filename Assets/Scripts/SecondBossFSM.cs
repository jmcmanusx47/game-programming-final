using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossFSM : MonoBehaviour
{
    public enum FSMStates
    {
        Patrol,
        CastingPatrol,
        SpawningPatrol,
        Dead
    }

    public FSMStates currentState;
    public GameObject trackingProjectile;
    public GameObject spellProjectile;
    public GameObject skeletonSpawnPrefab;
    public GameObject player;
    public float enemySpeed = 5f; 
    public bool onScreen;
    public float shootCooldown = 3.0f;
    float shootCD;
    public float shootSpeed = 100;
    public float phaseChangeTime = 5.0f;
    public float elapsedTime;
    public float spawnCooldown = 2.0f;
    float spawncd;
    public float castingCooldown = 0.5f;
    float castingcd;

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    int currentDestinationIndex = 0;
    GameObject spawningPoint;
    public GameObject[] enemySpawnPoints;
    public GameObject enemySpawnVFX;

    public EnemyHealth enemyhealth;
    int health;
    private bool isDead;

    
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        spawningPoint = GameObject.FindGameObjectWithTag("SpawningPoint");
        
        enemyhealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();

        isDead = false;
        shootCD = shootCooldown;
        castingcd = castingCooldown;
        elapsedTime = phaseChangeTime;
        health = enemyhealth.currentHealth;
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
                case FSMStates.CastingPatrol:
                    UpdateCastingPatrolState();
                    break;
                case FSMStates.SpawningPatrol:
                    UpdateSpawningState();
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

        //anim.SetInteger("animState", 1); //walk animation


        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }

        if (elapsedTime <= 0)
        {
            currentState = FSMStates.CastingPatrol;
            elapsedTime = phaseChangeTime;
        }
        
        if (shootCD <= 0)
        {
            GameObject projectile = Instantiate(trackingProjectile, transform.position + transform.forward + new Vector3(0, 1.5f, 0), transform.rotation);

            shootCD = shootCooldown;
        }

        if (shootCD > 0)
        {
            shootCD -= Time.deltaTime;
        }


        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);

    }

    void UpdateCastingPatrolState()
    {
        //print("Aggressively Patrolling!");
        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (elapsedTime <= 0)
        {
            currentState = FSMStates.SpawningPatrol;
            elapsedTime = phaseChangeTime;
        }

        if (castingcd <= 0)
        {
            Instantiate(spellProjectile, transform.position + transform.forward + new Vector3(0, 1.5f, 0), transform.rotation);
            castingcd = castingCooldown;
        }

        if (castingcd > 0)
        {
            castingcd -= Time.deltaTime;
        }



        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    void UpdateSpawningState()
    {
        nextDestination = spawningPoint.transform.position;
        
        if (elapsedTime <= 0)
        {
            currentState = FSMStates.Patrol;
            elapsedTime = phaseChangeTime;
            FindNextPoint();
        }

        

        if (spawncd <= 0)
        {
            foreach (GameObject point in enemySpawnPoints)
            {
                Instantiate(skeletonSpawnPrefab, point.transform.position, point.transform.rotation);
                Instantiate(enemySpawnVFX, point.transform.position + new Vector3(0, 0, 1.5f), Quaternion.Euler(90, 0.0f, 0.0f));
            }
            spawncd = spawnCooldown;
        }
        
        if (spawncd > 0)
        {
            spawncd -= Time.deltaTime;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
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
        //print("On screen!");
        if (other.gameObject.CompareTag("MainCamera"))
        {
            
            onScreen = true;
        }
    }

}
