using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
	public Animator DoorAnimator;
	bool PlayerIn;
	public GameObject ButtonToPress;
	AudioManager AM;
    // Start is called before the first frame update
    void Start()
    {
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
		ButtonToPress.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if (PlayerIn)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				AM.Play("Lever");
				AM.Play("DoorOpen");
				DoorAnimator.SetTrigger("Open");
			}
		}
    }

	void OnTriggerEnter2D(Collider2D Col)
	{
		if(Col.tag == "Player")
		{
			PlayerIn = true;
			ButtonToPress.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			PlayerIn = false;
			ButtonToPress.SetActive(false);
		}
	}
}
