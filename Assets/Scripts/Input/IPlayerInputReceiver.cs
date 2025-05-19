using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPlayerInputReceiver
{
    void OnMove(Vector2 direction);
    void OnJump();
    void OnInteract();
}