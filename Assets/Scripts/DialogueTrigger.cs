using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public bool Boss;
	public Collider2D Col;
	public bool TutStart;
	public bool GameFinish;
	public DialogueManager DM;
	public bool Villager;
	public Dialogue D;
    
	void Awake()
	{
		Col = GetComponent<Collider2D>();
		DM = GameObject.FindGameObjectWithTag("DM").GetComponent<DialogueManager>();
	}
	void Start()
	{
		DM.GameFinish = GameFinish;
		if (Boss)
		{
			StartDialogue();
		}
	}

	void OnTriggerEnter2D(Collider2D Col)
	{
		if(Col.tag == "Player")
		{
			StartDialogue();
		}
	}
	public void StartDialogue()
	{
		if(Col!= null)
			Col.enabled = false;
		DM.Boss = Boss;
		DM.Villager = Villager;
		DM.TutStart = TutStart;
		DM.StartConversation(D);
	}
}
