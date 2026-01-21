using System.Collections.Generic;
using UnityEngine;

namespace Core.FSM
{
    public class StateMachine<TState, TTrigger> : IStateMachineCore
        where TState : IState<TState, TTrigger>
        where TTrigger : ITrigger
    {
        private TState _currentState;
        private bool _isTransitioning;
        private readonly Queue<TTrigger> _pendingTriggers = new();

        /// <summary>
        /// Current active state
        /// </summary>
        public TState CurrentState => _currentState;

        /// <summary>
        /// Creates a new state machine and attaches it to a GameObject
        /// </summary>
        /// <param name="gameObject">GameObject to attach the state machine runner to</param>
        public StateMachine(GameObject gameObject)
        {
            var runner = gameObject.AddComponent<StateMachineRunner>();
            runner.Initialize(this);
        }

        /// <summary>
        /// Triggers an event for the current state to handle
        /// </summary>
        /// <param name="data">The data to trigger</param>
        /// <returns>True if trigger was handled and caused a state transition</returns>
        public bool Trigger(TTrigger data)
        {
            if (_currentState == null)
                return false;

            // If transitioning, queue the trigger to process after OnEnter completes
            if (_isTransitioning)
            {
                _pendingTriggers.Enqueue(data);
                return false;
            }

            var nextState = _currentState.HandleTrigger(data);

            if (nextState == null || nextState.Equals(_currentState)) return false;

            TransitionToState(nextState);
            return true;
        }

        /// <summary>
        /// Initializes the state machine with a starting state
        /// </summary>
        public void Initialize(TState initialState)
        {
            if (_currentState != null)
            {
                _currentState.OnExit();
            }

            _currentState = initialState;
            _isTransitioning = true;
            _currentState.OnEnter();
            _isTransitioning = false;

            ProcessPendingTriggers();
        }

        /// <summary>
        /// Transitions from the current state to a new state
        /// </summary>
        private void TransitionToState(TState newState)
        {
            _isTransitioning = true;

            _currentState.OnExit();
            _currentState = newState;
            _currentState.OnEnter();

            _isTransitioning = false;

            ProcessPendingTriggers();
        }

        /// <summary>
        /// Processes any triggers that were queued during state transitions
        /// </summary>
        private void ProcessPendingTriggers()
        {
            while (_pendingTriggers.Count > 0)
            {
                var trigger = _pendingTriggers.Dequeue();
                Trigger(trigger);
            }
        }

        public void OnUpdate()
        {
            if (_currentState != null && !_isTransitioning)
            {
                _currentState.OnUpdate();
            }
        }

        public void OnFixedUpdate()
        {
            if (_currentState != null && !_isTransitioning)
            {
                _currentState.OnFixedUpdate();
            }
        }
    }
}