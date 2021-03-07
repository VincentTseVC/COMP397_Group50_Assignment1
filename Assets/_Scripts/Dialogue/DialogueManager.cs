using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    public PlayerBehaviour player;
    private GameObject caller;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (player.isTalking && Input.anyKeyDown)
            DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue, GameObject caller)
    {
        this.caller = caller;
        player.isTalking = true;

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
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
        player.isTalking = false;
        animator.SetBool("IsOpen", false);
    }

}