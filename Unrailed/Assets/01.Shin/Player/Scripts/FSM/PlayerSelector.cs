using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSelector : ActionSelelector<PlayerBehavior>
{
    protected PlayerSelector(PlayerBehavior behavior) : base(behavior) { }

}
