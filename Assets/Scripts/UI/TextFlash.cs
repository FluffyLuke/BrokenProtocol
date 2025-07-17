using System;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFlash : MonoBehaviour
{
    public string[] Frames;
    public float TimeBetweenFrames = 1;
    private TextMeshProUGUI _text;
    private float _lastTime;
    private int _currentFrame = 0;
    void Start() {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = Frames[0];

        _lastTime = Time.time;
    }
    void Update() {
        if(_lastTime + TimeBetweenFrames <= Time.time) {
            _lastTime = Time.time;

            _currentFrame++;
            if(_currentFrame >= Frames.Length) {
                _currentFrame = 0;
            }

            _text.text = Frames[_currentFrame];
        }
    }
}
