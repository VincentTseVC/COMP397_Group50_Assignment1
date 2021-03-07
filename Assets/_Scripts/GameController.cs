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
    
}
