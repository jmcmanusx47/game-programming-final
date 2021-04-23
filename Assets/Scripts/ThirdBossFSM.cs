using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdBossFSM : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Patrol,
        CastingPatrol,
        SpreadFireball,
        MovingSpreadFireball,
        Flying,
        Dead
    }

    public FSMStates currentState;
    public GameObject spellProjectile;
    public GameObject player;
    public float enemySpeed = 5f; 
    public bool onScreen;
    public float shootCooldown = 3.0f;
    float shootCD;
    public float phaseChangeTime = 5.0f;
    public float elapsedTime;
    public float spawnCooldown = 2.0f;
    float spawncd;
    public float castingCooldown = 0.5f;
    float castingcd;

    /*
    These might be used if i was smart but gonna leave them commented for now
    public float rotation = 45f;
    Vector3 leftAngle;
    Vector3 rightAngle;
    bool rotatingLeft;
    */

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    int currentDestinationIndex = 0;
    GameObject spawningPoint;
    public GameObject[] fireballSpawnPoints;
    public GameObject[] angleFireballSpawnPoints;
    public GameObject[] flyingWanderPoints;
    int currentFlyingDestIndex = 0;

    public GameObject MouthFireballSpawn;

    public EnemyHealth enemyhealth;
    int health;
    private bool deadAnimPlayed;

    
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        spawningPoint = GameObject.FindGameObjectWithTag("SpawningPoint");
        fireballSpawnPoints = GameObject.FindGameObjectsWithTag("FireballSpawnPoint");
        angleFireballSpawnPoints = GameObject.FindGameObjectsWithTag("AngleFireballSpawnPoint");
        flyingWanderPoints = GameObject.FindGameObjectsWithTag("FlyingWanderPoint");

        //leftAngle = transform.forward + rotation;
        //rightAngle = transform.forward - rotation;
        //rotatingLeft = true;

        enemyhealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        deadAnimPlayed = false;

        shootCD = shootCooldown;
        castingcd = castingCooldown;
        elapsedTime = phaseChangeTime;
        health = enemyhealth.currentHealth;
        currentState = FSMStates.Idle;
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
                case FSMStates.Idle:
                    UpdateIdleState();
                    break;
                case FSMStates.Patrol:
                    UpdatePatrolState();
                    break;
                case FSMStates.CastingPatrol:
                    UpdateCastingPatrolState();
                    break;
                case FSMStates.SpreadFireball:
                    UpdateSpreadFireballState();
                    break;
                case FSMStates.MovingSpreadFireball:
                    UpdateMovingSpreadFireballState();
                    break;
                case FSMStates.Flying:
                    UpdateFlyingState();
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

    void UpdateIdleState()
    {
        Invoke("Roar", 0.2f);
    }

    void Roar()
    {
        anim.SetTrigger("Fire Breath Attack");
        currentState = FSMStates.Patrol;
    }

    void UpdatePatrolState()
    {
        //print("Patrolling!");

        anim.SetBool("Walk Forward", true);
        transform.rotation = Quaternion.Euler(0, 180, 0);
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
            GameObject projectile = Instantiate(spellProjectile, transform.position + transform.forward + new Vector3(0, 1.5f, -5), transform.rotation);

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
        anim.SetBool("Walk Forward", true);

        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (elapsedTime <= 0)
        {
            currentState = FSMStates.SpreadFireball;
            elapsedTime = phaseChangeTime;
        }

        if (castingcd <= 0)
        {
            Instantiate(spellProjectile, transform.position + transform.forward + new Vector3(0, 1.5f, -5), transform.rotation);
            castingcd = castingCooldown;
        }

        if (castingcd > 0)
        {
            castingcd -= Time.deltaTime;
        }



        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    void UpdateSpreadFireballState()
    {
        anim.SetBool("Walk Forward", false);
        nextDestination = spawningPoint.transform.position;
        
        if (elapsedTime <= 0)
        {
            currentState = FSMStates.MovingSpreadFireball;
            elapsedTime = phaseChangeTime;
            FindNextPoint();
        }



        

        if (spawncd <= 0 && Vector3.Distance(transform.position, spawningPoint.transform.position) < 0.5)
        {
            foreach (GameObject point in angleFireballSpawnPoints)
            {
                Instantiate(spellProjectile, point.transform.position + new Vector3(0, 1.5f, 0), point.transform.rotation);
            }
            spawncd = spawnCooldown;
        }
        
        if (spawncd > 0)
        {
            spawncd -= Time.deltaTime;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    void UpdateMovingSpreadFireballState()
    {
        
        nextDestination = spawningPoint.transform.position;

        if (elapsedTime <= 0)
        {
            currentState = FSMStates.Flying;
            elapsedTime = phaseChangeTime * 4;
            FindNextFlyingPoint();
        }

        /*
        if (Vector3.Distance(transform.rotation.eulerAngles, leftAngle) >= 1)
        {
            transform.Rotate(new Vector3(0, rotation * Mathf.Sin(Time.deltaTime), 0), Space.World);
        }
        else if (Vector3.Distance(transform.rotation.eulerAngles, right) >= 1)
        {
            transform.Rotate(new Vector3(0, -rotation * Mathf.Sin(Time.deltaTime), 0), Space.World);
        } */

        transform.LookAt(player.transform);
        

        if (spawncd <= 0 && Vector3.Distance(transform.position, spawningPoint.transform.position) < 0.5)
        {
            anim.SetTrigger("Fire Breath Attack");
            foreach (GameObject point in fireballSpawnPoints)
            {
                Instantiate(spellProjectile, point.transform.position + new Vector3(0, 1.5f, 0), point.transform.rotation);
            }
            spawncd = spawnCooldown * 2;
        }

        if (spawncd > 0)
        {
            spawncd -= Time.deltaTime;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }
    
    void UpdateFlyingState()
    {
        anim.SetBool("Fly Idle", true);

        if (elapsedTime <= 0)
        {
            anim.SetBool("Fly Idle", false);
            currentState = FSMStates.Patrol;
            elapsedTime = phaseChangeTime;
            FindNextPoint();
        }

        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextFlyingPoint();
        }

        if (spawncd <= 0)
        {
            GameObject projectile = Instantiate(spellProjectile, MouthFireballSpawn.transform.position + transform.forward + new Vector3(0, 1.5f, -5), transform.rotation);
            projectile.transform.LookAt(player.transform);

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
        if (!deadAnimPlayed)
        {
            anim.SetTrigger("Die");
            deadAnimPlayed = true;
        }
        
        GameObject.FindObjectOfType<LevelManager>().LevelWon();

        //Enemy health deals with death stuff.
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

    }

    void FindNextFlyingPoint()
    {
        nextDestination = flyingWanderPoints[currentFlyingDestIndex].transform.position;
        transform.LookAt(flyingWanderPoints[currentFlyingDestIndex].transform.position);

        currentFlyingDestIndex = (currentFlyingDestIndex + 1) % flyingWanderPoints.Length;
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
