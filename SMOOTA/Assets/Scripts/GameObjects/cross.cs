using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cross : MonoBehaviour
{
    SpriteRenderer mask;
    private float dist => Vector3.Distance(this.transform.position, playerMain.Player.transform.position);

    void Start()
    {
        SpriteRenderer [] objectArray = GetComponentsInChildren<SpriteRenderer>();
        mask = objectArray[1];
        mask.enabled = false;
    }

    void Update()
    {
        if (dist < 2f)
        {
            mask.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(Application.persistentDataPath);
                FindObjectOfType<SaveLoad>().SaveGame();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                FindObjectOfType<SaveLoad>().LoadGame();
            }
        }
        else
            mask.enabled = false;
    }
}
