using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformAbilitie: MonoBehaviour
{
    private PlayerInputAction _playerInputAction;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _sense;
    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
    }
    private void OnEnable()
    {
        EnableLeft();
    }
    private void OnDisable()
    {
        DiableLeft();
    }
    private void PlaceBlock(InputAction.CallbackContext context)
    {
        //get world pos from mouse input
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane + _camera.transform.position.z*-1; //z won't be 0 but it will be closer to 0 than 1 or -1
        //snap to grid

        //spawn
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.x = Mathf.RoundToInt(worldPos.x);
        worldPos.y = Mathf.RoundToInt(worldPos.y);
        worldPos.z = Mathf.RoundToInt(worldPos.z);
        Debug.Log(worldPos);
        GameObject d = Instantiate(_gameObject, worldPos, Quaternion.identity);
        d.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        /*  RaycastHit hit;
          Ray ray2 = _camera.ScreenPointToRay(Input.mousePosition);
          //Ray ray3 = new Ray (_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), Vector3.forward);

          Physics.Raycast(ray, Vector3.forward, Mathf.Infinity);
          /*Debug.DrawRay(ray, Vector3.forward, Color.red, Mathf.Infinity);
          Debug.DrawLine(ray, hit.point);/
          //Debug.DrawRay(_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), Vector3.forward, Color.red, Mathf.Infinity);
          // Debug.Log(hit.transform.name);
          if (Physics.Raycast(ray2, out hit))
          {
              NodeScript nodeSctipt = hit.transform.GetComponent<NodeScript>();
              //  if (_setEnd)
              {
                  if (nodeSctipt.Indeks != _generateGrid.IndeksStart && nodeSctipt.Indeks != _generateGrid.IndeksEnd)
                  {
                      if (nodeSctipt.Walkable)
                      {
                          nodeSctipt.Walkable = false;
                          hit.transform.GetComponent<MeshRenderer>().material = _wallMat;
                      }
                      else
                      {
                          nodeSctipt.Walkable = true;
                          hit.transform.GetComponent<MeshRenderer>().material = _defaultMat;
                      }
                  }
              }
              /* else if (_setStart && nodeSctipt.Indeks != _generateGrid.IndeksStart)
               {
                   _generateGrid.IndeksEnd = nodeSctipt.Indeks;
                   hit.transform.GetComponent<MeshRenderer>().material = _endMat;
                   _setEnd = true;
                   SetDistanceFromEnd(_generateGrid.IndeksEnd);
               }
               else
               {
                   _generateGrid.IndeksStart = nodeSctipt.Indeks;
                   hit.transform.GetComponent<MeshRenderer>().material = _startMat;
                   _setStart = true;
               }*
          }*/
    }
    /*private void SetDistanceFromEnd(int end)
    {
        int size = _generateGrid.SizeX * _generateGrid.SizeY;
        int X = _generateGrid.IndeksEnd % _generateGrid.SizeX;
        int Y = (_generateGrid.IndeksEnd - X) / (_generateGrid.SizeX);
        for (int i = 0; i < size; i++)
        {
            int X2 = _generateGrid.Nodes[i].Indeks % _generateGrid.SizeX;
            int Y2 = (_generateGrid.Nodes[i].Indeks - X2) / (_generateGrid.SizeX);
            _generateGrid.Nodes[i].FromEnd = Mathf.Abs(X - X2) + Mathf.Abs(Y - Y2);
        }
    }*/
    public void EnableLeft()
    {
        _playerInputAction.Player2.PlaceBlock.Enable();
        _playerInputAction.Player2.PlaceBlock.performed += PlaceBlock;
    }
    public void DiableLeft()
    {
        _playerInputAction.Player2.PlaceBlock.Disable();
        _playerInputAction.Player2.PlaceBlock.performed -= PlaceBlock;
    }
}