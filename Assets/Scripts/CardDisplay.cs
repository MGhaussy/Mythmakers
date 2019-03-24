using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Card card;
    Transform returnParent = null;
    public Text nameText;
    public Text cardTypeText;
    public Text apText;
    public Text rangeText;
    public Text targetText;
    public Text effectText;
    public Image img;

    public void SetCard(Card icard)
    {
        this.card = icard;
        img.sprite = icard.image;
    }
    private void OnValidate()
    {
        //nameText.text = card.name;
        //cardTypeText.text = card.cardtype;
        //apText.text = "AP:" + System.Environment.NewLine + card.ap.ToString();
        //rangeText.text = "Range: " + card.range.ToString();
        //targetText.text = "Target: " + card.target;
        //effectText.text = card.effect;
        //img.sprite = card.image;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData data)
    {
        this.transform.position = data.position;
    }

    public void OnEndDrag(PointerEventData data)
    {
        this.transform.SetParent(returnParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
