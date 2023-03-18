using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remomentizer : MonoBehaviour
{

    [SerializeField] private GameObject _target;
    private bool _active;

    private void Update( )
    {
        if (Input.GetKey(KeyCode.R) && Input.GetKey( KeyCode.U ) && Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.E)  )
        // https://www.youtube.com/watch?v=e3o6L10rEp8&ab_channel=Man don't be afraid do it
        // I hate you
        {
            _active = !_active;
            _target.SetActive(_active);
        }
    }
}
