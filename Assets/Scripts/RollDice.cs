using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RollDice : MonoBehaviour
{
    public List<Vector3> DirectionsForward = new List<Vector3>();
    public List<Vector3> DirectionsUp = new List<Vector3>();
    public int LatestResult = -1;
    public UnityEvent RollComplete;
    private Vector3 _originalSize;

    private void Awake()
    {
        _originalSize = transform.localScale;
    }

#if UNITY_EDITOR
    [Button("Record pose")]
    void Record()
    {
        Undo.RecordObject(this, "Save");
        DirectionsForward.Add(transform.forward);
        DirectionsUp.Add(transform.up);
    }
#endif

    [Button("Roll dice"), DisableInEditorMode]
    public int Roll()
    {
        transform.localScale = Vector3.zero;
        int result = Random.Range(1, 7);
        Sequence sequence = DOTween.Sequence().SetUpdate(true);
        float time = 0.5f;
        sequence.Insert(0 * time, RandRotationTween(time));
        sequence.Insert(0 * time, BounceTween(time));
        sequence.Insert(1 * time, RandRotationTween(time));
        sequence.Insert(1 * time, BounceTween(time));
        sequence.Insert(2 * time, ValueRotationTween(result, time));
        sequence.Insert(2 * time, BounceTween(time));

        sequence.OnComplete(() => { RollComplete?.Invoke(); });

        LatestResult = result;

        return result;
    }

    [Button("Set pose")]
    public void SetPose(int value)
    {
        int index = value - 1;
        if (index >= 0 && index < DirectionsForward.Count && index < DirectionsUp.Count)
        {
            Debug.Log($"Set rotation {index}");
            Quaternion rotation = Quaternion.LookRotation(DirectionsForward[index], DirectionsUp[index]);
            transform.rotation = rotation;
        }
    }

    Tween RandRotationTween(float time = 0.5f)
    {
        return transform.DORotate(new Vector3(RandDegree(), RandDegree(), RandDegree()), time, RotateMode.FastBeyond360).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    float RandDegree()
    {
        return Random.Range(0, 360);
    }

    Tween ValueRotationTween(int value, float time = 0.5f)
    {
        int index = value - 1;
        if (index >= 0 && index < DirectionsForward.Count && index < DirectionsUp.Count)
        {
            Quaternion rotation = Quaternion.LookRotation(DirectionsForward[index], DirectionsUp[index]);
            return transform.DORotate(rotation.eulerAngles, time).SetUpdate(true);
        }
        else
        {
            return null;
        }
    }

    Sequence BounceTween(float time = 0.5f)
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(true);

        sequence.Insert(0, transform.DOScale(_originalSize * 1.3f, time / 2).SetUpdate(true));
        sequence.Insert(time/2, transform.DOScale(_originalSize, time / 2).SetUpdate(true));

        return sequence;
    }

    public void Shrink()
    {
        transform.DOScale(Vector3.zero, 0.3f).SetUpdate(true).OnComplete(() => { gameObject.SetActive(false); });
    }
}
