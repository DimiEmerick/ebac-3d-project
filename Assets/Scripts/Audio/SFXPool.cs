using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Ebac.Core.Singleton;

public class SFXPool : Singleton<SFXPool>
{
    public AudioMixerGroup sfxMixer;
    public int poolSize = 10;

    private List<AudioSource> _audioSources;
    private int _index = 0;

    protected override void Awake()
    {
        base.Awake();
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSources = new List<AudioSource>();
        for(int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
            _audioSources[i].outputAudioMixerGroup = sfxMixer;
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        _audioSources.Add(go.AddComponent<AudioSource>());
    }

    public void Play(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE) return;
        var sfx = SoundManager.Instance.GetSFXByType(sfxType);
        _audioSources[_index].clip = sfx.audioClip;
        if (sfxType == SFXType.SHOOT_01)
            _audioSources[_index].volume = .25f;
        else
            _audioSources[_index].volume = 1;
        if (sfxType == SFXType.SHOOT_02)
            _audioSources[_index].spatialBlend = .9f;
        else
            _audioSources[_index].spatialBlend = 0;
        _audioSources[_index].Play();
        _index++;
        if (_index >= _audioSources.Count) _index = 0;
    }
}
