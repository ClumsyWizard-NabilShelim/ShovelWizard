using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
	AudioManager AM;
	public string HurtSoundEffect;
	public GameObject Effect;

	void Start()
	{
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
	}

	void Update()
	{
		if(transform.position.y < -100)
		{
			Kill();
		}
	}
	public void Kill()
	{
		AM.Play(HurtSoundEffect);
		GameObject EF = Instantiate(Effect, transform.position, Quaternion.identity);
		Destroy(EF, 2f);
		if(gameObject.tag == "Player")
		{
			AM.Restart();
		}
		Destroy(gameObject);
	}
}
