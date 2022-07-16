using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private List<StringAudioPair> _clipList;
    public float AUDIBLE_DISTANCE = 15;

    private GameObject _playerTransform;
    private Dictionary<string, AudioClip> _clipDict;

    public void Awake()
    {
        _clipDict = new Dictionary<string, AudioClip>();
        _playerTransform = GameObject.Find("Player");

        foreach (StringAudioPair pair in _clipList)
        {
            _clipDict.Add(pair.key, pair.clip);
        }
    }

    public void TryPlayClip(string audioKey)
    {
        PlayClip(audioKey);
    }

    public void PlayClip(string audioKey)
    {
        _source.PlayOneShot(_clipDict[audioKey]);
    }

    public void PlayClipPlayerProximity(string audioKey)
    {
        float distanceToPlayer = Vector3.Distance(_playerTransform.transform.position, transform.position);
        if (distanceToPlayer < AUDIBLE_DISTANCE) {
            _source.clip = _clipDict[audioKey];
            _source.volume = 0.2f; // TODO: make it based on distance to player instead.
            _source.PlayOneShot(_clipDict[audioKey]);
        }
    }

    public void LoopClip(string audioKey)
    {
        _source.clip = _clipDict[audioKey];
        _source.loop = true;
        _source.Play();
    }

    public void StopAudio()
    {
        _source.Stop();
    }
}

class StringAudioPair
{
    public string key;
    public AudioClip clip;
}

