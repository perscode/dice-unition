using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class UiPulsate : MonoBehaviour
{
    public float Time = 1;
    public float SizeFactor = 1.05f;
    public Ease Ease = Ease.InOutSine;

    private Button _button;

    private Tween _tween;

    void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    void OnEnable()
    {
        _button.Select();
        Debug.Log($"Started pulsation for {name}");
        _tween = transform.DOScale(transform.localScale * SizeFactor, Time).SetEase(Ease).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    void OnDisable()
    {
        if (_tween != null)
        { 
            _tween.Kill();
        }
    }
}
