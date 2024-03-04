using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSelector : MonoBehaviour
{
    protected PlayerBehavior player;

    public PlayerSelector(PlayerBehavior player)
    {
        this.player = player;
    }

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();

}
