using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTest : MonoBehaviour
{
    PlayerState state;

    PlayerBaseNode currentState;
    Idle idle;
    InteractObject interactObject;
    Move Move;

    private void Awake()
    {
        idle = new Idle();
        interactObject = new InteractObject();
        Move = new Move();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = idle;
        currentState.OnStateEnter(this);
    }

    private void Update()
    {        
        currentState.OnStateUpdate(this);
    }

    private void LateUpdate()
    {
    }

    public void ChangedState(PlayerState state)
    {
        if(this.state == state) { return; }

        switch (state)
        {
            case PlayerState.Idle:
                currentState = idle;
                break;
            case PlayerState.Interact:
                currentState = interactObject;
                break;
            case PlayerState.Move:
                currentState = Move;
                break;
        }

        currentState.OnStateEnter(this);
    }
}
