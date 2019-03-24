using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Space : MonoBehaviour
{
    public Space[] neighbors;
    public Board myboard;
    public bool isClickable = false;
    public Piece mypiece;
    public enum SpaceType
    {
        White,
        Blue,
        Red,
        Green
    }
    public SpaceType spacetype;

    public Space[] GetNeighbors(Space space)
    {
        return space.neighbors;
    }

    private void OnMouseDown()
    {
        //Move the current Piece to this position.
        if (isClickable)
        {
            this.myboard.currentPiece.Move(this);
        }
    }

    public void Toggle()
    {
        isClickable = !isClickable;
        transform.GetChild(0).GetComponent<Light>().enabled = !transform.GetChild(0).GetComponent<Light>().enabled;
    }


}
