using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToIntro : MonoBehaviour
{
public void GoToNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by adding 1 to the current scene index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
