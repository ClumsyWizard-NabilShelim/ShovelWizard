using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public Animator FadeAnim;
	public Sounds[] sounds;
    // Start is called before the first frame update

	void Awake()
	{
		//DontDestroyOnLoad(gameObject);

		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		foreach (Sounds s in sounds)
		{
			s.Source = gameObject.AddComponent<AudioSource>();
			s.Source.clip = s.Clip;
			s.Source.volume = s.volume;
			s.Source.loop = s.IsLoop;
		}
	}

	void Start()
	{
		FadeAnim = GameObject.FindGameObjectWithTag("FadeAnim").GetComponent<Animator>();
		Play("BG");
	}
    public void Play(string name)
	{
		Sounds s = Array.Find(sounds, sound => sound.AudioName == name);
		s.Source.Play();
	}

	public void Restart()
	{
		StartCoroutine(RestartLevel());
	}

	IEnumerator RestartLevel()
	{
		if(FadeAnim != null)
			FadeAnim.SetTrigger("Fade");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Destroy(gameObject);
	}
}
