using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour
{
    //NOTE: SCRIPT-ONLY FOR 'GAME' SCENE.
    bool isCheckpointReached = false;

    void Start()
    {
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!SceneManager.GetActiveScene().Equals("Lobby"))
        {
            if (!isCheckpointReached)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    int cp = int.Parse(transform.name.Split("_")[1]);
                    Debug.Log($"Checkpoint [{cp}] Reached!");
                    PlayerPrefs.SetInt("checkpoint", cp);
                    RenderSettings.fog = true;
                    isCheckpointReached = true;
                }
            }
        }
    }
}