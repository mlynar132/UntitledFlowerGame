using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TutorialAdapter : MonoBehaviour
{
    [SerializeField] private GameObject _p1Icon;
    [SerializeField] private GameObject _p2Icon;
    [SerializeField, Range( 1, 2 )] private int _character;

    private void Start( )
    {
        if ( PlayerDevices.char1Player == 0 )
        {
            _p1Icon.SetActive( _character == 1 );
            _p2Icon.SetActive( _character == 2 );
            return;
        }


        switch ( _character )
        {
            case 1:
                _p1Icon.SetActive( PlayerDevices.char1Player == 1 );
                _p2Icon.SetActive( PlayerDevices.char1Player == 2 );
                break;
            case 2:
                _p1Icon.SetActive( PlayerDevices.char2Player == 1 );
                _p2Icon.SetActive( PlayerDevices.char2Player == 2 );
                break;
        }
    }
}