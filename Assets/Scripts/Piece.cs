using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public Space myspace;
    public Champion mychamp;
    public Board myboard;
    public bool isTurn;
    public int hasMoved;
    public bool isClickable;
    public int situation;
    public GameObject currentcard;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = myspace.transform.position;
        hasMoved = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Space space)
    {
        this.myspace = space;
        this.transform.position = myspace.transform.position;
        this.myboard.TurnOffLights();
        hasMoved--;
    }

    public List<Piece> Neighbors()
    {
        List<Piece> neighbors = new List<Piece>();
        foreach (Piece piece in myboard.piecelist)
        {
            if (myspace.GetNeighbors().Contains(piece.myspace))
            {
                neighbors.Add(piece);
            }
        }
        return neighbors;
    }

    public List<Piece> Allies()
    {
        List<Piece> allies = new List<Piece>();
        foreach (Piece piece in myboard.pieces)
        {
            if (piece.mychamp.team == mychamp.team)
            {
                allies.Add(piece);
            }
        }
        return allies;
    }

    public List<Piece> Enemies()
    {
        List<Piece> enemies = new List<Piece>();
        foreach (Piece piece in myboard.pieces)
        {
            if (piece.mychamp.team != mychamp.team)
            {
                enemies.Add(piece);
            }
        }
        return enemies;
    }

    private void OnMouseDown()
    {
        Debug.Log(this.GetRange(this.myspace, this.mychamp.speed).Count());
        if (isTurn && hasMoved > 0 && !isClickable)
        {
            myboard.movePiece = this;
            foreach (Space space in this.GetRange(this.myspace, this.mychamp.speed))
                {
                    space.Toggle();
                }
        }
        else if (isClickable)
        {
            switch (situation)
            {
                case 1:
                    this.mychamp.modifiers[0] += 1;
                    break;
                case 2:
                    this.myboard.movePiece = this;
                    foreach (Space space in this.GetRange(myspace, 2))
                    {
                        space.Toggle();
                    }
                    break;
                default:
                    myboard.overlay.SetTarget(this);
                    myboard.overlay.Play(myboard.currentPiece.currentcard);
                    break;
            }
            foreach (Piece piece in myboard.pieces)
            {
                piece.Deactivate();
            }
            
        }
        
    }

    public void Activate()
    {
        isClickable = true;
        myspace.LightOn();
    }

    public void Deactivate()
    {
        isClickable = false;
        myspace.LightOff();
    }

    public bool CanPlay(Card card)
    {
        Debug.Log(mychamp.ap.ToString());
        Debug.Log(card.GetAP().ToString());
        if (mychamp.ap >= card.GetAP())
        {
            if (card.range == 0)
            {
                return true;
            }
            foreach (Piece piece in myboard.piecelist)
            {
                if (this.GetRange(this.myspace, card.range).Contains(piece.myspace) && card.IsRightType(piece.mychamp))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    public Space[] GetRange(Space space, int range)
    {
        Space[] inrange = { space };
        if (range > 0)
        {
            foreach (Space neighbor in space.neighbors)
            {
                inrange = GetRangeHelper(neighbor, range - 1, inrange);
            }
        }
        return inrange;
    }

    public Space[] GetRangeHelper(Space space, int range, Space[] visited)
    {
        if (!visited.Contains(space))
        {
            Space[] thisspace = { space };
            var temp = visited.Concat(thisspace);
            visited = temp.ToArray();
        }
        if (range > 0)
        {
            foreach (Space neighbor in space.neighbors)
            {
                //if (!visited.Contains(neighbor))
                //{
                    visited = GetRangeHelper(neighbor, range - 1, visited);
                //}
            }
        }
        return visited;
    }
}
