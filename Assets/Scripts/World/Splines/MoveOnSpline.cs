using UnityEngine;
using UnityEngine.Splines;

public class MoveOnSpline {
    [SerializeField] private SplineManager splineManager;
    [SerializeField] private SplineContainer currentSplineContainer;
    [SerializeField] private int currentSplineIndex = 0;
    public float speed = 2f;
    public float t = 0f; // Position on current slide

    void Start() {
        checkForNextSplines();
    }

    void Update() {
        Spline currentSpline = currentSplineContainer[currentSplineIndex];

        t += speed / currentSpline.GetLength() * Time.deltaTime;

        if (t > 1) {
            float deltaT = t - 1;

            SplineSegment s;
            s.index = currentSplineIndex;
            s.container = currentSplineContainer;

            if (splineManager.GetNextSegment(s, SplineSide.End, out SplineSegment connectedSegment, out SplineSide connectedSide)) {
                currentSplineContainer = connectedSegment.container;
                currentSplineIndex = connectedSegment.index;

                t = connectedSide == SplineSide.Beginning ? 0 : 1;
            } else {
                Debug.LogWarning("End of the spline. No more connections found.");
            }
        }
    }

    private void checkForNextSplines() {
        //nextSplines = currentSplineContainer.gameObject.GetComponent<NextSpline>();
    }
}