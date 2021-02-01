using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	PortalMaker PM;
	public GameObject PauseMenuObject;
	Canvas PauseCanvas;
	public Animator Anim;
	public Animator FadeAnim;

	void Start()
	{
		PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PortalMaker>();
		PauseCanvas = PauseMenuObject.GetComponentInParent<Canvas>();
		PauseCanvas.worldCamera = Camera.main;
		PauseMenuObject.SetActive(false);
	}
	// Update is called once per frame
	void Update()
    {
        if(FadeAnim == null)
		{
			FadeAnim = GameObject.FindGameObjectWithTag("FadeAnim").GetComponent<Animator>();
		}
    }

	public void Pause()
	{
		PM.IsPaused = true;
		PauseCanvas.sortingOrder = 1;
		PauseMenuObject.SetActive(true);
		Anim.SetBool("In", true);
		Time.timeScale = 0;
	}

	public void Resume()
	{
		StartCoroutine(PlayStart());
	}

	IEnumerator PlayStart()
	{
		Anim.SetBool("In", false);
		Time.timeScale = 1;
		yield return new WaitForSeconds(1f);
		PauseCanvas.sortingOrder = -100;
		PM.IsPaused = false;
		PauseMenuObject.SetActive(false);
	}

	public void Home()
	{
		StartCoroutine(MainMenu());
	}
	IEnumerator MainMenu()
	{
		FadeAnim.SetTrigger("Fade");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("MainMenu");
	}

	public void Restart()
	{
		StartCoroutine(RestartLevel());
	}
	IEnumerator RestartLevel()
	{
		FadeAnim.SetTrigger("Fade");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
