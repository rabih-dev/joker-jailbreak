using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

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

    public static GameManager GetInstance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StateControl()
    {

    }

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
    }
}
