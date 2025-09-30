using UnityEngine;

public class BasicSwitch : ISwitch
{
    [SerializeField] private bool state = false;
    public override bool IsTurnedOn() 
    {
        return state;
    }
    public override void ChangeState(bool newState) 
    {
        state = newState;

        if (newState == true) TurnedOn.Invoke();
        else TurnedOff.Invoke();

        if(state == newState) {
            StateChange.Invoke(newState);
        }
    }
    #region Helper functions
    public override void ChangeState() 
    {
        ChangeState(!state);
    }
    public override void TurnOff() 
    {
        ChangeState(false);
    }
    public override void TurnOn() 
    {
        ChangeState(true);
    }
    #endregion
}