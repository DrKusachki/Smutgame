using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthDisplay : MonoBehaviour
{
    public playerMain player;
    RectTransform RT;
    private void Start()
    {
        RT = GetComponent<RectTransform>();
    }

    void Update()
    {
        RT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 32 * (player.stats.health / 100f));
    }
}
