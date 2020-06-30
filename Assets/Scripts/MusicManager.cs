using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
	public static MusicManager instance;

    void Awake()
	{
        if (instance == null)
		{
			instance = this;
		}

		else
		{
			Destroy(gameObject);
		}
	}

    void Start()
	{
        
	}

}
