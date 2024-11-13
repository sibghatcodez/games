using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// SCENE 1: SCRIPT (ONLY)
public class StartScript : MonoBehaviour
{
   [SerializeField] Button playBtn;
   [SerializeField] Button exitBtn;

    void Start () {
        playBtn.onClick.AddListener(onPlayButtonClick);
        exitBtn.onClick.AddListener(onExitButtonClick);
        PlayerPrefs.SetInt("wave", 0);
        PlayerPrefs.SetInt("towersAllowed", 0);
    }
   public void onPlayButtonClick () {
    PlayerPrefs.SetInt("wave", 1);
    SceneManager.LoadScene("Level 1");
   }
   public void onExitButtonClick () {
    Application.Quit();
    PlayerPrefs.DeleteAll();
   }
}