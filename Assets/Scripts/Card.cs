using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public Champion mychamp;
    public enum CardType
    {
        Attack,
        Ability,
        Enchant
    }
    public CardType cardtype;
    public enum TargetType
    {
        Enemy,
        Ally,
        Self
    }
    public TargetType[] targets;
    public int damage;
    public int ap;
    public int range;
    public string target;
    public string effect;
    public Sprite image;
    public int copies;


    public Card(string nam, int _ap, int ran, string tar, string eff, Sprite img)
    {
        this.name = nam;
        this.ap = _ap;
        this.range = ran;
        this.target = tar;
        this.effect = eff;
        this.image = img;
    }


    //public int CalculateDamage()
    //{

    //}
}
