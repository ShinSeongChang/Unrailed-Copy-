using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Idle : PlayerSelector
{
    public Idle(PlayerBehavior player) : base(player)
    {
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateUpdate()
    {
        behavior.Move();
    }

    public override void OnStateExit()
    {        
        behavior.ChangeNode(playerState.Interact);
    }

}
