using System.Collections.Generic;
using UnityEngine;

public class SoundManagers : MonoBehaviour
{
    public enum SoundType
    {
        SOUND_SFX,
        SOUND_MUSIC
    }

    public static SoundManagers Instance { get; private set; } // Static object of the class.

    private Dictionary<string, AudioClip> sfxDictionarys = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> musicDictionarys = new Dictionary<string, AudioClip>();
    private AudioSource sfxSources;
    private AudioSource musicSources;
    private float volumeMasters = 1.0f;
    private float volumeSfxs = 1.0f;
    private float volumeMusics = 0.25f;

    // Initialize the SoundManager. I just put this functionality here instead of in the static constructor.
    public void Initialize(GameObject go)
    {
        sfxSources = go.AddComponent<AudioSource>();
        sfxSources.volume = volumeSfxs * volumeMasters;
        
        musicSources = go.AddComponent<AudioSource>();
        musicSources.volume = volumeMusics * volumeMasters;
        musicSources.loop = true;
    }

    // Add a sound to the dictionary.
    public void AddSound(string soundKey, AudioClip audioClip, SoundType soundType)
    {
        Dictionary<string, AudioClip> targetDictionary = GetDictionaryByType(soundType);

        if (!targetDictionary.ContainsKey(soundKey))
        {
            targetDictionary.Add(soundKey, audioClip);
        }
        else
        {
            Debug.LogWarning("Sound key " + soundKey + " already exists in the " + soundType + " dictionary.");
        }
    }

    // Play a sound by key interface.
    public void PlaySound(string soundKey)
    {
        Play(soundKey, SoundType.SOUND_SFX);
    }

    public void PlayLoopedSound(string soundKey)
    {
        if (sfxSources.isPlaying) return;
        if (sfxDictionarys.ContainsKey(soundKey))
        {
            sfxSources.clip = sfxDictionarys[soundKey];
            sfxSources.loop = true;
            sfxSources.Play();
        }
    }

    public void StopLoopedSound()
    {
        sfxSources.Stop();
        sfxSources.clip = null;
        sfxSources.loop = false;
    }

    // Play music by key interface.
    public void PlayMusics(string soundKey)
    {
        musicSources.Stop();
        musicSources.clip = musicDictionarys[soundKey];
        musicSources.Play();
    }

    // Play utility.
    private void Play(string soundKey, SoundType soundType)
    {
        Dictionary<string, AudioClip> targetDictionary;
        AudioSource targetSource;

        SetTargetsByTypes(soundType, out targetDictionary, out targetSource);

        if (targetDictionary.ContainsKey(soundKey))
        {
            targetSource.PlayOneShot(targetDictionary[soundKey]);
        }
        else
        {
            Debug.LogWarning("Sound key " + soundKey + " not found in the " + soundType + " dictionary.");
        }
    }

    public void SetVolumes(float volume, SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                volumeSfxs = volume;
                sfxSources.volume = volumeSfxs * volumeMasters;
                break;
            case SoundType.SOUND_MUSIC:
                volumeMusics = volume;
                musicSources.volume = volumeMusics * volumeMasters;
                break;
            default:
                Debug.LogError("Unknown sound type: " + soundType);
                break;
        }
    }

    public void SetMasterVolumes(float volume)
    {
        volumeMasters = volume;
        sfxSources.volume = volumeSfxs * volumeMasters;
        musicSources.volume = volumeMusics * volumeMasters;
    }

    private void SetTargetsByTypes(SoundType soundType, out Dictionary<string, AudioClip> targetDictionary, out AudioSource targetSource)
    {
        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                targetDictionary = sfxDictionarys;
                targetSource = sfxSources;
                break;
            case SoundType.SOUND_MUSIC:
                targetDictionary = musicDictionarys;
                targetSource = musicSources;
                break;
            default:
                Debug.LogError("Unknown sound type: " + soundType);
                targetDictionary = null;
                targetSource = null;
                break;
        }
    }
    private Dictionary<string, AudioClip> GetDictionaryByType(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                return sfxDictionarys;
            case SoundType.SOUND_MUSIC:
                return musicDictionarys;
            default:
                Debug.LogError("Unknown sound type: " + soundType);
                return null;
        }
    }

    private AudioSource GetSourceByTypes(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                return sfxSources;
            case SoundType.SOUND_MUSIC:
                return musicSources;
            default:
                Debug.LogError("Unknown sound type: " + soundType);
                return null;
        }
    }
}