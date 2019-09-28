using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 사운드 목록
public enum AudioClipName
{
    Jump,
    doorDown,
    crashRok,
    doorStop,
    rokCrash,
    SuckSuck
}

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] audioClips;
    public Resources resources;

    public void SoundPlay(int ClipNum)
    {
        SoundPlayProcessor(ClipNum);
    }

    public void SoundPlayLoop(int ClipNum)
    {
        SoundPlayProcessorLoop(ClipNum);
    }

    public void StopSoundPlay()
    {
        audioSource.loop = false;
        audioSource.Stop();
        Debug.Log("사운드 스탑");
    }

    private void SoundPlayProcessor(int ClipNum)
    {
        Debug.LogFormat("사운드 클립 {0}", ClipNum);
        var tmpClip = audioClips[ClipNum];
        audioSource.loop = false;
        audioSource.PlayOneShot(tmpClip);
    }

    private void SoundPlayProcessorLoop(int clipNum)
    {
        Debug.LogFormat("사운드 클립 {0}", clipNum);
        var tmpClip = audioClips[clipNum];
        audioSource.loop = true;
        audioSource.clip = tmpClip;
        audioSource.Play();
    }
}