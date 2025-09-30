using UnityEngine;
using UnityEngine.Events;

public abstract class ISwitch : MonoBehaviour
{
    public UnityEvent<bool> StateChange = new();
    public UnityEvent TurnedOn = new();
    public UnityEvent TurnedOff = new();
    public abstract bool IsTurnedOn();
    public abstract void TurnOn();
    public abstract void TurnOff();
    public abstract void ChangeState();
    public abstract void ChangeState(bool newState);
}
