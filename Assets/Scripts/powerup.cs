using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] protected float duration = 15f;
    [SerializeField] private GameObject collectEffect;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || !other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            ApplyPowerup(player);
            PlayCollectEffect();
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyPowerup(Player player);

    private void PlayCollectEffect()
    {
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
    }
}
