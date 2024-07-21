using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cross : MonoBehaviour
{
    SpriteRenderer mask;

    void Start()
    {
        SpriteRenderer [] objectArray = GetComponentsInChildren<SpriteRenderer>();
        mask = objectArray[1];
        mask.enabled = false;
    }

    void Update()
    {
        float dist = Vector3.Distance(this.transform.position, playerMain.Player.transform.position);
        if (dist < 2f)
        {
            mask.enabled = true;
            if (Input.GetKey(KeyCode.E))
            {
                FindObjectOfType<SaveLoad>().SaveGame();
            }
        }
        else
            mask.enabled = false;
    }
}
