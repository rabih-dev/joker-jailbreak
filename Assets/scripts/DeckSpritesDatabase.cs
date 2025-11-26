using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu]
public class DeckSpritesDatabase : ScriptableObject
{

    public Sprite jokerSprite;

    //0 = spades | 1 = clubs | 2 = hearts | 3 = diamonds in gamesetup
    public Sprite[] spadesSprites = new Sprite[13];
    public Sprite[] clubsSprites = new Sprite[13];
    public Sprite[] heartsSprites = new Sprite[13];
    public Sprite[] diamondsSprites = new Sprite[13];

    public Sprite cardBack;

    public Sprite GetCardFace(string suit, int value)
    {
        switch (suit)
        {
            case "spades": return spadesSprites[value - 1];
            case "clubs": return clubsSprites[value - 1];
            case "hearts": return heartsSprites[value - 1];
            case "diamonds": return diamondsSprites[value - 1];
        }

        return null;
    }

    public Sprite GetCardFace(bool isJoker)
    {
        if (isJoker)
        {
            return jokerSprite;
        }

        return null;
    }

    public Sprite GetCardBack() 
    {
        return cardBack;
    }


}
