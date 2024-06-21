using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactiveEEEEEE : MonoBehaviour
{   float dist = 10000f;
    private void Update()
    {
        float dist = Vector3.Distance(this.transform.position, playerMain.Player.transform.position);
    }
    public LayerMask uiLayer;
    [SerializeField] GameObject motherShip;   
    public void spawn ( )
    {
        if ( dist < 4f)
        {
            Vector3 position = transform.position + Vector3.up * 2f;
            GameObject ebutton = Instantiate(motherShip, position, Quaternion.identity);
           
        }
    }
}
