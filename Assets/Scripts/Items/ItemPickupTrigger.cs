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
            var inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                bool success = inventory.Pickup(itemData);
                if (success)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
