using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDialogueTrigger : MonoBehaviour
{

    public DialogueTwoPaths dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManagerTwoPaths>().StartDialogue(dialogue);
    }

    void OnTriggerEnter2D(Collider2D door)
    {
        Debug.Log("door");
        TriggerDialogue();
    }

    void Update()
    {

        // continue dialogue with enter key
        if (Input.GetKeyDown("return"))
        {
            Debug.Log("c");
            FindObjectOfType<DialogueManagerTwoPaths>().DisplayNextSentence();
        }

    }

}

