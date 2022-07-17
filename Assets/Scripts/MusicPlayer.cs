using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

public class MusicPlayer : SingletonBase<MusicPlayer>
{
    [SerializeField]
    private AudioClip _defaultMusic, _actionStartMusic, _actionLoopMusic, _menuMusic, _winClip, _looseClip;
    [SerializeField]
    private AudioMixerGroup _mixerGroup;
    [SerializeField]
    private AudioSource _oneShotPlayer;

    private AudioSourceController _defaultPlayer;
    private AudioSourceController _actionPlayer;
    private AudioSourceController _menuPlayer;

    private List<AudioSourceController> _sourceControllers;

    private Transform _mainCameraTransform;
    private Transform _transformSelf;

    // Start is called before the first frame update
    void Start()
    {
        _transformSelf = GetComponent<Transform>();

        _defaultPlayer = new AudioSourceController(this, gameObject.AddComponent<AudioSource>(), _defaultMusic, _mixerGroup);

        _actionPlayer = new AudioSourceController(this, gameObject.AddComponent<AudioSource>(), _actionLoopMusic, _mixerGroup);
        _actionPlayer.Intro = _actionStartMusic;
        _actionPlayer.FadeOutTime = 2;
        _menuPlayer = new AudioSourceController(this, gameObject.AddComponent<AudioSource>(), _menuMusic, _mixerGroup);
        _actionPlayer.FadeInTime = 2;

        _sourceControllers = new List<AudioSourceController>();
        _sourceControllers.Add(_defaultPlayer);
        _sourceControllers.Add(_actionPlayer);
        _sourceControllers.Add(_menuPlayer);
    }

    private void Update()
    {
        SetToCameraPosition();
    }

    private void SetToCameraPosition()
    {
        if (_mainCameraTransform != null)
        {
            _transformSelf.position = _mainCameraTransform.position;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        //        MessageDispatcher.AddListener(Msg.AlarmStarted, OnAlarmStarted);
        //        MessageDispatcher.AddListener(Msg.AlarmStopped, OnAlarmStopped);
        //MessageDispatcher.AddListener(Msg.PlayerWon, PlayMenuMusic);
        MessageDispatcher.AddListener(Msg.PlayerWon, PlayWinSound);
        MessageDispatcher.AddListener(Msg.LevelUp, PlayWinSound);
        //MessageDispatcher.AddListener(Msg.PlayerLost, PlayMenuMusic);
        MessageDispatcher.AddListener(Msg.PlayerLost, PlayLostSound);
        MessageDispatcher.AddListener(Msg.LevelStarted, OnLevelStarted);
        MessageDispatcher.AddListener(Msg.StartMenuMusic, PlayMenuMusic);
    }

    private void PlayLostSound(IMessage rMessage)
    {
        _oneShotPlayer.PlayOneShot(_looseClip);
    }

    private void PlayWinSound(IMessage rMessage)
    {
        _oneShotPlayer.PlayOneShot(_winClip);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        _mainCameraTransform = Camera.main?.transform;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        //        MessageDispatcher.RemoveListener(Msg.AlarmStarted, OnAlarmStarted);
        //        MessageDispatcher.RemoveListener(Msg.AlarmStopped, OnAlarmStopped);
        //MessageDispatcher.RemoveListener(Msg.PlayerLost, PlayMenuMusic);
        MessageDispatcher.RemoveListener(Msg.PlayerLost, PlayLostSound);
        MessageDispatcher.RemoveListener(Msg.LevelStarted, OnLevelStarted);
        MessageDispatcher.RemoveListener(Msg.LevelUp, PlayWinSound);
        //MessageDispatcher.RemoveListener(Msg.PlayerWon, PlayMenuMusic);
        MessageDispatcher.RemoveListener(Msg.PlayerWon, PlayWinSound);
        MessageDispatcher.RemoveListener(Msg.StartMenuMusic, PlayMenuMusic);

    }

//    [Button("Level Started")]
    private void OnLevelStarted(IMessage rMessage)
    {
        Debug.Log("Level started music");
        FadeToPlayer(_defaultPlayer);
    }

//    [Button("Level Ended")]
    private void PlayMenuMusic(IMessage rMessage)
    {
        Debug.Log("Start menu music");
        FadeToPlayer(_menuPlayer);
    }

//    [Button("Alarm Started")]
    private void OnAlarmStarted(IMessage rMessage)
    {
        FadeToPlayer(_actionPlayer);
    }

//    [Button("Alarm Stopped")]
    private void OnAlarmStopped(IMessage rMessage)
    {
        FadeToPlayer(_defaultPlayer);
    }

    void FadeToPlayer(AudioSourceController sourceController)
    {
        FadeOutAllExcept(sourceController);
        sourceController.FadeIn();
    }

    void FadeOutAllExcept(AudioSourceController sourceControllerToKeep)
    {
        foreach (AudioSourceController ctrlr in _sourceControllers)
        {
            if (ctrlr != sourceControllerToKeep)
            {
                ctrlr.FadeOut();
            }
        }

    }

}

