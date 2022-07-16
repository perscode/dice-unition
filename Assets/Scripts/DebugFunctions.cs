using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;

public class DebugFunctions : MonoBehaviour
{
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
            MessageDispatcher.SendMessage(Msg.LevelUp);
        }
    }
}
