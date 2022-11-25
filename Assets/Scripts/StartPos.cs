using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    public int Startmappos;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        if(Startmappos == player.PreviousMapNum)
        {
            player.transform.position = this.transform.position;
        }
    }

}
