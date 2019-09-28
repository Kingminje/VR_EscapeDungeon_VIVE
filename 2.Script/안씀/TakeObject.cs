using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObject : MonoBehaviour
{
    public Transform player_Tr = null;
    public Transform movePoint_Tr = null;

    public Transform PlayerSet
    {
        get { return this.player_Tr; }
        set { this.player_Tr = value; }
    }

    public Transform MoveSet
    {
        get { return this.movePoint_Tr; }
        set { this.movePoint_Tr = value; }
    }

    public void MovePlayerAndBoxTogether()
    {
    }
}