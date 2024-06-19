using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/playerState")]
public class playerStats : ScriptableObject
{
    public float health = 100f;
    public float mana = 5f;

    public float baseDamage = 15f;
    public float attackSpeed = .4f;
}
