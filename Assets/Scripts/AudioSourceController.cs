using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using DG.Tweening;

public class AudioSourceController
{
    public MonoBehaviour _parentBehavour;
    public AudioClip MainClip;
    public AudioClip Intro;
    public AudioClip Outro;
    public float FadeInTime = 1;
    public float FadeOutTime = 1;
    AudioSource _source;
    Coroutine _waitingToEnd;
    private bool _isPlaying = false;
    private Tween currentTween;

    public bool IsPlaying => _isPlaying;

    public AudioSourceController(MonoBehaviour parentBehavour, AudioSource source, AudioClip mainClip, AudioMixerGroup mixerGroup)
    {
        _parentBehavour = parentBehavour;
        _source = source;
        _source.outputAudioMixerGroup = mixerGroup;
        MainClip = mainClip;

        CleanUp();
    }

    public void PlayQueue(Queue<AudioClip> playQueue)
    {
        if (playQueue.Count == 0)
        {
            _isPlaying = false;
            return;
        }
        else
        {
            _isPlaying = true;
        }

        _source.clip = playQueue.Dequeue();
        _source.Play();

        if (_source.clip == MainClip && playQueue.Count == 0)
        {
            _source.loop = true;
            return;
        }

        if (playQueue.Count > 0)
        {
            _waitingToEnd = _parentBehavour.StartCoroutine(WaitForClipToEndCo(_source, () =>
            {
                PlayQueue(playQueue);
            }));
        }
        else
        {
            _waitingToEnd = _parentBehavour.StartCoroutine(WaitForClipToEndCo(_source, () =>
            {
                _isPlaying = false;
            }));
        }
    }

    public void Play()
    {
        if (!_isPlaying)
        {
            Queue<AudioClip> queue = GenerateQueue();
            PlayQueue(queue);
        }
    }

    private Queue<AudioClip> GenerateQueue()
    {
        Queue<AudioClip> queue = new Queue<AudioClip>();
        EnqueIfExists(queue, Intro);
        EnqueIfExists(queue, MainClip);
        EnqueIfExists(queue, Outro);

        return queue;
    }

    private void EnqueIfExists(Queue<AudioClip> queue, AudioClip clip)
    {
        if (clip != null)
        {
            queue.Enqueue(clip);
        }
    }

    private void CleanUp()
    {
        if (_waitingToEnd != null)
        {
            _parentBehavour.StopCoroutine(_waitingToEnd);
        }
        if (currentTween != null)
        {
            currentTween.Kill();
        }
        if (_source.isPlaying)
        {
            _source.Stop();
        }
        _source.loop = false;
        _source.volume = 0;
    }

    public void FadeOut()
    {
        CleanUp();

        if (IsPlaying)
        {
            currentTween = _source.DOFade(0, FadeOutTime).SetUpdate(true).OnComplete(() =>
            {
                _source.Stop();
            });
            _isPlaying = false;
        }
    }

    public void FadeIn()
    {
        if (!IsPlaying)
        {
            CleanUp();
            Play();
            currentTween = _source.DOFade(1, 1).SetUpdate(true);
        }
    }

    IEnumerator WaitForClipToEndCo(AudioSource source, Action actionOnEnd)
    {
        while (source.isPlaying == true)
        {
            yield return 0;
        }

        actionOnEnd();
    }
}
