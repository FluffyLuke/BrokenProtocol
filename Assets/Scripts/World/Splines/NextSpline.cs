using System;
using UnityEngine;
public class NextSpline {

    [Serializable]
    public enum SplinePart {
        Beginning,
        End,
    }

    [Serializable]
    public struct SplineSwitchData {
        public SplinePart splinePart;
        public int splineIndex;
        public SplinePart newSplinePart;
        public int newSplineIndex;
    }

    public SplineSwitchData[] switchData = new SplineSwitchData[0];
}