using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    [Header("Audio Sources")]
    [SerializeField] private AudioSource mainAudioSource;

    [Header("Audio Collection")]
    [SerializeField] private AudioCollection audioCollection;
    private Dictionary<AudioName, AudioData> audioDataLookup;
    [SerializeField] private Rigidbody boatRigidbody;

    private void Start() => InitializeAudioData();
    private void InitializeAudioData()
    {
        audioDataLookup = new Dictionary<AudioName, AudioData>();

        foreach (var audio in audioCollection.audios)
        {
            if (!audioDataLookup.ContainsKey(audio.audioName))
                audioDataLookup.Add(audio.audioName, audio);
        }
    }

    public void PlayAudio(AudioName audioName)
    {
        if (mainAudioSource.isPlaying) mainAudioSource.Stop();
        PlayAudioClip(audioName, mainAudioSource);
    }
    private void PlayAudioClip(AudioName audioName, AudioSource audioSource)
    {
        if (audioDataLookup.TryGetValue(audioName, out AudioData audioData) && audioSource)
        {
            if (audioSource.isPlaying && audioSource.clip == audioData.audioClip) return;

            audioSource.clip = audioData.audioClip;
            if (audioName != AudioName.COLLISION) audioSource.volume = audioData.volume;
            else audioSource.volume = Mathf.Clamp01(boatRigidbody.linearVelocity.magnitude / 10f);
            audioSource.loop = audioData.loop;
            audioSource.Play();
        }
    }
}