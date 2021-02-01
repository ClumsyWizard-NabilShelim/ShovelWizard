using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPortals : MonoBehaviour
{
	public Animator Anim;
	public AudioManager AM;
	public GameObject Effect;
	public string Levelname;
	void Start()
	{
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
	}
    void OnTriggerEnter2D(Collider2D Col)
	{
		if(Col.tag == "Player")
		{
			StartCoroutine(ChangeScene(Col.gameObject));
		}
	}

	IEnumerator ChangeScene(GameObject C)
	{
		AM.Play("Portal");
		Anim.SetTrigger("Fade");
		Destroy(C.gameObject);
		Instantiate(Effect, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(Levelname);
	}
}
