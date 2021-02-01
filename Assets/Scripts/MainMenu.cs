using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public Animator FadeAnimator;
	public string PlayScene;
	//public string OptionsScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Play()
	{
		StartCoroutine(StartGame());
	}

	IEnumerator StartGame()
	{
		FadeAnimator.SetTrigger("Fade");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(PlayScene);
	}
	//public void Options()
	//{
	//	SceneManager.LoadScene(OptionsScene);
	//}

}
