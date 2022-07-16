using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class UiPulsate : MonoBehaviour
{
    public float Time = 1;
    public float SizeFactor = 1.05f;
    public Ease Ease = Ease.InOutSine;

    private Tween _tween;
    
    void OnEnable()
    {
        Debug.Log("Yoyoing!");
        _tween = transform.DOScale(transform.localScale * SizeFactor, Time).SetEase(Ease).SetLoops(-1, LoopType.Yoyo);
    }

    void OnDisable()
    {
        Debug.Log("Stopping!");
        _tween.Kill();
    }
}
