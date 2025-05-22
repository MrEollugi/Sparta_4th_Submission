using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPlayerInputReceiver
{
    void OnMove(Vector2 direction);
    void OnJump();
    void OnInteract();
    void OnSwitchView();
    void OnUseItem();
    void OnDropItem();
    void OnSwitchSlot();
    void OnDash();

}