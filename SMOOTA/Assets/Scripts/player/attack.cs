using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public float damage;
    private void Start()
    {
        StartCoroutine(delete());
    }

    public void setDamage(float dmg)
    {
        damage = dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            if (collision.gameObject.tag != "Player")
                collision.gameObject.GetComponent<IDamageable>().takeDamage(damage);
        }
        catch { }
    }

    IEnumerator delete()
    {
        yield return new WaitForSeconds(FindObjectOfType<playerMain>().stats.attackSpeed);
        Destroy(gameObject);
        yield return null;
    }
}
