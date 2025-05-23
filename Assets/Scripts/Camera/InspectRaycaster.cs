using UnityEngine;

public class InspectRaycaster : MonoBehaviour
{
    #region Inspector Settings
    // ������ �ν� �Ÿ�
    [SerializeField] private float inspectDistance = 5f;
    
    #endregion

    #region Unity Callbacks

    private void Update()
    {
        // ī�޶� 1��Ī ����� �� ����ĳ��Ʈ �õ�
        Ray? ray = CameraController.Instance.TryGetInspectRay();

        if(ray.HasValue && Physics.Raycast(ray.Value, out RaycastHit hit, 3f))
        {
            // ��Ʈ�� ������Ʈ�� InspactableItem�̸� UI ǥ��
            if(hit.collider.TryGetComponent<InspectableItem>(out var item))
            {
                InspectUIManager.Instance.Show(item.itemData);
                return;
            }
        }
        // ���� ����� �� UI �����
        InspectUIManager.Instance.Hide();
    }

    #endregion
}