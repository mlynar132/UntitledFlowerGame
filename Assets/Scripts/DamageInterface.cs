using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageTarget
{
    public void DecreaseHealth(int amount);

    public void KillTarget();
}
