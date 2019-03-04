using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public enum CardType
    {
        Attack,
        Ability,
        Enchant
    }
    public CardType cardtype;
    public int damage;
    public int ap;
    public int range;
    public string target;
    public string effect;
    public Sprite image;
    public int copies;

    public string GetName()
    {
        return this.name;
    }
    public void SetName(string value)
    {
        this.name = value;
    }
    public int GetAP()
    {
        return this.ap;
    }
    public void SetAP(int value)
    {
        this.ap = value;
    }
    public int GetRange()
    {
        return this.range;
    }
    public void SetRange(int value)
    {
        this.range = value;
    }
    public string GetTarget()
    {
        return this.target;
    }
    public void SetTarget(string value)
    {
        this.target = value;
    }
    public string GetEffect()
    {
        return this.effect;
    }
    public void SetEffect(string value)
    {
        this.effect = value;
    }

    public Card(string nam, int _ap, int ran, string tar, string eff, Sprite img)
    {
        this.name = nam;
        this.ap = _ap;
        this.range = ran;
        this.target = tar;
        this.effect = eff;
        this.image = img;
    }
}
