using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    [SerializeField] float timeTillDestruction = 20f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("NextSceneGo", timeTillDestruction);
    }

    void NextSceneGo()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
