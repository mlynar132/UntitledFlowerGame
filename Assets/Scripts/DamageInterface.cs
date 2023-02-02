using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DamageTarget
{
    public void DecreaseHealth(int amount);

    public void KillTarget();
}
