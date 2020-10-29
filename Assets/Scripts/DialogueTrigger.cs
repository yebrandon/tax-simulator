using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

    void Start()
    {
        TriggerDialogue();
    }

    void Update()
    {

        // continue dialogue with enter key
        if(Input.GetKeyDown("return"))
        {
            Debug.Log("c");
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }

    }

}
