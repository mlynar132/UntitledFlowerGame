using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityToggle : MonoBehaviour
{
    [SerializeField] private Abilities _abilities;
    [SerializeField] private bool _abilityState;
    [SerializeField] private ScriptableStats[] _stats;

    private void OnTriggerEnter2D( Collider2D col )
    {
        ScriptableStats stat;

        if ( col.CompareTag( "Player 1" ) )
        {
            stat = _stats[0];
        }
        else if ( col.CompareTag( "Player 2" ) )
        {
            stat = _stats[1];
        }
        else
        {
            return;
        }

        if ( _abilities.HasFlag( Abilities.Vine ) )
        {
            stat.AllowVineAbility = _abilityState;
        }

        if ( _abilities.HasFlag( Abilities.Anchor ) )
        {
            stat.AllowAnchor = _abilityState;
        }

        if ( _abilities.HasFlag( Abilities.Platform ) )
        {
            stat.AllowBlockAbility = _abilityState;
        }

        if ( _abilities.HasFlag( Abilities.Stun ) )
        {
            stat.AllowStunAbility = _abilityState;
        }

        if ( _abilities.HasFlag( Abilities.Dash ) )
        {
            stat.AllowDash = _abilityState;
        }

        if ( _abilities.HasFlag( Abilities.Bomb ) )
        {
            stat.AllowBomb = _abilityState;
        }
    }

    [Flags]
    private enum Abilities
    {
        Vine = 1,
        Anchor,
        Platform,
        Stun,
        Dash,
        Bomb
    }
}