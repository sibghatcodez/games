using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AudioData
{
    public AudioName audioName;
    public AudioClip audioClip;
    public float volume = 1.0f;
    public bool loop = false;
}