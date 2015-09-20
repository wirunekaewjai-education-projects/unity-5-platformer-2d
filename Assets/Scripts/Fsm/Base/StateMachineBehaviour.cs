using UnityEngine;
using System.Collections.Generic;
using System;

namespace devdayo.Fsm
{
    public class StateMachineBehaviour : MonoBehaviour
    {
        private Dictionary<int, Type> transitions = new Dictionary<int, Type>();
        private Dictionary<Type, StateBehaviour> states = new Dictionary<Type, StateBehaviour>();

        private StateBehaviour currentState;
        private int currentId = int.MinValue;

        public void AddTransition(int transitionId, Type nextState)
        {
            transitions[transitionId] = nextState;
        }

        public void DoTransition(int transitionId)
        {
            if (transitionId < 0)
                return;

            if (currentId == transitionId)
                return;

            if (!transitions.ContainsKey(transitionId))
                return;

            if (null != currentState)
                currentState.enabled = false;

            Type type = transitions[transitionId];

            if (!states.ContainsKey(type))
                states[type] = gameObject.AddComponent(type) as StateBehaviour;

            currentId = transitionId;
            currentState = states[type];
            currentState.enabled = true;
        }

        public virtual void OnDestroy()
        {
            foreach (var state in states.Values)
            {
                Destroy(state);
            }

            states.Clear();
            transitions.Clear();
        }
    }

}