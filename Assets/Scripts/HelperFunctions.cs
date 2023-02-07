using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    public static bool PartOfLayerMask(GameObject gameObject, LayerMask layerMask) => layerMask == (layerMask | (1 << gameObject.layer));
}
