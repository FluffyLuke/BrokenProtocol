using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[Serializable]
public enum SplineSide {
    Beginning,
    End,
}
[Serializable]
public struct SplineConnection {
    public SplineSegment spline1;
    public SplineSide spline1side;
    public SplineSegment spline2;
    public SplineSide spline2side;
}
[Serializable]
public struct SplineSegment {
    public SplineContainer container;
    public int index;
}
public class SplineManager {
    public SplineConnection[] connections = new SplineConnection[0];
    public bool GetNextSegment(SplineSegment currentSegment, SplineSide currentSegmentSide, out SplineSegment connectedSegment, out SplineSide connectedSegmentSide) {
        foreach(SplineConnection s in connections) {
            if (
                s.spline1.container != currentSegment.container // if splines are not in the same container
                || currentSegment.index != s.spline1.index // if splines don't have the same index
                || s.spline1side != currentSegmentSide // if spline sides does not match
            ) continue;

            connectedSegment = s.spline2;
            connectedSegmentSide = s.spline2side;
            return true;
        }

        connectedSegment = default;
        connectedSegmentSide = default;
        return false;
    }
}