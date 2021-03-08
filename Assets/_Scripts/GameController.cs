using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
* Vincent Tse.
* 2021-02-13
*/
public class GameController : MonoBehaviour
{
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("Crystal Count")]
    public Text crystalText;
    public int crystalCount = 0;

    [Header("Potion Count")]
    public Text potionText;
    public int potionCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCrystal()
    {
        crystalCount++;
        crystalText.text = ""+ crystalCount;
    }

    public void addPotion()
    {
        potionCount++;
        potionText.text = "" + potionCount;
    }

    public void usePotion()
    {
        potionCount--;
        potionText.text = "" + potionCount;
    }
}
