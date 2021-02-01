using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	public Animator CaseAnim;
	AudioManager AM;
    // Start is called before the first frame update
    void Start()
    {
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D Col)
	{
		if(Col.CompareTag("Player") || Col.CompareTag("Grabable") || Col.CompareTag("Projectile"))
		{
			AM.Play("ButtonPress");
			AM.Play("DoorOpen");
			CaseAnim.SetTrigger("Open");
		}
	}
}
