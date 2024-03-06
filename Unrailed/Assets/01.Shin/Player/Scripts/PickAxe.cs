using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : PickObject
{
    public override PickObject Interact()
    {
        Debug.Log("곡괭이");
        return base.Interact();
    }
}
