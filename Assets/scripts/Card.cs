using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image cardImageUI;


    public CardData cardData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardImageUI = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HideAndReveal();
    }

    public void SetCardData(string color, int value, string suit)
    {
        this.cardData.color = color;
        this.cardData.value = value;
        this.cardData.suit = suit;
    }

    public void HideAndReveal()
    {
        if (cardData.isRevealed)
        {
            cardImageUI.sprite = cardData.face;
        }
        else
        {
            cardImageUI.sprite = cardData.back;
        }
    }
}
