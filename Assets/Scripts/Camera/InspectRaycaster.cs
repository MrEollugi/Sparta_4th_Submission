using UnityEngine;

public class InspectRaycaster : MonoBehaviour
{
    #region Inspector Settings
    // 아이템 인식 거리
    [SerializeField] private float inspectDistance = 5f;
    
    #endregion

    #region Unity Callbacks

    private void Update()
    {
        // 카메라가 1인칭 모드일 때 레이캐스트 시도
        Ray? ray = CameraController.Instance.TryGetInspectRay();

        if(ray.HasValue && Physics.Raycast(ray.Value, out RaycastHit hit, 3f))
        {
            // 히트된 오브젝트가 InspactableItem이면 UI 표시
            if(hit.collider.TryGetComponent<InspectableItem>(out var item))
            {
                InspectUIManager.Instance.Show(item.itemData);
                return;
            }
        }
        // 조건 불충분 시 UI 숨기기
        InspectUIManager.Instance.Hide();
    }

    #endregion
}