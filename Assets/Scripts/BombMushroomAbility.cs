using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombMushroomAbility : MonoBehaviour {
    private IPlayer1Controller _playerInterface;
    [SerializeField] private FloatEvent _cooldownEvent;

    [SerializeField] private Transform _throwLocation;
    [SerializeField] private ExplodingObject _explodingObject;
    [SerializeField] private Transform _aim;

    [SerializeField][Range(0, 15)] private float _xForce = 6;
    [SerializeField][Range(0, 15)] private float _yForce = 4;

    [SerializeField][Range(0, 180)] private float _maxUpAngle = 80;
    [SerializeField][Range(10, 180)] private float _maxDownAngle = 20;

    [SerializeField] private float _rotationSpeed = 500;

    private float _rotateDir;

    private bool _cooldown;
    private void Awake() {
        _playerInterface = GetComponent<IPlayer1Controller>();
    }
    private void Start() {
        _playerInterface.BombUsed += ThrowMushroom;

        _cooldownEvent.ResetValue();
    }

    private void Update() {
        /*if(Input.GetKeyDown(KeyCode.Y) && !_cooldown)
        {
            ThrowMushroom(_aim.position - transform.position);
        }*/

        //        _aim.Rotate(0, 0, _rotateDir * _rotationSpeed * Time.deltaTime);

        // float zRotation = Mathf.Clamp(_aim.eulerAngles.z, _maxDownAngle, _maxUpAngle);

        //        _aim.eulerAngles = new Vector3(0, 0, zRotation);

        /*        if(_cooldown)
                {
                    _cooldownEvent.ChangeValue(_cooldownEvent.currentValue - Time.deltaTime);

                    if(_cooldownEvent.currentValue <= 0)
                    {
                        _cooldownEvent.ChangeValue(0);
                        _cooldown = false;
                    }
                }*/
    }

    //  public void OnAim(InputValue value) => _rotateDir = -value.Get<Vector2>().x;

    private void ThrowMushroom(Vector2 dir) {
       // Debug.Log("DAS");
        //  _cooldown = true;
        // _cooldownEvent.ResetValue();

        ExplodingObject explodingObject = Instantiate(_explodingObject, transform.position + new Vector3(0.75f,0.75f,0) , Quaternion.identity);
       // Debug.Log("DAS2");
        //Vector3 force = new Vector3(dir.x * _xForce, dir.y * _yForce, 0); Debug.Log("DAS3");
        explodingObject.AddForce(new Vector2(dir.x * _xForce, dir.y * _yForce)); //Debug.Log("DAS4");
    }
}
