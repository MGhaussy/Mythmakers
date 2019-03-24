using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Champ", menuName = "Champion")]
public class Champion : ScriptableObject
{
    public new string name;
    public float color;
    public int team;
    public int speed;
    public int handlimit = 5;
    public string effect;
    public int[] modifiers;
    public int[] vicdef;
    public string[] quests = new string[2];
    public Queue<Card> health = new Queue<Card>();
    public Stack<Card> damage;
    public Stack<Card> discard = new Stack<Card>();
    public Card[] cardlist;
    public List<Card> hand;

    public void PopulateDeck()
    {
        if (this.health.Count == 0)
        {
            foreach (Card card in cardlist)
            {
                for (int i = 0; i < card.copies; i++)
                {
                 this.health.Enqueue(card);
                }
            }
        }
        
    }

    public void Shuffle()
    {
        List<Card> shufflecards = this.health.ToList();
        for (int n = shufflecards.Count - 1; n > 0; n--)
        {
            int k = Random.Range(0, n + 1);
            Card temp = shufflecards[n];
            shufflecards[n] = shufflecards[k];
            shufflecards[k] = temp;
        }
        Queue<Card> shuffledeck = new Queue<Card>();
        foreach (Card card in shufflecards)
        {
            shuffledeck.Enqueue(card);
        }
        this.health = shuffledeck;
    }

    public void Print()
    {
        Debug.Log(this.health.Count);
    }

    public Card Draw()
    {
        Card newhandcard = this.health.Dequeue();
        if (newhandcard)
        {
            hand.Add(newhandcard);
            return newhandcard;
        }
        return null;
    }

    public void Damage(int dmg)
    {
        for (int i = 0; i < dmg; i++)
        {
            this.damage.Push(this.health.Dequeue());
        }
    }

    public void Play(Card card)
    {
        hand.Remove(card);
        discard.Push(card);
        Debug.Log(card.name + " was played!");
    }

    public void Shutdown()
    {
        this.health = null;
        hand.RemoveRange(0, hand.Count - 1);
    }

}
