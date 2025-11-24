using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {

        Debug.Log("caiu coisa viu");
        GameObject droppedObj = eventData.pointerDrag;
        Card cardScript = droppedObj.GetComponent<Card>();
        cardScript.postDragParent = transform;
    }

}
