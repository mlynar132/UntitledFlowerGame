using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    bool isOpen = false;

    [SerializeField]
    public GameObject door;


    private void OnTriggerEnter(Collider other)
    {

        if (!isOpen)
        {
            door.transform.position = new Vector3(-3.7974f, 8.2626f, -1f);
            isOpen = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpen)
        {
            door.transform.position = new Vector3(-3.7974f, 3.2626f, -1f);
            isOpen = false;
        }
    }
}
