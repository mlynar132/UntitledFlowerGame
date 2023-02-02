using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformAbilitie: MonoBehaviour
{
    private PlayerInputAction _playerInputAction;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private GameObject _player;
    private GameObject lol;
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
        if (context.started)
        {
            //get position
            Vector3 worldPos = _player.transform.position;
            //make restriction for spawning 
            //if (Physics.OverlapBox(worldPos, Vector3.one * 0.45f, Quaternion.identity/*layer masks for condition*/).Length==0) //can't be 0.5 cuz edges will colide {   
          
            //}
            //spawn
            lol = Instantiate(_gameObject, worldPos, Quaternion.identity);
            lol.transform.GetComponent<MeshRenderer>().material.color = Color.green;

        }
        if (context.canceled && lol != null)
        {
            Destroy(lol);
            _player.SetActive(true);
        }
        /*Debug.Log(context);
        if (context.started)
        {
            Debug.Log("DAS");
            //get world pos from mouse input
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = Camera.main.nearClipPlane + _camera.transform.position.z*-1; //z won't be 0 but it will be closer to 0 than 1 or -1
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            //snap to grid
            worldPos.x = Mathf.RoundToInt(worldPos.x);
            worldPos.y = Mathf.RoundToInt(worldPos.y);
            worldPos.z = Mathf.RoundToInt(worldPos.z);
            //make restriction for spawning 
            if (Physics.OverlapBox(worldPos, Vector3.one * 0.45f,Quaternion.identity/*layer masks for condition).Length==0) //can't be 0.5 cuz edges will colide
            {
                //spawn
                lol = Instantiate(_gameObject, worldPos, Quaternion.identity);
                lol.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        if (context.canceled && lol != null)
        {
            Destroy(lol);
        }*/
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
        _playerInputAction.Player2.PlaceBlock.started += PlaceBlock;
       // _playerInputAction.Player2.PlaceBlock.performed += PlaceBlock;
        _playerInputAction.Player2.PlaceBlock.canceled += PlaceBlock;

    }
    public void DiableLeft()
    {
        _playerInputAction.Player2.PlaceBlock.Disable();
        _playerInputAction.Player2.PlaceBlock.started -= PlaceBlock;
        // _playerInputAction.Player2.PlaceBlock.performed -= PlaceBlock;
        _playerInputAction.Player2.PlaceBlock.canceled -= PlaceBlock;
    }
}