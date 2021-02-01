using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	public PlayerController PC;
	public PortalMaker PM;
	public Animator Anim;
	public int I = 0;
	public GameObject[] TutorialPages;
    // Start is called before the first frame update
    void Start()
    {
		if(PM != null)
			PM.enabled = false;
		if (PC != null)
			PC.enabled = false;
    }

    // Update is called once per frame
    void Update()
	{
		if(TutorialPages.Length == 1 && PC != null)
		{
			PC.enabled = true;
		}
		if (I == 1)
		{
			PC.enabled = true;
		}
		for (int i = 0; i < TutorialPages.Length; i++)
		{
			if(i == I)
			{
				TutorialPages[i].SetActive(true);
			}
			else
			{
				TutorialPages[i].SetActive(false);
			}
		}
    }

	public void Next()
	{
		I++;
	}

	public void Previous()
	{
		I--;
	}

	public void Close()
	{
		Anim.SetTrigger("IN");
		if (PM != null)
			PM.enabled = true;
	}
}
