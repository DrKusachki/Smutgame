using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMain : MonoBehaviour
{
    [SerializeField] public playerStats stats;

    private static playerMain _Player;
    public static playerMain Player { get { return _Player;  } }

    private void Start()
    {
        if (Player != this && _Player != this)
            Destroy(this);
        else
            _Player = this;
    }

}
