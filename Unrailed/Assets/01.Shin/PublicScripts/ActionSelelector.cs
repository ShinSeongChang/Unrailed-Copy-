using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionSelelector<T> where T : MonoBehaviour
{
    protected T behavior;

    public ActionSelelector(T behavior)
    {
        this.behavior = behavior;
    }

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();
}
