using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string _mainGameLevel;

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void StartGameLevel()
    {
        Debug.Log($"Starting {_mainGameLevel}");
        SceneManager.LoadScene(_mainGameLevel);
    }
}
