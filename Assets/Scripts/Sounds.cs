using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds
{
	public string AudioName;

	public AudioClip Clip;

	[Range(0,1)]
	public float volume;
	public bool IsLoop;
	[HideInInspector]
	public AudioSource Source;
}
