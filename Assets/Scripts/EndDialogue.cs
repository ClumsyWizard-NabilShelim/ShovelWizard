using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDialogue : MonoBehaviour
{
	public DialogueTrigger DT;
    // Start is called before the first frame update
    void Start()
    {
		DT.StartDialogue();
	}
}
