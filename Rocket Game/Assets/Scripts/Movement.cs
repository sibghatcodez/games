using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public Rigidbody player;
    [SerializeField] public float thrustSpeed = 1500f;
    [SerializeField] public float rotationSpeed = 1f;
    [SerializeField] public AudioClip EngineSound;
    [SerializeField] public AudioClip SuccessSound;
    [SerializeField] public AudioClip LoseSound;
    [SerializeField] public Collisions collisionScript;
    [SerializeField] public ParticleSystem boostParticle;
    [SerializeField] public Animator CamAnim;
    public AudioSource sound = null;


    void Start()
    {
        collisionScript.attempts.text = "Attempts: " + PlayerPrefs.GetInt("Attempts");
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            player.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
             PlaySound(EngineSound);
             boostParticle.Play();
        } 
        if (Input.GetKeyUp(KeyCode.Space)) {
            if(sound.isPlaying) {
                sound.Stop();
                boostParticle.Stop();
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
        }

        if(PlayerPrefs.GetInt("gameLoaded") == 1) PlayCameraAnim();
    }

   public void PlaySound(AudioClip audio)
    {
        
            sound.clip = audio;
            if(!sound.isPlaying) sound.Play();
    }
    public void PlayCameraAnim() {
        CamAnim.SetBool("CameraAnim", true);
        PlayerPrefs.SetInt("gameLoaded", 0);
        PlayerPrefs.Save();
    }
}