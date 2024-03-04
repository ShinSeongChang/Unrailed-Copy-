using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Interact : PlayerSelector
{
    public Interact(PlayerBehavior player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("�ι�° ������Ʈ ����");
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
        player.ChangeNode(playerState.Idle);
    }

}
