using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract: MonoBehaviour, IPlayerInputReceiver
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactRange = 3f;

    public void OnMove(Vector2 direction) { }
    public void OnJump() { }

    public void OnInteract()
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            Debug.Log($"[PlayerInteract] 상호작용: {hit.collider.name}");

            if(hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }
    }
}
