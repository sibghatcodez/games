using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioManager", menuName = "Game Audio")]
public class AudioCollection : ScriptableObject
{
    public List<AudioData> audios;
}