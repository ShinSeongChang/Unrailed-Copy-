using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    CharacterController myController;

    [Space]

    [Header("�÷��̾� �̵� ����")]
    [Range(1f, 20f)]
    public float speed;
    [Range(360, 720)]
    public int rotationSpeed;

    private void Awake()
    {
        myController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // TODO : InputSystem�� Ű ����
        // �÷��̾� Ű�Է¿� ���� �̵����� ����
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        myController.Move(move * speed * Time.deltaTime);

        // �÷��̾� �̵��� ���� ȸ������
        if (move != Vector3.zero)
        {
            // TODO : �� ���ʹϾ� �Լ� ��� �ľ��ϱ�
            Quaternion rotate = Quaternion.LookRotation(move.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, Time.deltaTime * rotationSpeed);
        }
    }

    #region PublicResponsibility


    #endregion
}
