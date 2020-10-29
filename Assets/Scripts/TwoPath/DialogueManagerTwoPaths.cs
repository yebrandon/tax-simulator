using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogueManagerTwoPaths : MonoBehaviour
{
    static bool isMarried;
    static string job;
    static bool houseBought;

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>(); // initialize queue to store sentences
    }

    public void StartDialogue(DialogueTwoPaths dialogue)
    {
        int randNum = Random.Range(1, 5); //generates number 1-4

        if (SceneManager.GetActiveScene().name == "party_scene")
        {
            if (!isMarried)
            {
                if (randNum == 1)
                {
                    dialogue.sentences = dialogue.sentences2; // 25% chance to switch to second batch of sentences.
                    isMarried = true;
                }
            }
            //spend
            // economy.wallet.Spend(10000);
            
        }
        else if (SceneManager.GetActiveScene().name == "work_scene")
        {
            randNum = Random.Range(1, 5); //generates 1-4
            if (randNum == 1)
            {
                dialogue.sentences = dialogue.sentences2; // 25% chance to switch to second batch of sentences.
                //promote
                economy.income += 10000;
                Debug.Log(economy.income);
            }
        }
        else if (SceneManager.GetActiveScene().name == "real_estate_scene")
        {
            //if balance > 100 k
            dialogue.sentences = dialogue.sentences2;
            houseBought = true;

            //else

        }

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

        //Teleport home show like what taxes youve done in the past year, and update the variables like balance and year
        SceneManager.LoadScene("black");




    }


}
