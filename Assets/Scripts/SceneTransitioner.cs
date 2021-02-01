using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
	public Animator FadeAnim;
	//public string SceneName;

	void OnTriggerEnter2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			StartCoroutine(ChangeScene(Col.gameObject));
		}
	}

	IEnumerator ChangeScene(GameObject C)
	{
		FadeAnim.SetTrigger("Fade");
		yield return new WaitForSeconds(1f);
		Destroy(C.gameObject);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
}
