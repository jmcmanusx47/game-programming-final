using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public Text gameText;
    public static bool isGameOver;
    public int shopIndex = 1;
    public bool isGameWon = false;

    public AudioClip winSFX;
    public AudioClip loseSFX;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("j"))
        {
            LevelWon();
        }   
        else if (Input.GetKey("l"))
        {
            LevelLost();
        }
    }
    

    public void LevelLost()
    {
        isGameOver = true;
        isGameWon = false;
        gameText.text = "YOU LOSE!";
        gameText.gameObject.SetActive(true);

        AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position, 0.015f);

        Invoke("LoadShop", 2);
    }

    public void LevelWon()
    {
        isGameOver = true;
        isGameWon = true;
        gameText.text = "YOU WIN!";

        AudioSource.PlayClipAtPoint(winSFX, transform.position);

        gameText.gameObject.SetActive(true);
        Invoke("LoadShop", 2);
    }

    void LoadShop()
    {
        if (isGameWon)
        {
            if (GlobalControl.Instance.currentSceneIndex == 2)
            {
                GlobalControl.Instance.currentSceneIndex = 3;
            }
            else if (GlobalControl.Instance.currentSceneIndex == 3)
            {
                GlobalControl.Instance.currentSceneIndex = 4;
            }
        }
        SceneManager.LoadScene(shopIndex);
    }
}
