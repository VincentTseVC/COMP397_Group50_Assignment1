using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Start is called before the first frame update
    public int crystals;
    public int potions;

    public GameData(GameController game)
    {
        crystals = game.crystalCount;
        potions = game.potionCount;
    }
}
