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
        Debug.Log("����");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("�ð� : " + time);
        player.Move();
        time += Time.deltaTime;

        if(time >= 5f)
        {
            time = 0;
            OnStateExit();
        }
    }

    public override void OnStateExit()
    {        
        player.ChangeNode(playerState.Interact);
    }

}
