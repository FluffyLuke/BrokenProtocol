using System;
using UnityEngine;

[Serializable]
public struct RailConnectionField {
    public bool isSet;
    public SplineAnchor anchor;
    public RailConnectionField(bool isSet, SplineAnchor anchor) {
        this.isSet = isSet;
        this.anchor = anchor;
    }
    public static RailConnectionField Empty() {
        return new RailConnectionField(false, default);
    }

    public void Set(SplineAnchor anchor) {
        this.anchor = anchor;
        isSet = true;
    }
}
public class RailConnection : MonoBehaviour {
    public RailConnectionField[] connectionAnchors = new RailConnectionField[0];
}