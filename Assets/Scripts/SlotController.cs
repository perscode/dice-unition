using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    public UpgradeMenuController UpgradeMenuController;
    public RollDice Dice;
    public GameObject Button;
    public int CurrentValue;

    public void SetDice()
    {
        if (UpgradeMenuController == null) return;
        if (UpgradeMenuController.State != UpgradeMenuState.DiceRolled) return;

        CurrentValue = UpgradeMenuController.GetCurrentDiceValue();
        Dice.SetPose(CurrentValue);
        Dice.gameObject.SetActive(true);
        Button.SetActive(false);
        MessageDispatcher.SendMessage(Msg.UpgradeSelected);
    }
}
