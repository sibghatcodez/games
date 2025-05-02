using Unity.VisualScripting;
using UnityEngine;

public class ThemeMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] themeSongs;
    [SerializeField] private AudioSource audioSource;
    private int total_theme_songs;

    private void OnEnable()
    {
        total_theme_songs = themeSongs.Length;
        PlayRandomThemeSong();
    }
    private void PlayRandomThemeSong()
    {
        int randIndex = Random.Range(0, total_theme_songs);
        audioSource.clip = themeSongs[randIndex];
        audioSource.Play();
    }
    private void OnDisable() => audioSource.Stop();
}