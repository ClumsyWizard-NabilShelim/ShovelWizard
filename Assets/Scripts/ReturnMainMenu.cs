using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
	public float delay;

    void Update()
    {
        if(delay <= 0)
		{
			SceneManager.LoadScene("MainMenu");
		}
		else
		{
			delay -= Time.deltaTime;
		}
    }
}
