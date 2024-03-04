using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 FSM을 상속받는 객체들은 자신의 Enum을 정의하고 스테이트 머신이 매개인자로 사용할 것
public abstract class FSM<T> : MonoBehaviour where T : System.Enum
{
    /// <summary>
    /// FSM을 구현하는 객체들은 진입시점에 자신의 노드를 생성을 강제
    /// </summary>
    protected abstract void CreateNode();

    /// <summary>
    /// FSM을 구현하는 객체들은 노드 변경시 자신만의 enum값을 전달하여 행동노드 변경
    /// </summary>
    /// <param name="_enum">객체만의 고유 상태</param>
    public abstract void ChangeNode(T _enum);
}
