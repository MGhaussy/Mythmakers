using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Overlay : MonoBehaviour
{
    public Champion champ;
    public Board board;
    public Queue<Champion> champions = new Queue<Champion>();
    public Champion[] champlist = new Champion[2];
    public Piece[] pieces = new Piece[6];
    public Text namedesc;
    public Text questsdesc;
    public Text effectdesc;
    public Text victorydesc;
    public Text defeatdesc;
    public Text healthtext;
    public Transform hand;
    public Transform display;
    public GameObject cardPrefab;
    public List<Piece> mytargets = new List<Piece>();
    public int discardcount;

    private void Start()
    {
        for (int i = 0; i < champlist.Length; i++)
        {
            champlist[i].team = i % 2;
        }
        foreach (Champion ch in champlist)
        {
            ch.Reset();
            champions.Enqueue(ch);
            ch.PopulateDeck();
            ch.Shuffle();
            ch.hand.RemoveRange(0, ch.hand.Count);
        }
        champ = champions.Dequeue();
        
        Draw();
        Draw();
        Draw();
        Draw();
        Draw();
        this.SetView(champ);
        champ.Print();
    }

    private void SetView(Champion champ)
    {
        namedesc.text = champ.name;
        questsdesc.text = "Quests" + System.Environment.NewLine + champ.quests[0] + System.Environment.NewLine + champ.quests[1];
        victorydesc.text = "Victories" + System.Environment.NewLine + champ.vicdef[0].ToString();
        defeatdesc.text = "Defeats" + System.Environment.NewLine + champ.vicdef[0].ToString();
        healthtext.text = champ.health.Count.ToString();
        
    }

    public void SwitchDisplay()
    {
        for (int i = 5; i < 10; i++)
        {
            this.transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 1 - this.transform.GetChild(i).GetComponent<CanvasGroup>().alpha;
        }
    }

    //Doesn't do anything.
    private void Shutdown()
    {
        Debug.Log("Bye.");
    }

    public void Draw()
    {
        Card handcard = champ.Draw();
        if (handcard)
        {
            GameObject cardDisplay = (GameObject)Instantiate(cardPrefab);
            cardDisplay.GetComponent<CardDisplay>().SetCard(handcard);
            cardDisplay.transform.SetParent(hand);
        }
    }

    public void Play(GameObject cardDisplay)
    {
        Card card = cardDisplay.GetComponent<CardDisplay>().card;
        if (board.currentPiece.CanPlay(card))
        {
            champ.Play(card, mytargets);
            Object.Destroy(cardDisplay);
        }
        board.ResetClickable();
    }

    public void Discard(GameObject cardDisplay)
    {
        champ.Discard(cardDisplay.GetComponent<CardDisplay>().card);
        Object.Destroy(cardDisplay);
    }

    public void SetTarget(Piece champ)
    {
        mytargets.Add(champ);
        Debug.Log(champ.name + " was added to targets.");
    }

    public void EndTurn()
    {
        while (champ.discard.Count > 0)
        {
            champ.health.Enqueue(champ.discard.Pop());
        }
        foreach (Transform cardDisplay in hand)
        {
            Object.Destroy(cardDisplay.gameObject);
        }
        champ.ap = 3;
        champ.fleetcount = 0;
        champions.Enqueue(champ);
        champ = champions.Dequeue();
        board.NextChamp();
        Debug.Log(champ.name);
        while (champ.hand.Count < champ.handlimit)
        {
            champ.Draw();
        }
        foreach (Card handcard in champ.hand)
        {
            GameObject cardDisplay = (GameObject)Instantiate(cardPrefab);
            cardDisplay.GetComponent<CardDisplay>().SetCard(handcard);
            cardDisplay.transform.SetParent(hand);
        }
        foreach (Flame flame in board.flames)
        {
            if (flame.isActive && flame.IsCaptured())
            {
                flame.Extinguish();
            }
        }
        this.SetView(champ);
    }

    public void AddSword()
    {
        foreach (Piece piece in mytargets)
        {
            piece.mychamp.modifiers[0] += 1;
        }
        this.transform.GetChild(13).GetComponent<CanvasGroup>().alpha = 0;
    }

    public void AddShield()
    {
        foreach (Piece piece in mytargets)
        {
            piece.mychamp.modifiers[1] += 1;
        }
        this.transform.GetChild(13).GetComponent<CanvasGroup>().alpha = 0;
    }

}
