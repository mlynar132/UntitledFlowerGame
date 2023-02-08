using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class Levels : ScriptableObject
{
    [SerializeField] private SceneAsset[] _levelAssets;
    public SceneAsset[] LevelAssets => _levelAssets;
}
