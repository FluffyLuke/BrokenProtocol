using System.Data.SqlTypes;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(MinecartSpline))]
public class Minecart : MonoBehaviour {
    public float speed = 3f;
    public float reverseSpeed = 2f;
    private MinecartSpline track;
    //[SerializeField] private Transform currentTransform;
    void Start() {
        track = GetComponent<MinecartSpline>();
    }
    public void PushForward() {
        track.PushCart(speed, true);
        
    }

    public void PushBackward() {
        track.PushCart(reverseSpeed, false);
    }
}