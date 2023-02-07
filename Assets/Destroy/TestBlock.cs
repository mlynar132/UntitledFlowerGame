using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlock : MonoBehaviour, IDestructable
{
    public void OnHit( )
    {
        Destroy( gameObject );
    }
}