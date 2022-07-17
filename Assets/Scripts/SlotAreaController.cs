using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotAreaController : MonoBehaviour
{
    public string Message;
    private List<SlotController> _slots;

    private void Awake()
    {
        _slots = new List<SlotController>(GetComponentsInChildren<SlotController>());
    }

    public void BroadcastValue()
    {
        float value = 0;

        foreach (var slot in _slots)
        {
            value += slot.CurrentValue;
        }

        Debug.Log($"Sending [{Message}]: {value}");
        MessageDispatcher.SendMessage(this, Message, value, 0);
    }
}
