using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    public virtual PickObject Interact()
    {
        return this;
    }

}
