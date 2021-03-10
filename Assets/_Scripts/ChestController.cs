using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Vincent Tse.
 * 2021-02-13
 */
public class ChestController : MonoBehaviour
{
    [SerializeField] private Animator animationController;
    public AudioSource chestOpen;
    public GameController gameController;

    public Dialogue dialogue;

    private bool isOpened = false;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")) && (isOpened == false))
        {
            animationController.SetBool("Triggered", true);
            chestOpen.Play();
            isOpened = true;
            gameController.addPotion();
            TriggerDialogue();
        }

    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this.gameObject);
    }
}
