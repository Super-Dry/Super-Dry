using UnityEngine;
using UnityEngine.SceneManagement;

public class playButton : MonoBehaviour
{
    public void PLayGame()
    {
        SceneManager.LoadScene("GameMap_battle");
        Time.timeScale = 1f;
        pauseGame.isPaused = false;
    }
}
