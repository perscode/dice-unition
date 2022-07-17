using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOnStart : MonoBehaviour, PlaySound
{
    [SerializeField]
    private AudioClip clip;
    [SerializeField, Range(0, 1)]
    private float volume = 1;
    [SerializeField]
    private AudioMixerGroup mixerGroup;

    public AudioClip Clip { get => clip; set => clip = value; }
    public float Volume { get => volume; set => volume = value; }
    public AudioMixerGroup MixerGroup { get => mixerGroup; set => mixerGroup = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (Clip != null && MixerGroup != null)
        {
            MessageDispatcher.SendMessage(this, Msg.PlaySound, this, 0);
        }
    }
}

public interface PlaySound
{
    public AudioClip Clip { get; }
    public float Volume { get; }
    public AudioMixerGroup MixerGroup { get; }
}

