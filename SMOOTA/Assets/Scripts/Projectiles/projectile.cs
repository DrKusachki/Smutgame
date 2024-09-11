using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public projectileInfo info;
    Rigidbody2D rb;
    public Vector3 LookAt = Vector3.up;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = info.affectedByGravity ? 1f : 0f;
        rb.velocity = transform.up * info.projectileSpeed;
    }

    private void Update()
    {
        transform.LookAt(transform.position + (Vector3)rb.velocity, LookAt);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
