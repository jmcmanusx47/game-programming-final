using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour
{
    public GameObject player;
    public PlayerController controller;
    public float delay;
    private float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Freeze()
    {
        playerSpeed = controller.speed;
        controller.speed = 0;
        Invoke("UnFreeze", delay);
    }

    public void UnFreeze()
    {
        controller.speed = playerSpeed;
    }
}
