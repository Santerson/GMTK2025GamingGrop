using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    [SerializeField] private float speedIncrease = 12f;
    [SerializeField] private float duration = 10f;

    public void activate()
    {
        FindObjectOfType<Player>().temporarySpeedUp(speedIncrease, duration);
        Destroy(transform.parent.gameObject);
    }
}
