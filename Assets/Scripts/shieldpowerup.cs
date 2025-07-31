using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : Powerup
{
    [Header("Shield Settings")]
    [SerializeField] private Color shieldColor = Color.cyan;

    protected override void ApplyPowerup(Player player)
    {
        if (player == null) return;

        //player.ActivateShield(duration, shieldColor);
    }
}
