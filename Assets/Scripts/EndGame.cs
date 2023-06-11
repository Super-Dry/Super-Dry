using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static BattleSystem;

public class EndGame : MonoBehaviour
{
    public GameObject health;
    private Slider slider;
    private Canvas page;

    // Start is called before the first frame update
    void Start()
    {  
        slider = health.GetComponent<Slider>();
        page = GetComponent<Canvas>();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        page.enabled = false;
    }

    public void GameEnded()
    {
        Time.timeScale = 0.25f;
        StartCoroutine(GameEndedRoutine());
    }

    IEnumerator GameEndedRoutine()
    {
        yield return new WaitForSeconds(1f);
        page.enabled = true;
        Time.timeScale = 0f;
        AudioSource audio;
        audio = GetComponent<AudioSource>();
        audio.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return null;
    }

    public void Restart()
    {
        Debug.Log("Restart called!");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
