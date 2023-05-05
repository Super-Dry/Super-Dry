using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionMenu");
    }
}
