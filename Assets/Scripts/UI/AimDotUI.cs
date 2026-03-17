using UnityEngine;
using UnityEngine.Events;

public class AimDotUI : MonoBehaviour {
    [SerializeField] private GameObject dot;
    private void Start() {
        PlayerEventBus.ShowAim.AddListener(showDot);
    }

    private void showDot(bool show) {
        dot.SetActive(show);
    }
}
