using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.ootii.Messages;
using System;

public class XpController : MonoBehaviour
{
    public RectTransform rectTransform;
    public Image experienceBarImage;
    public int LevelToWin = 10;
    private int experience;
    public static int level;
    private float experienceToNextLevel;
    public int DebugStartLevel = 0;

    void Awake()
    {
        level = DebugStartLevel;
        experience = 0;
        experienceToNextLevel = 3;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        float percentage = (float)experience / experienceToNextLevel;

        SetExperienceBarSize(percentage * 100);
        if (experience >= experienceToNextLevel)
        {
            DoLevelUp();
        }
    }

    public void DoLevelUp()
    {
        level++;
        experience = 0;
        experienceToNextLevel *= 1.2f;
        if (level < LevelToWin)
        {
            MessageDispatcher.SendMessage(Msg.LevelUp);
        }
        else
        {
            MessageDispatcher.SendMessage(Msg.PlayerWon);
        }
        SetExperienceBarSize(0f);
    }

    internal void SetLastLevel()
    {
        level = LevelToWin - 2;
        DoLevelUp();
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        rectTransform.sizeDelta = new Vector2((float)(experienceNormalized * 10), 0);
    }

    private void OnGainXp(IMessage rMessage)
    {
        AddExperience(1);
    }

    public void OnEnable()
    {
        if (rectTransform != null) { 
            SetExperienceBarSize(0f);
        }
        MessageDispatcher.AddListener(Msg.GainXP, OnGainXp);
    }


    public void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.GainXP, OnGainXp);
    }
}
