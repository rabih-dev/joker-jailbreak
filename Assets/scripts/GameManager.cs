using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Deck Generation
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

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Singleton
    private static GameManager instance;

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //State Machine
    public GameStates currentState;
    public GameStates previousState;

    [HideInInspector]
    public List<GameObject> stagedCards = new List<GameObject>();
    private int blackTotalValue;
    private int redTotalValue;
    public enum GameStates
    {
        gameSetup,
        openGameState,
        stageResolve,
        drawState
    }

    void Awake()
    {
        // padrão clássico de singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GenerateDeck();
        currentDeck = ShuffleDeck(completeDeck);
        DealThePiles();
    }



    // Update is called once per frame
    void Update()
    {
        if (jokerPile.GetCount() >= 4)
        {
            Debug.Log("Perdeu");
        }

    }

    public void StateControl()
    {

    }

    public void WinCheck() 
    {
        for (int pileIndex = 0; pileIndex < centerPiles.Length; pileIndex++) 
        {
            if (centerPiles[pileIndex].GetCount() <= 0)
            {
                Debug.Log("Ganhou!!");
            }
        }
    }

    public void CardToJokerPile()
    {
        jokerPile.AddCard(currentDeck[currentDeck.Count - 1]);
        jokerPile.cardsInPile[jokerPile.GetCount() - 1].GetComponent<Card>().SetParent(jokerPile);

        deckPile.cardsInPile.RemoveAt(currentDeck.Count - 1);
        currentDeck.RemoveAt(currentDeck.Count - 1);
       

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
                centerPiles[pileIndex].revealTopCard = true;
            }
        }

        for (int pileIndex = 0; pileIndex < cornerPiles.Length; pileIndex++)
        {
            for (int c = 0; c < 2; c++)
            {
                GameObject card = currentDeck[currentDeck.Count - 1];
                currentDeck.RemoveAt(currentDeck.Count - 1);

                cornerPiles[pileIndex].AddCard(card);
                cornerPiles[pileIndex].revealTopCard= true; 
            }
        }

        foreach (GameObject card in currentDeck)
        {
            deckPile.AddCard(card);
            deckPile.isDeckPile = true;
            deckPile.revealTopCard = true;
        }

        jokerPile.AddCard(GenerateCard(true));
        jokerPile.revealTopCard= true;
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
        return deck;
    }

    #region Stage
    public void StageCard(GameObject cardObj)
    {
        Card cardScript = cardObj.GetComponent<Card>();

        if (cardScript.cardData.color == "black")
        {
            blackTotalValue += cardScript.cardData.value;
        }

        else
        {
            redTotalValue += cardScript.cardData.value;
        }

        stagedCards.Add(cardObj);

        Debug.Log("preto: " + blackTotalValue + "| vermelho: " + redTotalValue);
    }

    public void StageResolution()
    {
        if (redTotalValue == blackTotalValue)
        {
            foreach (GameObject card in stagedCards)
            {
                card.GetComponent<Card>().pileParent.RemoveCard(card);
                card.SetActive(false);

                redTotalValue = 0;
                blackTotalValue = 0;
            }
            stagedCards.Clear();
            WinCheck();
        }

        else
        {
            Debug.Log("invalid selection");
        }

    }

    public void CancelStage()
    {
        foreach (GameObject card in stagedCards)
        {
            Card cardScript = card.GetComponent<Card>();

            card.transform.position = cardScript.pileParent.transform.position;
            card.transform.SetParent(cardScript.pileParent.transform);

            redTotalValue = 0;
            blackTotalValue = 0;
        }

        stagedCards.Clear();
    }
    #endregion Stage

    #region Deck Generation
    private GameObject GenerateCard(string color, int value, string suit)
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

    private GameObject GenerateCard(bool isJoker)
    {
        GameObject cardObj = Instantiate(cardPrefab);
        Card cardScript = cardObj.GetComponent<Card>();

        cardScript.cardData.isJoker = isJoker;

        cardScript.cardData.face = deckSpritesDB.GetCardFace(isJoker);
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
                        completeDeck.Add(GenerateCard("black", j, cardSuits[i]));

                    }
                    break;

                case 1:
                    for (int j = 1; j <= suitValues; j++)
                    {
                        completeDeck.Add(GenerateCard("black", j, cardSuits[i]));
                    }
                    break;

                case 2:
                    for (int j = 1; j <= suitValues; j++)
                    {
                        completeDeck.Add(GenerateCard("red", j, cardSuits[i]));
                    }
                    break;

                case 3:
                    for (int j = 1; j <= suitValues; j++)
                    {
                        completeDeck.Add(GenerateCard("red", j, cardSuits[i]));
                    }
                    break;
            }
        }

        Debug.Log("deck criado com sucesso!");
    }

    #endregion Deck Generation

    public static GameManager GetInstance()
    {
        return instance;
    }
}
