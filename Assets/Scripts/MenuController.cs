using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    SubMenu _mainMenu, _winScreen, _loseScreen, _upgradeScreen;

    PauseManager _pauseManager;

    Dictionary<string, SubMenu> _subMenuDict;
    // Start is called before the first frame update
    void Awake()
    {
        _pauseManager = UnityEngine.Object.FindObjectOfType<PauseManager>();

        _subMenuDict = new Dictionary<string, SubMenu>();
        _subMenuDict.Add("MainMenu", _mainMenu);
        _subMenuDict.Add("WinScreen", _winScreen);
        _subMenuDict.Add("LooseScreen", _loseScreen);
        _subMenuDict.Add("UpgradeScreen", _upgradeScreen);
    }

    public void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.PlayerWon, OnPlayerWon);
        MessageDispatcher.AddListener(Msg.PlayerLost, OnPlayerLost);
        MessageDispatcher.AddListener(Msg.LevelUp, OnLevelUp);
    }


    public void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.PlayerWon, OnPlayerWon);
        MessageDispatcher.RemoveListener(Msg.PlayerLost, OnPlayerLost);
        MessageDispatcher.RemoveListener(Msg.LevelUp, OnLevelUp);
    }

    private void OnPlayerWon(IMessage rMessage)
    {
        ActivateSubMenu("WinScreen");
    }

    private void OnPlayerLost(IMessage rMessage)
    {
        ActivateSubMenu("LooseScreen");
    }

    private void OnLevelUp(IMessage rMessage)
    {
        ActivateSubMenu("UpgradeScreen");
    }

    private void ActivateSubMenu(string key)
    {
        if (_pauseManager != null)
        {
            _pauseManager.Pause();
        }

        foreach (var item in _subMenuDict)
        {
            if (item.Key == key)
            {
                item.Value.Activate();
            }
            else
            {
                item.Value.Deactivate();
            }
        }
    }

    public void ContinueGame()
    {
        if (_pauseManager != null)
        {
            _pauseManager.Resume();
        }

        DeactivateAll();
    }

    private void DeactivateAll()
    {
        foreach (var item in _subMenuDict)
        {
            item.Value.Deactivate(true);
        }
    }
}

