using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public int myteam;
    public bool isActive = true;
    public Space[] myspaces;
    public Board myboard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsCaptured()
    {
        int count = 0;
        foreach (Piece piece in myboard.piecelist)
        {
            if (myspaces.Contains(piece.myspace))
            {
                if (myteam == piece.mychamp.team)
                {
                    count++;
                }
                else
                {
                    count--;
                }
            }
        }
        return count < 0;
    }

    public void Extinguish()
    {
        isActive = false;
        this.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        this.transform.GetChild(1).GetComponent<Light>().enabled = false;
    }
}
