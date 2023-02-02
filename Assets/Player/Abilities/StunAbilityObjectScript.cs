using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAbilityObjectScript : MonoBehaviour
{
    [SerializeField] private float _attackDuration;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        _attackDuration -= Time.deltaTime;
        if (_attackDuration<=0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IStunable targetInterface = other.transform.GetComponent<IStunable>();
        if (targetInterface != null)
        {
            targetInterface.Stun();
        }
    }
}
