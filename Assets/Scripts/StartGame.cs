using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	public Animator Fadeanim;

	void OnTriggerEnter2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			StartCoroutine(ChangeScene(Col.gameObject));
		}
	}

	IEnumerator ChangeScene(GameObject C)
	{
		Fadeanim.SetTrigger("Fade");
		yield return new WaitForSeconds(1f);
		Destroy(C.gameObject);
		SceneManager.LoadScene("Level_1");
	}
}
