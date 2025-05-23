using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    #region JumpPad Settings

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private string playerTag = "Player";

    #endregion

    #region Unity Callbacks

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    #endregion
}
