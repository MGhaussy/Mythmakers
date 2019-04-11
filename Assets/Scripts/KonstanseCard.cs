using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New KonstanseCard", menuName = "KonstanseCard")]
public class KonstanseCard : Card
{
    public int bonus;

    public override void Play(List<Piece> targets)
    {
        switch (cardtype)
        {
            case CardType.Ability:
                switch (name)
                {
                    case "Back Into The Fray":
                        targets[0].mychamp.Heal(5);
                        targets[0].hasMoved++;
                        targets[0].myboard.movePiece = targets[0];
                        foreach (Space space in targets[0].GetRange(targets[0].myspace, 3))
                        {
                            space.Toggle();
                        }
                        break;
                    case "Rush of Blood":
                        mychamp.TrueDamage(3);
                        mychamp.modifiers[0]++;
                        break;
                    case "Steel Resolve":
                        if (mychamp.damage.Count >= 10)
                        {
                            mychamp.modifiers[1] += 2;
                        }
                        break;
                }
                break;
            case CardType.Attack:
                switch (name)
                {
                    case "Bloody Murder":
                        if (mychamp.damage.Count >= 12)
                        {
                            targets[0].mychamp.Damage(bonus + this.GetDamage());
                        } else
                        {
                            targets[0].mychamp.Damage(this.GetDamage());
                        }
                        break;
                    case "Wild Slash":
                        if (mychamp.damage.Count >= 5)
                        {
                            targets[0].mychamp.Damage(bonus + this.GetDamage());
                        }
                        else
                        {
                            targets[0].mychamp.Damage(this.GetDamage());
                        }
                        break;
                    case "Thunderous Punch":
                        if (mychamp.damage.Count >= 7)
                        {
                            targets[0].mychamp.ApplyEffect(Champion.EffectType.Stunned);
                        }
                        targets[0].mychamp.Damage(this.GetDamage());
                        break;
                    case "Reckless Smash":
                        targets[0].myboard.currentPiece.mychamp.TrueDamage(3);
                        targets[0].mychamp.Damage(this.GetDamage());
                        break;
                    case "Leaping Attack":
                        targets[0].myboard.currentPiece.hasMoved++;
                        foreach (Space space in targets[0].GetRange(targets[0].myspace, 1))
                        {
                            if (targets[0].myboard.currentPiece.GetRange(targets[0].myboard.currentPiece.myspace, 2).Contains(space))
                            {
                                space.Toggle();
                            }
                        }
                        targets[0].mychamp.Damage(this.GetDamage());
                        break;
                    case "Pull Into Combat":
                        targets[0].mychamp.Damage(this.GetDamage());
                        targets[0].myboard.movePiece = targets[0];
                        foreach (Space space in targets[0].GetRange(targets[0].myspace, 2))
                        {
                            space.Toggle();
                        }
                        break;
                }
                break;
        }
    }

    public override int GetAP()
    {
        return ap;
    }
}
