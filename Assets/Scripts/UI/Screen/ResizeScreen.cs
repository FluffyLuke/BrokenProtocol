using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

public class ResizeScreen : MonoBehaviour
{
    public RectTransform Screen;
    public CanvasManager Manager;
    public void Open(float speed = 0.2f, float delay = 0.5f) {
        Vector2 previousSize = Screen.sizeDelta;
        Screen.sizeDelta = new(previousSize.x, 0);
        float fromY = 0;
        DOTween.To(() => fromY, y => fromY = y, previousSize.y, speed).SetDelay(delay)
            .OnUpdate(() => {
                Screen.sizeDelta = new(previousSize.x, fromY);
            });
    }

    public void Close(Action onComplete = null, float speed = 0.2f, float delay = 0.5f) {
        Vector2 previousSize = Screen.sizeDelta;
        float fromY = previousSize.y;
        DOTween.To(() => fromY, y => fromY = y, 0, speed).SetDelay(delay)
            .OnUpdate(() => {
                Screen.sizeDelta = new(previousSize.x, fromY);
            })
            .OnComplete(() => {onComplete();});
    }
}
