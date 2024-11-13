using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TrainScript : MonoBehaviour
{
    PlayableDirector timeline;
    [SerializeField] GameObject panelAnim;

    [Header("Camera Settings")]
    [SerializeField] Camera trainCamera;
    [Header("Player n Smoke Settings")]
    [SerializeField] GameObject player;
    [SerializeField] Animator playerCameraAnim;
    [SerializeField] GameObject Smoke;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextMeshProUGUI countdownTXT;

    AudioSource audio;
    public float timer = 1f;
    public bool IsGameOver = false;
    int index = 3;

    //NOTE: SCRIPT-ONLY FOR 'GAME' SCENE.

    void Awake()
    {
        audio = GetComponent<AudioSource>();

        if (!SceneManager.GetActiveScene().Equals("Lobby"))
        {
            int checkpoint = PlayerPrefs.GetInt("checkpoint", 0);
            Debug.Log("CHECKPOINT: " + checkpoint);
            timeline = GetComponent<PlayableDirector>();
            //PlayerPrefs.DeleteAll(); //testing, to be removed at the end.
            PlayerPrefs.SetInt("checkpoint", checkpoint);

            if (checkpoint.Equals(1))
            {
                TransferControls();
                audio.enabled = false;
                playerCameraAnim.enabled = false;
                player.transform.rotation = quaternion.Euler(0, 0, 0);
                player.transform.position = new Vector3(309.350006f, -148.172f, 105f);
                playerCameraAnim.gameObject.transform.rotation = quaternion.Euler(0, 0, 0);
                Smoke.gameObject.SetActive(true);
                if (timeline != null) timeline.time = 300;
                Invoke("ChangeTrainRotation", 1f);
                playerMovement.countdownStarted = true;
                Invoke("CountdownMessage", 0.1f);
                Invoke("DisableCountdown", 3f);
            }
            else
            {
                if (timeline != null)
                {
                    timeline.stopped += OnTimelineStopped;
                }
            }
        }
    }
    void DisableCountdown()
    {
        playerMovement.countdownStarted = false;
    }
    void CountdownMessage()
    {
        if (index > 0)
        {
            countdownTXT.gameObject.SetActive(true);
            countdownTXT.text = "" + index;
            index--;
            Invoke("CountdownMessage", 1f);
        }
        else if (index == 0)
        {
            countdownTXT.text = "Go";
            index--;
            Invoke("CountdownMessage", 1f);
        }
        else
        {
            countdownTXT.gameObject.SetActive(false);
        }
    }


    void HideCountdown()
    {
        countdownTXT.gameObject.SetActive(false);
    }

    void Start()
    {
    }
    void Update()
    {
        if (!IsGameOver) timer += Time.deltaTime;
    }
    void OnTimelineStopped(PlayableDirector pd)
    {
        panelAnim.gameObject.SetActive(true); //Making the Black Cinematic Screen Visible.
        transform.rotation = Quaternion.Euler(0, 90, 60);
        Smoke.gameObject.SetActive(true); //Making the smoke trails from train visible.
        Invoke("TransferControls", 3f);
    }
    void TransferControls()
    {
        trainCamera.gameObject.SetActive(false);
        player.gameObject.SetActive(true);

    }
    void ChangeTrainRotation()
    {
        transform.rotation = Quaternion.Euler(0, 90, 60);
    }
}