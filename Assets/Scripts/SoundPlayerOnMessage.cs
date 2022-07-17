using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;

public class SoundPlayerOnMessage : MonoBehaviour
{
    [SerializeField]
    AudioSource _audioSource;
    private void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.PlaySound, OnPlaySound);
    }

    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.PlaySound, OnPlaySound);
    }

    private void OnPlaySound(IMessage rMessage)
    {
        if (rMessage.Data is PlaySound)
        {
            PlaySound soundData = (PlaySound)rMessage.Data;

            _audioSource.outputAudioMixerGroup = soundData.MixerGroup;
            _audioSource.PlayOneShot(soundData.Clip, soundData.Volume);
        }
    }
}
