using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Space : MonoBehaviour
{
    public Space[] neighbors;
    public enum SpaceType
    {
        White,
        Blue,
        Red,
        Green
    }
    public SpaceType spacetype;
}
