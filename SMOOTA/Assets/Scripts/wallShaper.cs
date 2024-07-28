using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallShaper : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().size = new Vector2(transform.localScale.x, transform.localScale.y);
        GetComponent<BoxCollider2D>().size = new Vector2(transform.localScale.x, transform.localScale.y);
        transform.localScale = Vector2.one;
        Destroy(this);
    }
}
