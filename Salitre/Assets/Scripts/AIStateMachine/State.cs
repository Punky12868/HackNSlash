using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//You can override one of the functions,
//all of them, or none of them,
//depending on what the individual state needs to do.
public abstract class State
{
    protected StateController sc;
    public void OnStateEnter(StateController stateController)
    {
        // Code placed here will always run
        sc = stateController;
        OnEnter();
    }
    protected virtual void OnEnter()
    {
        // Code placed here can be overridden
    }
    public void OnStateUpdate()
    {
        // Code placed here will always run
        OnUpdate();
    }
    protected virtual void OnUpdate()
    {
        // Code placed here can be overridden
    }
    public void OnStateHurt()
    {
        // Code placed here will always run
        OnHurt();
    }
    protected virtual void OnHurt()
    {
        // Code placed here can be overridden
    }
    public void OnStateExit()
    {
        // Code placed here will always run
        OnExit();
    }
    protected virtual void OnExit()
    {
        // Code placed here can be overridden
    }
}
