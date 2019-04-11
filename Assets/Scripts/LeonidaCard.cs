using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New LeoCard", menuName = "LeoCard")]
public class LeonidaCard : Card
{


    public override void Play(List<Piece> targets)
    {
        switch (cardtype)
        {
            case CardType.Ability:
                switch (name)
                {
                    case "Combat Maneuvers":
                        targets[0].myboard.overlay.transform.GetChild(13).GetComponent<CanvasGroup>().alpha = 1;
                        break;
                    case "Etherial Shield":
                        targets[0].mychamp.modifiers[1] += 1;
                        targets[0].mychamp.ApplyEffect(Champion.EffectType.Immune);
                        break;
                    case "Strategic Planning":
                        targets[0].myboard.overlay.Draw();
                        targets[0].myboard.overlay.Draw();
                        targets[0].myboard.overlay.discardcount = 2;
                        break;
                    case "Into Formation":
                        targets[0].myboard.currentPiece.hasMoved++;
                        foreach (Space space in targets[0].myboard.currentPiece.GetRange(targets[0].myboard.currentPiece.myspace, 2))
                        {
                            space.Toggle();
                        }
                        break;
                    case "Taunt":
                        targets[0].myboard.movePiece = targets[0];
                        foreach (Space space in targets[0].GetRange(targets[0].myspace, targets[0].mychamp.speed))
                        {
                            space.Toggle();
                        }
                        break;
                }
                Debug.Log("Ability");

                break;
            case CardType.Attack:
                Debug.Log("Attack");
                
                switch (name)
                {
                    case "Coordinated Slash":
                        foreach (Piece ally in targets[0].myboard.currentPiece.Allies())
                        {
                            ally.Activate();
                            ally.situation = 1;
                        }
                        break;
                    case "Defensive Blow":
                        mychamp.ApplyEffect(Champion.EffectType.Immune);
                        break;
                    case "Pinpoint Strike":
                        if(targets[0].Neighbors().Count == 1)
                        {
                            damage += 2;
                        }
                        break;
                    case "Synchronized Assault":
                        foreach (Piece ally in targets[0].myboard.currentPiece.Allies())
                        {
                            ally.Activate();
                            ally.situation = 2;
                        }
                        break;
                }
                foreach (Piece piece in targets)
                {
                    piece.mychamp.Damage(this.GetDamage());
                }
                break;
            case CardType.Enchant:
                Debug.Log("Enchant");
                mychamp.enchants.Add(this);
                break;
        }
    }

    public override int GetAP()
    {
        return ap;
    }
}
