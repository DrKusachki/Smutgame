using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGroundedCheck : MonoBehaviour
{
    public bool isGrounded;
    private void OnTriggerStay2D(Collider2D collision)
    {
        LayerMask mask = LayerMask.GetMask("Wall", "Ground");
        if (collision.gameObject.layer == mask)
            isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        LayerMask mask = LayerMask.GetMask("Wall", "Ground");
        if (collision.gameObject.layer == mask)
            isGrounded = false;
    }
}
