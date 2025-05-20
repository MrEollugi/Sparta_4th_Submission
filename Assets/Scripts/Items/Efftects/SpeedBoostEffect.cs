using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Effects/Speed Boost")]
public class SpeedBoostEffect : ItemEffect
{
    public float boostAmount;
    public float duration;

    public override void ApplyEffect(Player player)
    {
        player.StartCoroutine(player.Movement.ApplySpeedBoost(boostAmount, duration));
    }
}
