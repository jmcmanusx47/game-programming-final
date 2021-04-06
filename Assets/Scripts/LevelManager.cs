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
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelLost()
    {
        isGameOver = true;
        isGameWon = false;
        gameText.text = "YOU LOSE!";
        gameText.gameObject.SetActive(true);

        //Camera.main.GetComponent<AudioSource>().pitch = 1;
        //AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("LoadShop", 2);
    }

    public void LevelWon()
    {
        isGameOver = true;
        isGameWon = true;
        gameText.text = "YOU WIN!";
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
        }
        SceneManager.LoadScene(shopIndex);
    }
}
