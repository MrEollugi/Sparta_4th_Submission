using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupTrigger : MonoBehaviour
{
    [SerializeField] public ItemData itemData;

    #region Trigger Logic

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���� ������ ���Դ��� Ȯ��
        if (other.CompareTag("Player"))
        {
            var inventory = other.GetComponent<PlayerInventory>();

            // �κ��丮 ������Ʈ�� ������ ��쿡�� ó��
            if (inventory != null)
            {
                // ������ ȹ�濡 �����ϸ� Ʈ���� ������Ʈ ����
                bool success = inventory.Pickup(itemData);
                if (success)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    #endregion
}
