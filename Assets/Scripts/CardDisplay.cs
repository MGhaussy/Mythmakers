using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text nameText;
    public Text cardTypeText;
    public Text apText;
    public Text rangeText;
    public Text targetText;
    public Text effectText;
    public Image img;

    private void OnValidate()
    {
        //nameText.text = card.name;
        //cardTypeText.text = card.cardtype;
        //apText.text = "AP:" + System.Environment.NewLine + card.ap.ToString();
        //rangeText.text = "Range: " + card.range.ToString();
        //targetText.text = "Target: " + card.target;
        //effectText.text = card.effect;
        img.sprite = card.image;
    }
}
