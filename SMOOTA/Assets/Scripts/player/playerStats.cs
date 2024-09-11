using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/playerState")]
public class playerStats : ScriptableObject
{
    public float health = 100f;
    public float mana = 5f;

    public float baseDamage = 15f;
    public float attackSpeed = .4f;
    public Dictionary<string, float> meleeAttack = new Dictionary<string, float>
    {
        {"damage", 2f },
        {"speed", 1f },
        {"width", 2f},
        {"height", 4f },
        {"travelDist",3f }
    };
    public Dictionary<string, float> rangedAttack = new Dictionary<string, float>
    {
        {"damage", 2f },
        {"speed", 1f },
        {"width", 2f},
        {"height", 4f },
        {"travelDist",3f },
        {"travelSpeed", 2f }
    };
    public float souls = 0f;
    public float soulsPersistent = 0f;

    public Vector3 playerPosition = Vector3.zero;
}
