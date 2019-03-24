using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    public Champion champ;
    public Board board;
    public Queue<Champion> champions = new Queue<Champion>();
    public Champion[] champlist = new Champion[2];
    public Piece[] pieces = new Piece[2];
    public Piece currentPiece;
    public Text namedesc;
    public Text questsdesc;
    public Text effectdesc;
    public Text victorydesc;
    public Text defeatdesc;
    public Transform hand;
    public GameObject cardPrefab;

    private void Start()
    {
        for (int i = 0; i < champlist.Length; i++)
        {
            champlist[i].team = i % 2;
        }
        foreach (Champion ch in champlist)
        {
            champions.Enqueue(ch);
            ch.PopulateDeck();
            ch.Shuffle();
            ch.hand.RemoveRange(0, ch.hand.Count);
        }
        champ = champions.Dequeue();
        
        this.SetView(champ);
        Draw();
        Draw();
        Draw();
        Draw();
        Draw();
        champ.Print();
    }

    private void SetView(Champion champ)
    {
        namedesc.text = champ.name;
        questsdesc.text = "Quests" + System.Environment.NewLine + champ.quests[0] + System.Environment.NewLine + champ.quests[1];
        victorydesc.text = "Victories" + System.Environment.NewLine + champ.vicdef[0].ToString();
        defeatdesc.text = "Defeats" + System.Environment.NewLine + champ.vicdef[0].ToString();
        
    }

    //Doesn't do anything.
    private void Shutdown()
    {
        foreach (Card card in this.champ.health)
        {
            this.champ.health.Dequeue();
        }
        for (int i = 0; i < this.champ.hand.Count; i++)
        {
            this.champ.hand[i] = null;
        }
    }

    private void Draw()
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
        if(currentPiece.CanPlay(card))
        champ.Play(card);
        Object.Destroy(cardDisplay);
    }

    public void EndTurn()
    {
        Debug.Log("click");
        while (champ.discard.Count > 0)
        {
            champ.health.Enqueue(champ.discard.Pop());
        }
        foreach (Transform cardDisplay in hand)
        {
            Object.Destroy(cardDisplay.gameObject);
        }
        champions.Enqueue(champ);
        champ = champions.Dequeue();
        board.NextChamp();
        this.SetView(champ);
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
    }

}
