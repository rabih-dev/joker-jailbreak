using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Card : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    public UnityEngine.UI.Image cardImageUI;
    public CardData cardData;
    public Pile pileParent;

    private Vector3 startDragPos;
    [HideInInspector] public Transform postDragParent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardImageUI = GetComponent<UnityEngine.UI.Image>();
        pileParent = GetComponentInParent<Pile>();
    }

    // Update is called once per frame
    void Update()
    {
        HideAndReveal();
    }
    public void SetParent(Pile parent)
    {
        pileParent = parent;
    }

     public void SetCardData(string color, int value, string suit)
    {
        this.cardData.color = color;
        this.cardData.value = value;
        this.cardData.suit = suit;
    }

    #region Drag n' Drop

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!cardData.isJoker && !pileParent.isDeckPile)
        {
            startDragPos = transform.position;
            transform.position = Input.mousePosition;
            cardImageUI.raycastTarget = false;
            Debug.Log("i got clicked");
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!cardData.isJoker && !pileParent.isDeckPile)
        {
            transform.position = Input.mousePosition;
            Debug.Log("being dragged");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
        {
            if (!cardData.isJoker && !pileParent.isDeckPile)
            {
                if (postDragParent != null)
                {
                    transform.SetParent(postDragParent);
                    transform.position = postDragParent.position;
                    postDragParent = null;

                    GameManager.GetInstance().StageCard(this.gameObject);
                }

                else
                {
                    transform.position = startDragPos;
                }

                cardImageUI.raycastTarget = true;
            }
        }

    #endregion Drag n' Drop

   

    public void HideAndReveal()
    {
        if (cardData.isRevealed)
        {
            cardImageUI.sprite = cardData.face;
            cardImageUI.color = Color.white;
        }
        else
        {
            cardImageUI.sprite = cardData.back;
            cardImageUI.color = Color.black;
        }
    }

   
}
