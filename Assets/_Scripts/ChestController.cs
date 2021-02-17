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

    private bool isOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")) && (isOpened == false))
        {
            animationController.SetBool("Triggered", true);
            chestOpen.Play();
            isOpened = true;
        }

    }
}
