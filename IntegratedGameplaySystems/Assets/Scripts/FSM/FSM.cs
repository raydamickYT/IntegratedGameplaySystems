using System.Collections.Generic;

public class FSM<T>
{
    private State<T> currentState;
    private Dictionary<System.Type, State<T>> allStates = new Dictionary<System.Type, State<T>>();

    public void AddState(State<T> _state)
    {
        if (!allStates.ContainsKey(_state.GetType()))
        {
            allStates.Add(_state.GetType(), _state);
        }
    }

    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }

    public void SwitchState(System.Type _type)
    {
        if (allStates.ContainsKey(_type))
        {
            currentState?.OnExit();
            currentState = allStates[_type];
            currentState?.OnEnter();
        }
    }
}