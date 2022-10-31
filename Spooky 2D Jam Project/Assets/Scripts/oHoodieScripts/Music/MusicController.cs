using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    #region Singleton
    private static MusicController instance;
    public static MusicController GetInstance()
    {
        if (instance == null)
        {
            GameObject musicControllerGameObject = GameObject.Find("MusicController");
            if (musicControllerGameObject != null) instance = musicControllerGameObject.GetComponent<MusicController>();
            if (instance == null) Debug.LogError("MusicController Prefab is missing in the scene!");
        }
        return instance;
    }
    #endregion

    public float fadeTime;

    private int enemyCount = 0;
    [SerializeField] private AudioSource interiorPeaceAudioSource;
    [SerializeField] private AudioSource interiorWarAudioSource;
    [SerializeField] private AudioSource exteriorPeaceAudioSource;
    [SerializeField] private AudioSource exteriorWarAudioSource;
    [SerializeField] private AudioSource bossAudioSource;

    private float currentFadeTime;
    private AudioSource activeAudioSource;
    private MusicTrigger activeMusicTrigger;

    private void Start()
    {
        activeAudioSource = interiorPeaceAudioSource;
    }

    private void Update()
    {
        SetVolumeForAudioSource(interiorPeaceAudioSource);
        SetVolumeForAudioSource(interiorWarAudioSource);
        SetVolumeForAudioSource(exteriorPeaceAudioSource);
        SetVolumeForAudioSource(exteriorWarAudioSource);
        SetVolumeForAudioSource(bossAudioSource);
    }

    public void AddEnemy()
    {
        enemyCount++;
        PlayMusicFromTrigger(activeMusicTrigger);
    }
    public void RemoveEnemy()
    {
        enemyCount--;
        PlayMusicFromTrigger(activeMusicTrigger);
    }

    private void SetVolumeForAudioSource(AudioSource audioSource)
    {
        if (activeAudioSource == audioSource && audioSource.volume == 1) return;
        if (activeAudioSource != audioSource && audioSource.volume == 0) return;

        float targetVolume = 1;
        if (activeAudioSource == audioSource)
        {
            targetVolume = audioSource.volume + fadeTime * Time.deltaTime;
            if (targetVolume > 1) targetVolume = 1;
        }
        else if (activeAudioSource != audioSource)
        {
            targetVolume = audioSource.volume - fadeTime * Time.deltaTime;
            if (targetVolume < 0) targetVolume = 0;

        }

        audioSource.volume = targetVolume;
    }

    public void PlayMusicFromTrigger(MusicTrigger musicTrigger)
    {
        activeMusicTrigger = musicTrigger;

        if (enemyCount == 0)
        {
            activeAudioSource = musicTrigger.peaceAudioSource;
        }
        else
        {
            activeAudioSource = musicTrigger.warAudioSource;
        }
    }
}
