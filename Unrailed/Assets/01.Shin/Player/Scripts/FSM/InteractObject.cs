using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : PlayerBaseNode
{

    public override void OnStateEnter(FSMTest stateManager)
    {
        Debug.Log("ObjectInteract ¡¯¿‘");
    }

    public override void OnStateUpdate(FSMTest stateManager)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            stateManager.ChangedState(PlayerState.Move);
        }
    }
    public override void OnStateExit(FSMTest stateManager)
    {
        Debug.Log("Interact ≈ª√‚");
        stateManager.ChangedState(PlayerState.Move);
    }


}
