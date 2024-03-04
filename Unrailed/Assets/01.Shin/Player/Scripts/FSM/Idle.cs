using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Idle : PlayerSelector
{
    float time = 0;

    public Idle(PlayerBehavior player) : base(player)
    {
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateUpdate()
    {
        time += Time.deltaTime;
        behavior.Move();

        if(time >= 5f)
        {
            time = 0;
            OnStateExit();
        }
    }

    public override void OnStateExit()
    {        
        behavior.ChangeNode(playerState.Interact);
    }

}
