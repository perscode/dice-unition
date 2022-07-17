using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;

public class DebugFunctions : MonoBehaviour
{
    XpController _xpController;
    private void Awake()
    {
        _xpController = Object.FindObjectOfType<XpController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MessageDispatcher.SendMessage(Msg.PlayerWon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MessageDispatcher.SendMessage(Msg.PlayerLost);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            
            if (_xpController != null) _xpController.DoLevelUp();
            //MessageDispatcher.SendMessage(Msg.LevelUp);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            if (_xpController != null) _xpController.SetLastLevel();
        }
    }
}
