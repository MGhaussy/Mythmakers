using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Champ", menuName = "Champion")]
public class Champion : ScriptableObject
{
    public new string name;
    public float color;
    public int speed;
    public string effect;
    public int[] modifiers;
    public int[] vicdef;
    public string[] quests = new string[2];
    public Queue<Card> health = new Queue<Card>();
    public Stack<Card> damage;
    public Stack<Card> discard;
    public Card[] cardlist;
    public Card[] hand = new Card[5];
    public Board board;
    public Space space;

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

    public void Draw()
    {
        for (int i = 0; i < this.hand.Length; i++)
        {
            if (this.hand[i] == null)
            {
                this.hand[i] = this.health.Dequeue();
                break;
            }
        }
    }

    public void Damage(int dmg)
    {
        for (int i = 0; i < dmg; i++)
        {
            this.damage.Push(this.health.Dequeue());
        }
    }

    public void Move(Space newspace)
    {
        this.space = newspace;
    }

}
