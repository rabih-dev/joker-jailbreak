using UnityEngine;
using System.Collections.Generic;

public class Pile : MonoBehaviour
{

    //o baralho tem 52 cartas
    
    [HideInInspector]
    public bool isDeckPile;
    public bool revealTopCard;
    public CardData topCardData;


    public List<GameObject> cardsInPile = new List<GameObject>();

    public Vector2 cardDisplacement;
    //public Vector2 messiness girar um pouco as cartas pra deixar elas mais bagunçada

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (cardsInPile.Count > 0)
        {
            topCardData = cardsInPile[cardsInPile.Count - 1].GetComponent<Card>().cardData;
            RevealTopCard();
        }
    }

    public void RevealTopCard()
    {
        if (revealTopCard && !isDeckPile)
        {
            Debug.Log("estou revelando");
            topCardData.isRevealed = true;
        }
        else
        { 
            topCardData.isRevealed = false;
        }
    }

    public int GetCount() 
    {
        return cardsInPile.Count;
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
