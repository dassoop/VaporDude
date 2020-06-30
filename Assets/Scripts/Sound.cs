using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public AudioClip clip;
    public string name;

    public float volume = 1f;
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;
}
