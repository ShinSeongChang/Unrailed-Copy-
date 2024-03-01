using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerBaseNode
{

    float time = default;
    public override void OnStateEnter(FSMTest stateManager)
    {
        Debug.Log("게임 진입");
        time = 0f;
    }


    public override void OnStateUpdate(FSMTest stateManager)
    {
        time += Time.deltaTime;
        Debug.Log("시간경과 : " + time);

        if(time >= 5f)
        {
            time = 0f;
            stateManager.ChangedState(PlayerState.Interact);
        }
    }
    public override void OnStateExit(FSMTest stateManager)
    {
        Debug.Log("5초 경과, Idle 탈출");
        stateManager.ChangedState(PlayerState.Interact);
    }
}
