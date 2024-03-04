using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum playerState
{
    Idle,
    Interact,
    Stop
}

public class PlayerBehavior : FSM<playerState>
{
    public ActionSelelector<PlayerBehavior> playerAction;
    Dictionary<playerState, ActionSelelector<PlayerBehavior>> actionNode = 
        new Dictionary<playerState, ActionSelelector<PlayerBehavior>>();
    CharacterController myController = null;
    PlayerInput input = null;

    #region FSMState

    Idle act1 = null;
    Interact act2 = null;

    #endregion

    private Vector3 moveValue = Vector3.zero;


    [Space]
    [Header("플레이어 이동 관련")]
    [Range(1f, 20f)]
    public float speed = default;
    [Range(360, 720)]
    public int rotationSpeed = default;

    private void Awake()
    {
        myController = GetComponent<CharacterController>();
        input = new PlayerInput();
        CreateNode();
        playerAction.OnStateEnter();
    }

    private void OnEnable()
    {
        // InputSystem 활성화
        input.Enable();
        input.Player.Move3D.performed += OnMove;
        input.Player.Move3D.canceled += CancelMove;
    }

    private void Update()
    {
        playerAction.OnStateUpdate();
    }
        
    private void OnDisable()
    {
        // InputSystem 비활성화
        input.Disable();
        input.Player.Move3D.performed -= OnMove;
        input.Player.Move3D.canceled -= CancelMove;
    }

    private void OnMove(InputAction.CallbackContext value)
    {
        moveValue = value.ReadValue<Vector3>();
    }

    private void CancelMove(InputAction.CallbackContext value)
    {
        moveValue = Vector3.zero;
    }

    public void Move()
    {
        // 플레이어 키입력에 따른 이동방향 결정
        myController.Move(moveValue * speed * Time.deltaTime);

        // 플레이어 이동에 따른 회전방향
        if (moveValue != Vector3.zero)
        {
            // TODO : 각 쿼터니언 함수 기능 파악하기
            Quaternion rotate = Quaternion.LookRotation(moveValue.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, Time.deltaTime * rotationSpeed);
        }
    }

    protected override void CreateNode()
    {
        act1 = new Idle(this);
        act2 = new Interact(this);

        actionNode.Add(playerState.Idle, act1);
        actionNode.Add(playerState.Interact, act2);

        playerAction = actionNode[playerState.Idle];
    }

    #region PublicResponsibility

    public override void ChangeNode(playerState targetNode)
    {
        playerAction = actionNode[targetNode];
        playerAction.OnStateEnter();
    }

    #endregion
}
