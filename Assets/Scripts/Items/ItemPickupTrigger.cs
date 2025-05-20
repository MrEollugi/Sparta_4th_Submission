using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupTrigger : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInventory>()?.Pickup(itemData);
            Destroy(gameObject);
        }
    }
}
