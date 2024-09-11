using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    private void Update()
    {
        try
        {
            if (playerMain.Player.transform.position.y - 1f < this.transform.position.y + .25f)
            {
                gameObject.layer = LayerMask.NameToLayer("PlayerPassable");
                gameObject.tag = "Platform";
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Default");
                gameObject.tag = "Ground";
            }
        }
        catch { }
    }
}
