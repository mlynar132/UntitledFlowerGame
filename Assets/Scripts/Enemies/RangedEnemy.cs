using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack")]
    [SerializeField] private Transform _projectileSpawnLocation;
    [SerializeField] private EnemyProjectile _projectile;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _projectileSpeed = 5;
    [SerializeField] private int _projectileDamage = 1;
    private Vector3 _targetLocation;
    private Coroutine _attackCoroutine;
    private bool _performingAttack;

    protected override void PlayerDetected(OnTrigger2dEvent.OnTriggerDelegation delegation)
    {
        base.PlayerDetected(delegation);

        StopAttackCoroutine();

        _attackCoroutine = StartCoroutine(AttackTimer());
    }

    private void StopAttackCoroutine()
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
    }

    public override void ObjectLost(OnTrigger2dEvent.OnTriggerDelegation delegation)
    {
        if (_currentAttackTarget != null)
            _targetLocation = _currentAttackTarget.transform.position;

        base.ObjectLost(delegation);
    }

    private IEnumerator AttackTimer()
    {
        while(_currentAttackTarget != null || _performingAttack)
        {
            _performingAttack = true;
            _state = State.Attacking;

            yield return new WaitForSeconds(_attackDelay);

            EnemyProjectile curretProjectile = Instantiate(_projectile, _projectileSpawnLocation.position, Quaternion.identity);
            curretProjectile.SetStats(_projectileSpeed, _projectileDamage);

            if(_currentAttackTarget != null)
                _targetLocation = _currentAttackTarget.transform.position;

            Quaternion newRotation = Quaternion.LookRotation(_targetLocation - curretProjectile.transform.position);
            newRotation.x = 0;
            curretProjectile.transform.rotation = newRotation;

            _performingAttack = false;

            if (_currentAttackTarget == null)
                _state = State.Moving;
        }
    }
}