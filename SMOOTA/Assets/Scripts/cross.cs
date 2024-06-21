using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cross : MonoBehaviour
{
    SpriteRenderer crossSprite;
    SpriteRenderer mask; 
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer [] massiveaf = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in massiveaf)
        {
            if (sr.gameObject.name == "cross")
            {
                crossSprite = sr;
            }
            else
            {
                mask = sr;
            }
        }
        mask.enabled = false;
        crossSprite.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(this.transform.position, playerMain.Player.transform.position);
        if (dist < 2f && Input.GetKey(KeyCode.E))
        {
            mask.enabled = true;
            crossSprite.enabled = false;
            FindObjectOfType<SaveLoad>().SaveGame();
            

        }
    }
}
