using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New OgakiCard", menuName = "OgakiCard")]
public class OgakiCard : Card
{

    public override void Play(List<Piece> targets)
    {
        switch (cardtype)
        {
            case CardType.Ability:
                switch (name)
                {
                    case "Blow A Kiss":
                        targets[0].mychamp.ApplyEffect(Champion.EffectType.Charmed);
                        break;
                }
                break;
            case CardType.Attack:
                switch (name)
                {

                }
                break;
            case CardType.Enchant:
                targets[0].mychamp.enchants.Add(this);
                break;
        }
    }

    public override int GetAP()
    {
        return ap;
    }
}

