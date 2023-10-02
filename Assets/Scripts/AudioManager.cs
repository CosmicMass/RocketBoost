using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    public AudioSource oneShotSource;

    public AudioSource loopSource;

    [Header("Audio Clips")]
    public AudioClip engineThrust;
    public AudioClip success;
    public AudioClip crash;
    public AudioClip collect;

    public bool isEngineThrustSoundPlaying = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlayAudioSource(AudioSource asource, AudioClip aclip)
    {
        asource.clip = aclip;
        asource.Play();
    }
    public void CollectPlay()
    {
        PlayAudioSource(oneShotSource, collect);
    }

    public void EngineThrustPlay()
    {
        PlayAudioSource(loopSource, engineThrust);
    }

    public void StopAudioSource(AudioSource bsource)
    {
        bsource.Stop();
    }

    public void ThrustStop()
    {
        StopAudioSource(loopSource);
    }

    public void SuccessPlay()
    {
        PlayAudioSource(oneShotSource, success);
    }

    public void CrashPlay()
    {
        PlayAudioSource(oneShotSource, crash);
    }
}
