using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeMenuController : MonoBehaviour
{
    public RollDice MainDice;
    public Button ContinueButton;
    public UpgradeMenuState State = UpgradeMenuState.Idle;
    private Vector3 _continueButtonSize;
    private SlotAreaController[] _slotAreas;

    private void Awake()
    {
        _continueButtonSize = ContinueButton.transform.localScale;
        SlotController[] slotControllers = GetComponentsInChildren<SlotController>();
        foreach (var slot in slotControllers)
        {
            slot.UpgradeMenuController = this;
        }

        _slotAreas = GetComponentsInChildren<SlotAreaController>();
    }

    private void OnEnable()
    {
        StartCoroutine(RollDice());
        ContinueButton.gameObject.SetActive(false);
        MessageDispatcher.AddListener(Msg.UpgradeSelected, OnUpgradeSelected);
    }



    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.UpgradeSelected, OnUpgradeSelected);
    }

    private IEnumerator RollDice()
    {
        yield return new WaitForSecondsRealtime(1.2f);

        MainDice.gameObject.SetActive(true);
        MainDice.Roll();
    }

    public void OnRollComplete()
    {
        State = UpgradeMenuState.DiceRolled;
    }

    internal int GetCurrentDiceValue()
    {
        return MainDice.LatestResult;
    }

    private void OnUpgradeSelected(IMessage rMessage)
    {
        State = UpgradeMenuState.UpgradeSelected;
        ContinueButton.gameObject.SetActive(true);
        ContinueButton.transform.localScale = Vector3.zero;
        ContinueButton.transform.DOScale(_continueButtonSize, 0.5f).SetUpdate(true);
        MainDice.Shrink();
    }

    public void OnContinueClick()
    {
        ContinueButton.gameObject.SetActive(false);


        Debug.Log($"Broadcasting from {_slotAreas.Length} slotAreas");
        foreach (var slotArea in _slotAreas)
        {
            slotArea.BroadcastValue();
        }

        State = UpgradeMenuState.Idle;
    }
}


public enum UpgradeMenuState
{
    Idle,
    DiceRolled,
    UpgradeSelected,
}
