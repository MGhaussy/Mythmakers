using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    public Champion champ;

    public Text namedesc;
    public Text questsdesc;
    public Text effectdesc;
    public Text victorydesc;
    public Text defeatdesc;
    public Image[] hand = new Image[5];

    private void OnValidate()
    {
        namedesc.text = champ.name;
        questsdesc.text = "Quests" + System.Environment.NewLine + champ.quests[0] + System.Environment.NewLine + champ.quests[1];
        victorydesc.text = "Victories" + System.Environment.NewLine + champ.vicdef[0].ToString();
        defeatdesc.text = "Defeats" + System.Environment.NewLine + champ.vicdef[0].ToString();
        
        champ.PopulateDeck();
        champ.Shuffle();
        champ.Draw();
        champ.Draw();
        champ.Draw();
        champ.Draw();
        champ.Draw();
        for (int i = 0; i < 5; i++)
        {
            hand[i].sprite = champ.hand[i].image;
        }
        champ.Print();
    }

    private void Shutdown()
    {

    }

}
