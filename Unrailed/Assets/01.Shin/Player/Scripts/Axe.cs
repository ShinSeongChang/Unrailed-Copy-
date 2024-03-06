using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : PickObject
{
    public override PickObject Interact()
    {
        Debug.Log("도끼");
        return base.Interact();
    }
}
