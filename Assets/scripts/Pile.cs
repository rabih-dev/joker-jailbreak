using UnityEngine;
using System.Collections.Generic;

public class Pile : MonoBehaviour
{

    //o baralho tem 52 cartas
    public bool revealTopCard;

    private CardData revealedCardData;

    private List<GameObject> cardsInPile = new List<GameObject>();

    public Vector2 cardDisplacement;
    //public Vector2 messiness girar um pouco as cartas pra deixar elas mais bagunçada

    void Start()
    {
        revealTopCard = true;
    }

    // Update is called once per frame
    void Update()
    {
        RevealTopCard();
    }

    public void RevealTopCard()
    {
        if (cardsInPile.Count > 0)
        {
            Debug.Log(cardsInPile.Count - 1);
            revealedCardData = cardsInPile[cardsInPile.Count - 1].GetComponent<Card>().cardData;

            if (revealTopCard)
            {
                revealedCardData.isRevealed = true;
            }
        }
    }

    public void AddCard(GameObject card)
    {
        cardsInPile.Add(card);
        card.transform.SetParent(transform, false);


        Debug.Log("card adicionado à pilha");
    }

    public void RemoveCard(GameObject card) 
    {
        cardsInPile.Remove(card);
    }


}
