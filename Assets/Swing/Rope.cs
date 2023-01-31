using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField, Range( 2, 100 )] private int _numJoints;
    [SerializeField] private Rigidbody _boundRb;
    [SerializeField] private GameObject _jointPrefab;
    [SerializeField, Min( 0f )] private float _length;
    [SerializeField] private List<GameObject> _joints;

    [ContextMenu( "Generate Rope" )]
    private void GenerateRope( )
    {
        _joints = new List<GameObject>();

        var segLength = _length / _numJoints;
        var nextPos = segLength * ( _numJoints - 1 ) * Vector3.down;
        var first = Instantiate( _jointPrefab, nextPos, Quaternion.identity, transform );
        nextPos.y += segLength;

        first.GetComponent<Joint>().connectedBody = _boundRb;
        _joints.Add( first );

        first.name = "first";

        for ( int i = 1; i < _numJoints; i++ )
        {
            var jointObject = Instantiate( _jointPrefab, nextPos, Quaternion.identity, transform );
            nextPos.y += segLength;
            var joint = jointObject.GetComponent<Joint>();
            var rb = _joints[i - 1].GetComponent<Rigidbody>();
            joint.connectedBody = rb;
            _joints.Add( jointObject );
        }

        // The last joint needs to be an anchor, aka kinematic
        var last = _joints[^1];
        last.transform.position = Vector3.zero;
        last.GetComponent<Rigidbody>().isKinematic = true;
        last.name = "last";
    }

    public void MoveRope( Vector3 start, Vector3 end )
    {
        for ( int i = 0; i < _numJoints; i++ )
        {
            _joints[i].transform.position = Vector3.Lerp( start, end, i / ( _numJoints - 1f ) );
        }
    }
}