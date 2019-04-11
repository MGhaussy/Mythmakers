using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public abstract class Card : ScriptableObject
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
    public string effect;
    public Sprite image;
    public int copies;




    public abstract int GetAP();

    public bool IsRightType(Champion champ)
    {
        foreach (TargetType targ in this.targets)
        {
            switch (targ)
            {
                case TargetType.Enemy:
                    if (mychamp.team != champ.team)
                    {
                        return true;
                    }
                    break;
                case TargetType.Ally:
                    if (mychamp.team == champ.team && mychamp.color != champ.color)
                    {
                        return true;
                    }
                    break;
                default:
                    return mychamp.color == champ.color;
            }
        }
        return false;
        
    }

    public abstract void Play(List<Piece> targets);


    public int GetDamage()
    {
        return damage + mychamp.modifiers[0];
    }
}
