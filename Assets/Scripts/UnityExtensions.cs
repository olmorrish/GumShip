using UnityEngine;
using UnityEngine.Events;

public static class UnityExtensions {

    //Extension method to check if a layer is in a layermask
    public static bool Contains(this LayerMask layerMask, int layer) {
        return layerMask == (layerMask | (1 << layer));
    }
}