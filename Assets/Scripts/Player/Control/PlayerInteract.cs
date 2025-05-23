using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract: MonoBehaviour
{
    #region Interact Settings

    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactRange = 3f;

    #endregion

    #region Input Interface (Unused)

    public void OnMove(Vector2 direction) { }
    public void OnJump() { }

    #endregion

    #region Interact Logic
    public void OnInteract()
    {
        // 카메라 또는 캐릭터 기준으로 Ray 생성 (머리 높이에서 앞으로)
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        // Raycast로 상호작용 가능한 대상 감지
        if(Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            // 감지된 오브젝트가 IInteractable 인터페이스를 구현했는지 확인
            if(hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }
    }

    #endregion
}
