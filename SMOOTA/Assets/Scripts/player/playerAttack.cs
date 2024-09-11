using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    public LayerMask enemyLayers;
    [SerializeField] GameObject Attack;

    public void attack(Vector2 direction)
    {
        Vector3 position = transform.position + Vector3.right * FindObjectOfType<playerMove>().Facing();
        GameObject Attack_prefab = Instantiate(Attack, position, Quaternion.Euler(0, 0, Mathf.Acos(Vector3.Dot(direction, Vector3.right))));
        Attack_prefab.GetComponent<attack>().setDamage(playerMain.Player.stats.meleeAttack["damage"]);
        
        Attack_prefab = null;
    }
    
}
