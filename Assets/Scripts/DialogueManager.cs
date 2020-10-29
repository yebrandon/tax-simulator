using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour {


    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>(); // initialize queue to store sentences
    }

    public void StartDialogue(Dialogue dialogue)
    {

        Debug.Log("b");
        animator.SetBool("IsOpen", true); // start animation

        nameText.text = dialogue.name; //set name of speaker

        sentences.Clear(); //clear sentences

        foreach (string sentence in dialogue.sentences) // queue up the sentences
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) // end if there are no sentences
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue(); // dequeue first sentence and store in string sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence)); // types string sentence
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "title_scene" || sceneName == "intro_scene" || sceneName == "end_scene" || sceneName == "controls_scene")
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
	}


}
