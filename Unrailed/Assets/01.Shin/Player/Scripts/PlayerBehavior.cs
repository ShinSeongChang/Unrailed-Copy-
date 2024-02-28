using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    CharacterController myController;

    [Space]

    [Header("플레이어 이동 관련")]
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
        // TODO : InputSystem의 키 연결
        // 플레이어 키입력에 따른 이동방향 결정
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        myController.Move(move * speed * Time.deltaTime);

        // 플레이어 이동에 따른 회전방향
        if (move != Vector3.zero)
        {
            // TODO : 각 쿼터니언 함수 기능 파악하기
            Quaternion rotate = Quaternion.LookRotation(move.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, Time.deltaTime * rotationSpeed);
        }
    }

    #region PublicResponsibility


    #endregion
}
