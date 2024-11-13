using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audio;
    [SerializeField] public List<AudioClip> clips;
    [SerializeField] GameObject player;
    AudioSource playerAudio;
    public bool isSoundOn = true;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        playerAudio = player.GetComponent<AudioSource>();
    }
    void Update()
    {
        if(player.transform.position.z > 9 && player.transform.position.z < 15) {
            playerAudio.enabled = true;
            audio.clip = clips[2];
            audio.Play();
            Debug.Log(">>player ryfl msg recieved<<");
        }
    }
    public void PlaySounds(string msg)
    {
        if (isSoundOn)
        {
            if (msg.Equals("death"))
            {
                playerAudio.enabled = false;
                audio.clip = clips[0];
                audio.Play();
                Debug.Log(">>player death msg recieved<<");
            }
            else if (msg.Equals("jump"))
            {
                audio.clip = clips[1];
                audio.Play();
                Debug.Log(">>player jump msg recieved<<");
            }
        }
    }
}