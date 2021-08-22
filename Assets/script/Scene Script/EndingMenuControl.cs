using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenuControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
