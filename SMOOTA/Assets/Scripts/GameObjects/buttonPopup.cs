using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPopup : MonoBehaviour
{   
    [SerializeField] GameObject button;
    private GameObject buttonInstance;
    
    private float dist => Vector3.Distance(transform.position, playerMain.Player.transform.position);
    private Vector3 buttonPos => transform.position + Vector3.up * 1.5f;

    private void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            if (dist <= 2f && buttonInstance == null)
            {
                spawn();
            }
            if(buttonInstance != null)
            {
                if (dist > 2f)
                    despawn();
            }
        }
        if(buttonInstance != null)
            movePopup();
    }

    private void spawn ()
    {
        buttonInstance = Instantiate(button, buttonPos, Quaternion.identity);
    }

    private void despawn()
    {
        Destroy(buttonInstance);
    }

    private void movePopup()
    {
        buttonInstance.transform.position = buttonPos;
    }
}
