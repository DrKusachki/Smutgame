using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/projectile")]
public class projectileInfo : ScriptableObject
{
    [Header("Physics")]
    public float projectileSpeed;
    public bool affectedByGravity;
    [Header("Damage")]
    public float projectileDamage;
    [Header("Misc")]
    public bool isParryable;
}
