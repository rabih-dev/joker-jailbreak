using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class GameSetup : MonoBehaviour
{

    [SerializeField] private GameObject cardPrefab;
    public DeckSpritesDatabase deckSpritesDB;

    private int suitValues = 13;
    private int suitCount = 4;

    //0 = spades | 1 = clubs | 2 = hearts | 3 = diamonds
    private string[] cardSuits = { "spades", "clubs", "hearts", "diamonds" };

    private List<GameObject> completeDeck = new List<GameObject>();
    private List<GameObject> currentDeck = new List<GameObject>();

    [SerializeField] private Pile deckPile;
    [SerializeField] private Pile[] centerPiles;
    [SerializeField] private Pile jokerPile;
    [SerializeField] private Pile[] cornerPiles;

    private int randomizer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        GenerateDeck();
        currentDeck = ShuffleDeck(completeDeck);
        DealThePiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CardToJokerPile()
    {
        jokerPile.AddCard(currentDeck[currentDeck.Count - 1]);
        currentDeck.RemoveAt(currentDeck.Count - 1);
    }

    private GameObject GenerateCard(string color, int value, string suit, int suitGenerationIndex)
    {
        GameObject cardObj = Instantiate(cardPrefab);
        Card cardScript = cardObj.GetComponent<Card>();

        cardScript.cardData.color = color;
        cardScript.cardData.value = value;
        cardScript.cardData.suit = suit;

        cardScript.cardData.face = deckSpritesDB.GetCardFace(suit, value);
        cardScript.cardData.back = deckSpritesDB.GetCardBack();
        return cardObj;

    }
    private void GenerateDeck()
    {
        for (int i = 0; i < suitCount; i++)
        {
            switch (i)
            {
                case 0:
                    for (int j = 1; j <= suitValues; j++)
                    {
                        completeDeck.Add(GenerateCard("black", j, cardSuits[i], 0));

                    }
                    break;

                case 1:
                    for (int j = 1; j <= suitValues; j++)
                    {
                        completeDeck.Add(GenerateCard("black", j, cardSuits[i], 1));
                    }
                    break;

                case 2:
                    for (int j = 1; j <= suitValues; j++)
                    {
                        completeDeck.Add(GenerateCard("red", j, cardSuits[i], 2));
                    }
                    break;

                case 3:
                    for (int j = 1; j <= suitValues; j++)
                    {
                        completeDeck.Add(GenerateCard("red", j, cardSuits[i], 3));
                    }
                    break;
            }
        }

        Debug.Log("deck criado com sucesso!");

        foreach (var card in completeDeck)
        {
            Debug.Log("numero: " + card.GetComponent<Card>().cardData.value + " | cor: " + card.GetComponent<Card>().cardData.color + " | naipe: " + card.GetComponent<Card>().cardData.suit);
        }
    }

    private List<GameObject> ShuffleDeck(List<GameObject> deck)
    {
        //Embaralhamento Fisher-Yates

        System.Random rng = new System.Random(); //número aleatorio
        int n = deck.Count; //total de posições

        while (n > 1) // enquanto houver posições a serem definidas
        {
            n--; //retira 1 posiçao
            int k = rng.Next(n + 1); // define um indice entre 0 e a qrd d posições

            (deck[k], deck[n]) = (deck[n], deck[k]); // define uma posição aleatoria como o ultimo item da lista (pq ele começa no valor total, e vai diminuindo)
        }

        Debug.Log("deck embaralhado!");

        //foreach (var card in deck)
        //{
        //    Debug.Log("numero: " + card.GetComponent<Card>().cardData.value + " | cor: " + card.GetComponent<Card>().cardData.color + " | naipe: " + card.GetComponent<Card>().cardData.suit);
        //}

        return deck;
    }

    private void DealThePiles()
    {
        for (int pileIndex = 0; pileIndex < centerPiles.Length; pileIndex++)
        {
            for (int c = 0; c < 6; c++)
            {
                GameObject card = currentDeck[currentDeck.Count - 1];
                currentDeck.RemoveAt(currentDeck.Count - 1);

                centerPiles[pileIndex].AddCard(card);
            }
        }

        for (int pileIndex = 0; pileIndex < cornerPiles.Length; pileIndex++)
        {
            for (int c = 0; c < 2; c++)
            {
                GameObject card = currentDeck[currentDeck.Count - 1];
                currentDeck.RemoveAt(currentDeck.Count - 1);

                cornerPiles[pileIndex].AddCard(card);
            }
        }

        foreach (GameObject card in currentDeck)
        {
            deckPile.AddCard(card);
        }
    }
}


