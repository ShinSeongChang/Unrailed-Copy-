using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum playerState
//{
//    Idle,
//    Interact,
//    Stop
//}

//public class TestPlayer : FSM
//{
//    protected PlayerSelector playerAction;
//    private Dictionary<playerState, PlayerSelector> test = new Dictionary<playerState, PlayerSelector>();

//    private void Awake()
//    {
//        CreateNode();
//        playerAction.OnStateEnter();
//    }

//    protected override void CreateNode()
//    {
//        Idle act1 = new Idle(this);
//        Interact act2 = new Interact(this);

//        test.Add(playerState.Idle, act1);
//        test.Add(playerState.Interact, act2);

//        playerAction = test[playerState.Idle];
//    }

//    public void ChangeNode(playerState targetNode)
//    {
//        playerAction = test[targetNode];
//        test[targetNode].OnStateEnter();
//    }

//    private void Update()
//    {
//        playerAction.OnStateUpdate();
//    }
//}
