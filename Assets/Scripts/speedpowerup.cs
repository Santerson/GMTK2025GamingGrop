using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : Powerup
{
    [Header("Speed Settings")]
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private ParticleSystem speedLinesEffect;

    protected override void ApplyPowerup(Player player)
    {
        if (player == null) return;

        player.ActivateSpeedBoost(speedMultiplier, duration);
        PlaySpeedEffect(player.transform);
    }

    private void PlaySpeedEffect(Transform playerTransform)
    {
        if (speedLinesEffect != null)
        {
            var effect = Instantiate(speedLinesEffect, playerTransform);
            effect.transform.localPosition = Vector3.zero;
            Destroy(effect.gameObject, duration);
        }
    }
}
