using UnityEngine;

public class InspectRaycaster : MonoBehaviour
{
    [SerializeField] private float inspectDistance = 5f;

    private void Update()
    {
        Ray? ray = CameraController.Instance.TryGetInspectRay();

        if(ray.HasValue && Physics.Raycast(ray.Value, out RaycastHit hit, 3f))
        {
            if(hit.collider.TryGetComponent<InspectableItem>(out var item))
            {
                InspectUIManager.Instance.Show(item.itemData);
                return;
            }
        }
        InspectUIManager.Instance.Hide();
    }
}
