using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _layerMask;

    public void Break( InputAction.CallbackContext context )
    {
        if ( context.ReadValueAsButton() )
        {
            var hit = Physics2D.Raycast( transform.position, Vector2.right, _range, _layerMask );

            if ( hit )
            {
                var target = hit.collider.gameObject.GetComponent<IDestructable>();

                target?.OnHit();
            }
        }
    }
}