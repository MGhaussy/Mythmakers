using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "New Champ", menuName = "Champion")]
public class Champion : ScriptableObject
{
    public new string name;
    public float color;
    public int team;
    public int speed;
    public int ap = 3;
    public int handlimit = 5;
    public string effect;
    public enum EffectType
    {
        None,
        Blinded,
        Charmed,
        Crippled,
        Immune,
        Stunned,
        Poisoned
    }
    public EffectType currentEffect;
    public int[] modifiers;
    public int[] vicdef;
    public string[] quests = new string[2];
    public Queue<Card> health = new Queue<Card>();
    public Stack<Card> damage = new Stack<Card>();
    public Stack<Card> discard = new Stack<Card>();
    public Card[] cardlist;
    public List<Card> hand;
    public List<Card> enchants;
    public int fleetcount = 0;

    public void PopulateDeck()
    {
        health.Clear();
        damage.Clear();
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
            int k = UnityEngine.Random.Range(0, n + 1);
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

    public void Discard(Card card)
    {
        hand.Remove(card);
        discard.Push(card);
    }

    public void Damage(int dmg)
    {
        if (dmg - modifiers[1] < 1)
        {
            this.damage.Push(this.health.Dequeue());
            return;
        }
        for (int i = 0; i < dmg - modifiers[1]; i++)
        {
            this.damage.Push(this.health.Dequeue());
        }
    }

    public void TrueDamage(int dmg)
    {
        for (int i = 0; i < dmg; i++)
        {
            this.damage.Push(this.health.Dequeue());
        }
    }

    public void Heal(int heal)
    {
        if (this.damage.Count >= heal)
        {
            for (int i = 0; i < heal; i++)
            {
                this.health.Enqueue(this.damage.Pop());
            }
        }
        else
        {
            while (this.damage.Count > 0)
            {
                this.health.Enqueue(this.damage.Pop());
            }
        }
        
    }

    public void Play(Card card, List<Piece> pieces)
    {
        card.Play(pieces);
        ap -= Math.Max(0, card.GetAP());
        Discard(card);
        Debug.Log(card.name + " was played!");
    }

    public void Shutdown()
    {
        this.health = null;
        hand.RemoveRange(0, hand.Count - 1);
    }

    public void ApplyEffect(EffectType effect)
    {
        if (currentEffect != EffectType.Immune)
        {
            RemoveEffect();
            currentEffect = effect;
            switch (effect)
            {
                case EffectType.Blinded:
                    modifiers[0] -= 2;
                    break;
                case EffectType.Charmed:
                    modifiers[1] -= 2;
                    break;
                case EffectType.Crippled:
                    speed -= 2;
                    break;
                case EffectType.Stunned:
                    ap = 2;
                    break;

            }
        } else
        {
            currentEffect = EffectType.None;
        }
        
    }

    public void RemoveEffect()
    {
        switch (currentEffect)
        {
            case EffectType.Blinded:
                modifiers[0] += 2;
                break;
            case EffectType.Charmed:
                modifiers[1] += 2;
                break;
            case EffectType.Crippled:
                speed += 2;
                break;
            case EffectType.Stunned:
                ap = 3;
                break;
        }
        currentEffect = EffectType.None;
    }

    public void Reset()
    {
        ap = 3;
        handlimit = 5;
        modifiers[0] = 0;
        modifiers[1] = 0;
        vicdef[0] = 0;
        vicdef[1] = 0;
        fleetcount = 0;
        hand.Clear();
        enchants.Clear();
    }

}
