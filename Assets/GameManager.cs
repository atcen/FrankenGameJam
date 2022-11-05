using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /**
     * Gets all Levels from the LevelManager and loads the next one
     */
    public void LoadNextLevel() {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;
        if (nextLevel >= SceneManager.sceneCountInBuildSettings) {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }
}
