using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board :  MonoBehaviour

{
    public Space[] board;
    public Piece[] piecelist;
    public Flame[] flames;
    public Queue<Piece> pieces = new Queue<Piece>();
    public Piece currentPiece;

    private void Start()
    {
        foreach (Piece piece in piecelist)
        {
            pieces.Enqueue(piece);
        }
        currentPiece = pieces.Dequeue();
        currentPiece.isTurn = true;
    }
    public Space[] GetNeighbors(Space space)
    {
        return space.neighbors;
    }

    public void TurnOffLights()
    {
        foreach (Space space in board)
        {
            space.isClickable = false;
            space.transform.GetChild(0).GetComponent<Light>().enabled = false;
        }
    }

    public void NextChamp()
    {
        pieces.Enqueue(currentPiece);
        currentPiece.isTurn = false;
        currentPiece.hasMoved = false;
        currentPiece = pieces.Dequeue();
        currentPiece.isTurn = true;
    }

}
