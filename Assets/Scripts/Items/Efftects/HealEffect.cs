using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Effects/Heal Effect")]
public class HealEffect : ItemEffect
{
    public float healAmount;
    public override void ApplyEffect(Player player)
    {
        player.Stats.Heal(healAmount);
    }
}
