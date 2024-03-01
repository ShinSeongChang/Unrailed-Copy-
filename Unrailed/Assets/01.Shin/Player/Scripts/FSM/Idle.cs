using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerBaseNode
{

    float time = default;
    public override void OnStateEnter(FSMTest stateManager)
    {
        Debug.Log("���� ����");
        time = 0f;
    }


    public override void OnStateUpdate(FSMTest stateManager)
    {
        time += Time.deltaTime;
        Debug.Log("�ð���� : " + time);

        if(time >= 5f)
        {
            time = 0f;
            stateManager.ChangedState(PlayerState.Interact);
        }
    }
    public override void OnStateExit(FSMTest stateManager)
    {
        Debug.Log("5�� ���, Idle Ż��");
        stateManager.ChangedState(PlayerState.Interact);
    }
}
