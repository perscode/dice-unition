using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;

public class UpgradeFromMessage : MonoBehaviour
{
    [SerializeField]
    private string _message;

    [SerializeField]
    private MonoBehaviour upgradableBehaviour;

    private IUpgradable upgradable;

    private void Awake()
    {
        if (upgradableBehaviour is IUpgradable)
        {
            upgradable = (IUpgradable)upgradableBehaviour;
        }
    }

    private void OnEnable()
    {
        MessageDispatcher.AddListener(_message, OnUpgrade);
    }

    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(_message, OnUpgrade);
    }

    private void OnUpgrade(IMessage rMessage)
    {
        if (upgradable != null && rMessage.Data is float)
        {
            float value = (float)rMessage.Data;
            upgradable.SetValue(value);
        }
    }
}
