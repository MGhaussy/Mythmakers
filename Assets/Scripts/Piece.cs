using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public Space myspace;
    public Champion mychamp;
    public Board myboard;
    public bool isTurn = false;
    public bool hasMoved = false;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = myspace.transform.position;
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
        hasMoved = true;
    }

    private void OnMouseDown()
    {
        Debug.Log(this.GetRange(this.myspace, this.mychamp.speed).Count());
        if (isTurn && !hasMoved)
        {
            foreach (Space space in this.GetRange(this.myspace, this.mychamp.speed))
                {
                space.Toggle();
                }
        }
        
    }

    public bool CanPlay(Card card)
    {
        foreach (Piece piece in myboard.piecelist)
        {

        }
        return true;
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
