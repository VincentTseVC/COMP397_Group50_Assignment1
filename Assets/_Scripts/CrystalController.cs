using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalController : MonoBehaviour
{
    // Start is called before the first frame update
    private float rotatex = 0;
    public GameController gameController;

    public Dialogue dialogue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //will try to rotate crystal for prettiness later
        //rotatex += 0.01f;
        //transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotatex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found Crystal");
            gameController.addCrystal();
            Destroy(this.gameObject);
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this.gameObject);
    }
}
