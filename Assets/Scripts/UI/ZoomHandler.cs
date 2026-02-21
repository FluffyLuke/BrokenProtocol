using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using DG.Tweening;
using System;

public class ZoomHandler : MonoBehaviour {
    [SerializeField] private Image[] images;
    [SerializeField] private CinemachineCamera cam;
    [Range(0f, 1f)]
    public float strengthOfBlur = 0.7f;
    public float speed = 0.5f;
    public float zoomFOV;
    private float defaultFOV;
    private Tween tween;
    void Awake() {
        PlayerEventBus.Zoom.AddListener(zoom);
        defaultFOV = cam.Lens.FieldOfView;
    }

    private void zoom(bool shouldZoom) {
        tween?.Kill();

        float targetFOV = shouldZoom ? zoomFOV : defaultFOV;
        
        tween = DOTween.To(() => cam.Lens.FieldOfView, x => {
            LensSettings lens = cam.Lens;
            lens.FieldOfView = x;
            cam.Lens = lens;

            float progress = Mathf.InverseLerp(defaultFOV, zoomFOV, x);
            foreach(Image i in images) {
                Color c = i.color;
                c.a = strengthOfBlur * progress;
                i.color = c;
            }
        }, targetFOV, speed);
    }
}