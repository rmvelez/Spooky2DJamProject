using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{
    private static SoundBank instance;
    public static SoundBank GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SoundBank>();
        }
        return instance;
    }


    public List<AudioClip> ghostAttackAudioClips;
    public List<AudioClip> ghostSpawnAudioClips;
    public List<AudioClip> ghostDieAudioClips;

    public List<AudioClip> skeletonAttackAudioClips;
    public List<AudioClip> skeletonSpawnAudioClips;
    public List<AudioClip> skeletonDieAudioClips;

    public List<AudioClip> indoorFootstepAudioClips;
    public List<AudioClip> outdoorFootstepAudioClips;

    public List<AudioClip> gunshotAudioClips;
    public List<AudioClip> newItemAudioClips;
    public List<AudioClip> itemSwitchAudioClips;

    public List<AudioClip> playerDeathAudioClips;
    public List<AudioClip> PlayerHurtAudioClips;

    public List<AudioClip> BathtubDrainAudioClips;
    public List<AudioClip> DoorUnlockAudioClips;
    public List<AudioClip> TVTunOnAudioClips;

    public List<AudioClip> dashAudioClips;

    public List<AudioClip> WinAudioClips;
    public List<AudioClip> LoseAudioClips;


    /// <summary>
    /// Returns a random audio clip from a list of audio clips
    /// </summary>
    /// <param name="audioClips"></param>
    /// <returns></returns>
    private static AudioClip GetRandomAudioClip(List<AudioClip> audioClips)
    {
        if (audioClips == null || audioClips.Count == 0) return null;

        return audioClips[Random.Range(0, audioClips.Count)];
    }

    /// <summary>
    /// Plays a random audio clip from a list of clips on a given audioSource
    /// </summary>
    /// <param name="audioClips">The audioClip to play. E.g. pick any of the public Sound collections attached to the soundbank like this: SoundBank.GetInstance()...audioClips</param>
    /// <param name="audioSource">audioSource on which the clip is played (provided by and probably attached to the caller of this method)</param>
    public static void PlayAudioClip(List<AudioClip> audioClips, AudioSource audioSource)
    {
        audioSource.clip = GetRandomAudioClip(audioClips);
        audioSource.Play();
    }
}
