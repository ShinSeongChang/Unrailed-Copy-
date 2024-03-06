using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� FSM�� ��ӹ޴� ��ü���� �ڽ��� Enum�� �����ϰ� ������Ʈ �ӽ��� �Ű����ڷ� ����� ��
public abstract class FSM<T> : MonoBehaviour where T : System.Enum
{
    /// <summary>
    /// FSM�� �����ϴ� ��ü���� ���Խ����� �ڽ��� ��带 ������ ����
    /// </summary>
    protected abstract void CreateNode();

    /// <summary>
    /// FSM�� �����ϴ� ��ü���� ��� ����� �ڽŸ��� enum���� �����Ͽ� �ൿ��� ����
    /// </summary>
    /// <param name="_enum">��ü���� ���� ����</param>
    public abstract void ChangeNode(T _enum);
}
