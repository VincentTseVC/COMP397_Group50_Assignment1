using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameController gameController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found Crystal");
            gameController.addCrystal();
            Destroy(this.gameObject);
        }
    }
}
