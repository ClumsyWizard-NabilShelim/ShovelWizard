using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
	public Boss B;
	public bool Boss;
	public GameObject StopCol;
	public bool TutStart;
	public bool GameFinish;
	public GameObject FinishGameobject;
	public GameObject TutMan;
	public GameObject TutCanvas;
	public GameObject UnknownVoice;
	public GameObject EndDialogueTextObject;
	[HideInInspector] public bool Villager;
	public Animator VillagerAnim;
	public Animator DialogueAnimator;
	public Queue<string> sentences;
	public TextMeshProUGUI DialogueText;
    // Start is called before the first frame update
    void Start()
    {
		sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void StartConversation(Dialogue d)
	{
		sentences.Clear();
		DialogueAnimator.SetBool("In", true);

		foreach (string S in d.DialogueText)
		{
			sentences.Enqueue(S);
		}

		Next();
	}

	public void Next()
	{
		Debug.Log("Helo");

		if (sentences.Count == 0)
		{
			End();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeOutText(sentence));
	}

	IEnumerator TypeOutText(string Sen)
	{
		DialogueText.text = "";
		foreach (char letter in Sen.ToCharArray())
		{
			DialogueText.text += letter;
			yield return null;
		}
	}

	void End()
	{
		DialogueAnimator.SetBool("In", false);
		if (Villager)
		{
			Destroy(StopCol);
			VillagerAnim.SetTrigger("MoveOut");
			Villager = false;
		}
		if(EndDialogueTextObject != null)
		{
			EndDialogueTextObject.SetActive(true);
		}
		if (GameFinish)
		{
			SceneManager.LoadScene("EndCredit");
		}
		if (TutStart && TutMan != null)
		{
			TutMan.SetActive(true);
			TutCanvas.SetActive(true);
			Destroy(UnknownVoice);
		}
		if (Boss)
		{
			B.IntroDone = true;
		}
	}
}
