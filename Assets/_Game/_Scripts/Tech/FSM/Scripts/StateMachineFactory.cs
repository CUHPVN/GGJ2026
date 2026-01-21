using UnityEngine;

namespace Core.FSM
{
    public static class StateMachineFactory
    {
        public static StateMachine<TState, TEvent> Create<TState, TEvent>(
            GameObject gameObject, 
            TState initialState)
            where TState : IState<TState, TEvent>
            where TEvent : ITrigger
        {
            var stateMachine = new StateMachine<TState, TEvent>(gameObject);
            stateMachine.Initialize(initialState);
            return stateMachine;
        }
    }
}