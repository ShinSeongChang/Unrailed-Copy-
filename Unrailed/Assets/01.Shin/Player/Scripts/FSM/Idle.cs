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
        Debug.Log("Idle 진입");
    }

    public override void OnStateUpdate()
    {
        behavior.Move();
    }

    public override void OnStateExit()
    {        
        behavior.ChangeNode(playerState.Interact);
    }

    public override void OnStateAction()
    {
        Debug.LogWarning("아무것도 들고있지 않은 상태입니다!");
    }

}
