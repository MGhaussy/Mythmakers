using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Board", menuName = "Board")]
public class Board : ScriptableObject
{
    public Space[] board;

    public Space[] GetNeighbors(Space space)
    {
        return space.neighbors;
    }
}
