using DG.Tweening;
using UnityEngine;
public class ChangeRotation : MonoBehaviour
{
    [SerializeField] bool startState = false;
    public Vector3 rotationOff = new();
    public Vector3 rotationOn = new();
    public float changeStateSpeed = 1.5f;
    void Start() {
        Rotate(startState);
    } 
    public void Rotate(bool newState) {
        setRotation(newState, changeStateSpeed);
    }
    private void setRotation(bool isTurnedOn, float speed) {
        Vector3 moveBy = isTurnedOn ? rotationOn : rotationOff;
        transform.DOLocalRotate(moveBy, speed, RotateMode.Fast);
    }
}