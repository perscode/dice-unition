using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.ootii.Messages;

public class XpController : MonoBehaviour
{
    public RectTransform rectTransform;
    public Image experienceBarImage;
    private int experience;
    private int level;
    private int experienceToNextLevel;

    public XpController ()
    {
        level = 0;
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
            level++;
            experience -= experienceToNextLevel;
            experienceToNextLevel *= 2;
            MessageDispatcher.SendMessage(Msg.LevelUp);
            SetExperienceBarSize(0f);
        }
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        rectTransform.sizeDelta = new Vector2((float)(experienceNormalized * 4.5), 0);
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
