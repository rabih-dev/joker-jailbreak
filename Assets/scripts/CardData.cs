using UnityEngine;

[System.Serializable]
public class CardData
{
    public bool isRevealed;
    public bool isJoker;

    public string color;
    public int value;
    public string suit;
    public Sprite face;
    public Sprite back;
    private int position;



    public CardData(string color, int value, string suit)//, Sprite face, Sprite back, int position)
    {
        this.color = color;
        this.value = value;
        this.suit = suit;
    }

    public CardData(bool isJoker) 
    {
        this.isJoker = isJoker;
    }
}
