using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New ArefCard", menuName = "ArefCard")]
public class ArefCard : Card
{
    public bool fleet;
    public bool isReduced;

    public override int GetAP()
    {
        if (isReduced)
        {
            return ap - mychamp.fleetcount;
        }
        return ap;
    }

    public override void Play(List<Piece> targets)
    {
        Debug.Log("It gets here." + targets[0].name);
        if (fleet)
        {
            mychamp.fleetcount++;
        }
        switch (cardtype)
        {
            case CardType.Ability:
                switch (name)
                {
                    case "Speed Boost":
                        targets[0].hasMoved++;
                        foreach (Space space in targets[0].GetRange(targets[0].myspace, 1))
                        {
                            space.Toggle();
                        }
                        break;
                    case "Quick Reflexes":
                        targets[0].myboard.overlay.Draw();
                        break;
                    case "Healing Touch":
                        targets[0].mychamp.Heal(3);
                        break;
                    case "Reserve of Energy":
                        targets[0].myboard.overlay.Draw();
                        targets[0].myboard.overlay.Draw();
                        break;
                    case "Cure All":
                        targets[0].mychamp.RemoveEffect();
                        break;
                    case "Urgent Message":
                        targets[0].myboard.currentPiece.hasMoved++;
                        foreach (Space space in targets[0].GetRange(targets[0].myspace, 1))
                        {
                            space.Toggle();
                        }
                        break;
                }
                Debug.Log("Ability");
                
                break;
            case CardType.Attack:
                Debug.Log("Attack");
                foreach (Piece piece in targets)
                {
                    piece.mychamp.Damage(this.GetDamage());
                }
                break;
        }
    }







}
