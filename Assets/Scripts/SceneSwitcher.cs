using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.ootii.Messages;

public class SceneSwitcher : MonoBehaviour
{
    public string _mainGameLevel;
    public string _messageOnStart;

    public void Start()
    {
        StartCoroutine(StartMusicSoon());
    }

    IEnumerator StartMusicSoon()
    {
        yield return new WaitForSeconds(0.5f);
        MessageDispatcher.SendMessage(_messageOnStart); 
    }

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
