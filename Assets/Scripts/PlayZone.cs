using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class PlayZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Overlay overlay;
    public void OnPointerEnter(PointerEventData data)
    {

    }
    public void OnPointerExit(PointerEventData data)
    {

    }

    public void OnDrop(PointerEventData data)
    {

        CardDisplay card = data.pointerDrag.GetComponent<CardDisplay>();
        Piece currentPiece = overlay.board.currentPiece;
        if (overlay.display.childCount == 0 && currentPiece.CanPlay(card.card))
        {
        overlay.mytargets.Clear();
        data.pointerDrag.transform.SetParent(overlay.display);
        currentPiece.currentcard = data.pointerDrag;
        if (card.card.name == "Urgent Message")
            {
                foreach(Piece piece in overlay.board.piecelist)
                {
                    Debug.Log(piece.name);
                    if (card.card.IsRightType(piece.mychamp))
                    {
                        piece.Activate();
                    }
                }
            }
        else
            {
                foreach (Piece piece in overlay.board.piecelist)
                {
                    // If piece is in range AND is the right type (Enemy, Ally, Self)
                    if (currentPiece.GetRange(currentPiece.myspace, card.card.range).Contains(piece.myspace) && card.card.IsRightType(piece.mychamp))
                    {
                        piece.Activate();
                    }
                }
            }
        
        }
        
    }
}
