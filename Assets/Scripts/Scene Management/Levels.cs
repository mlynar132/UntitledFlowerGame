using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Levels")]
public class Levels : ScriptableObject
{
    [SerializeField] private int[] _levelIndexes;
    public int[] LevelIndexes => _levelIndexes;
}
