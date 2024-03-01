using System;
using UnityEngine;

public abstract class PlayerBaseNode : MonoBehaviour
{
    public abstract void OnStateEnter(FSMTest stateManager);
    public abstract void OnStateUpdate(FSMTest stateManager);
    public abstract void OnStateExit(FSMTest stateManager);
}
