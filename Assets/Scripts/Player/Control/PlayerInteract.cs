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
        // ī�޶� �Ǵ� ĳ���� �������� Ray ���� (�Ӹ� ���̿��� ������)
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        // Raycast�� ��ȣ�ۿ� ������ ��� ����
        if(Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            // ������ ������Ʈ�� IInteractable �������̽��� �����ߴ��� Ȯ��
            if(hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }
    }

    #endregion
}
