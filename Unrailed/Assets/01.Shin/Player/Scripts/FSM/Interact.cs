using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Interact : ActionSelelector<PlayerBehavior>
{
    public Interact(PlayerBehavior player) : base(player) { }

    public override void OnStateEnter()
    {
        Debug.Log("Interact 진입");
    }


    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        behavior.ChangeNode(playerState.Idle);
    }

}
