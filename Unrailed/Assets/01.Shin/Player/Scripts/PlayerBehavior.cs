using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Idle,
    Interact,
    Move
};

public class PlayerBehavior : MonoBehaviour
{

    public PlayerState myState {  get; private set; }

    CharacterController myController = null;
    PlayerInput input = null;

    private Vector3 moveValue = Vector3.zero;

    [Space]

    [Header("�÷��̾� �̵� ����")]
    [Range(1f, 20f)]
    public float speed = default;
    [Range(360, 720)]
    public int rotationSpeed = default;

    private void Awake()
    {
        myController = GetComponent<CharacterController>();
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        // InputSystem Ȱ��ȭ
        input.Enable();
        input.Player.Move3D.performed += OnMove;
        input.Player.Move3D.canceled += CancelMove;
    }

    private void FixedUpdate()
    {
        Move();
    }
        
    private void OnDisable()
    {
        // InputSystem ��Ȱ��ȭ
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

    private void Move()
    {
        // �÷��̾� Ű�Է¿� ���� �̵����� ����
        myController.Move(moveValue * speed * Time.deltaTime);

        // �÷��̾� �̵��� ���� ȸ������
        if (moveValue != Vector3.zero)
        {
            // TODO : �� ���ʹϾ� �Լ� ��� �ľ��ϱ�
            Quaternion rotate = Quaternion.LookRotation(moveValue.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, Time.deltaTime * rotationSpeed);
        }
    }

    #region PublicResponsibility


    #endregion
}
