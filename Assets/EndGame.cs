using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static BattleSystem;
public class EndGame : MonoBehaviour
{
    public static bool endGame = false;
    public GameObject health;
    private Slider slider;
    private Canvas page;

    // Start is called before the first frame update
    void Start()
    {  
        slider = health.GetComponent<Slider>();
        page = GetComponent<Canvas>();
        endGame = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        page.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value <= 0 )//|| endBattle
        {
            GameEnded();
            endGame = true;
        }
    }

    void GameEnded()
    {
        page.enabled = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {

        Debug.Log("Restart called!");
        endGame = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
         endGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
