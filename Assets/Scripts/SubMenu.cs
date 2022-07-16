using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SubMenu : MonoBehaviour
{
    public CanvasGroup _canvasGroup;
    private Tween _fadeTween;


    public void Activate()
    {
        _canvasGroup.alpha = 0;
        gameObject.SetActive(true);
        _fadeTween = _canvasGroup.DOFade(1, 1).SetUpdate(true);
    }

    public void Deactivate(bool fade = false)
    {
        if (fade == true && gameObject.activeSelf == true)
        {
            _fadeTween = _canvasGroup.DOFade(0, 0.25f).SetUpdate(true);
            _fadeTween.OnComplete(() => { gameObject.SetActive(false); });
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnDisable()
    {
        if (_fadeTween != null) _fadeTween.Kill();
        _canvasGroup.alpha = 0;
    }
}
