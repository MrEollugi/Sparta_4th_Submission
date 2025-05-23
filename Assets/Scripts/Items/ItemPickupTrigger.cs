using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupTrigger : MonoBehaviour
{
    [SerializeField] public ItemData itemData;

    #region Trigger Logic

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거 안으로 들어왔는지 확인
        if (other.CompareTag("Player"))
        {
            var inventory = other.GetComponent<PlayerInventory>();

            // 인벤토리 컴포넌트가 존재할 경우에만 처리
            if (inventory != null)
            {
                // 아이템 획득에 성공하면 트리거 오브젝트 제거
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
