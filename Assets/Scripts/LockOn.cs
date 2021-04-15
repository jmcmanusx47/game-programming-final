using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    public float speed = 100;
    public bool lockOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lockOn)
        {
            GameObject enemy = FindClosestEnemy();
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,
                enemy.transform.position, step);
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float min = float.MaxValue;
        GameObject closest = null;
        foreach (GameObject enemy in enemies)
        {
            var dist = Vector3.Distance(enemy.transform.position,
                gameObject.transform.position);
            if (dist < min)
            {
                min = dist;
                closest = enemy;
            }
        }
        return closest;
    }

}
