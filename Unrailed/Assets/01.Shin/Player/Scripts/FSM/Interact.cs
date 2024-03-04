using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Interact : ActionSelelector<PlayerBehavior>
{
    public Interact(PlayerBehavior player) : base(player) { }

    public override void OnStateEnter()
    {
        Debug.Log("두번째 스테이트 진입");
    }


    public override void OnStateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            OnStateExit();
        }
    }
    public override void OnStateExit()
    {
        behavior.ChangeNode(playerState.Idle);
    }

}
