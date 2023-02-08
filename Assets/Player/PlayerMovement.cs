using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _moveDir;
    private Vector2 _aimDir;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce;
    private Rigidbody2D _rb;
    private bool _jumped = false;
    [SerializeField] Collider2D _jumpTriger;
    private bool _grounded;
    private bool _moveable;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _moveable = true;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _jumped = true;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        _moveDir = context.ReadValue<Vector2>();
        _aimDir = _moveDir.normalized;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _grounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _grounded = false;
    }
    private void Update()
    {
        if (_moveDir.x > 0)
        {
            if (transform.eulerAngles.y < 0)
            {
                Debug.Log("R");
                transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            }
        }
        else if (_moveDir.x < 0)
        {
           
            if (transform.eulerAngles.y > -180)
            {
                Debug.Log("L");
                transform.Rotate(Vector3.up, -_rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
        }
    }
    private void FixedUpdate()
    {
        if (!_moveable)
        {
            return;
        }
        //_rb.AddForce(new Vector2(_moveDir * _speed,0));
        _rb.velocity = new Vector2(_moveDir.x * _speed, _rb.velocity.y);
        RaycastHit hit;
        Physics.Raycast(transform.position - new Vector3(0, 0.51f, 0), Vector3.down, out hit, 0.5f, 0);
        Debug.DrawRay(transform.position - new Vector3(0, 0.51f, 0), Vector3.down * 0.5f, Color.red);
        if (!_grounded)
        {

            return;
        }

        if (_jumped)
        {
            _rb.AddForce(new Vector2(0, 1) * _jumpForce, ForceMode2D.Impulse);
            _jumped = false;
        }
    }
    public void StopMovement()
    {
        //stop the up movement
        if (_rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }
        _moveable = false;
        _rb.simulated = false;
    }
    public void StartMovement()
    {
        _moveable = true;
        _rb.simulated = true;
    }
    public void HidePlayer()
    {
        _meshRenderer.enabled = false;
    }
    public void ShowPlayer()
    {
        _meshRenderer.enabled = true;
    }
}