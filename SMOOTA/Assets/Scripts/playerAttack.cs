using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    public LayerMask enemyLayers;
    [SerializeField] GameObject Attack;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            attack();
    }
    void attack()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 attackVector = (mousePos - (transform.position - transform.position.z * Vector3.forward)).normalized;
        Vector3 attackPosition = attackVector + (transform.position - transform.position.z * Vector3.forward);


        Instantiate(Attack, attackPosition, Quaternion.identity);
        Attack.transform.LookAt(attackPosition + attackVector);
        Attack.GetComponent<attack>().damage = playerMain.Player.stats.baseDamage;
    }
}
