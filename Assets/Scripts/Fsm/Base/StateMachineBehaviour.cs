using UnityEngine;
using System.Collections.Generic;
using System;

namespace devdayo.Fsm
{
    public class StateMachineBehaviour : MonoBehaviour
    {
        public bool destroyStateOnDisabled = true;

        private Dictionary<int, Type> transitions = new Dictionary<int, Type>();
        private Dictionary<Type, StateBehaviour> states = new Dictionary<Type, StateBehaviour>();
        private Dictionary<Type, GameObject> owners = new Dictionary<Type, GameObject>();

        private StateBehaviour currentState;
        private int currentId = int.MinValue;

        public void SetStateOwner(Type state, GameObject owner)
        {
            owners[state] = owner;
        }

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
            {
                currentState.enabled = false;

                if(destroyStateOnDisabled)
                {
                    DestroyState(currentState);
                }
            }

            Type type = transitions[transitionId];

            if (!states.ContainsKey(type))
            {
                CreateState(type);
            }

            currentId = transitionId;
            StartCoroutine(OnChangeState(states[type]));
        }

        private System.Collections.IEnumerator OnChangeState(StateBehaviour nextState)
        {
            yield return new WaitForEndOfFrame();

            currentState = nextState;
            currentState.enabled = true;
        }

        private void CreateState(Type type)
        {
            GameObject owner = gameObject;

            if(owners.ContainsKey(type))
                owner = owners[type];

            states[type] = owner.AddComponent(type) as StateBehaviour;
            StateBehaviour state = states[type];
            state.fsm = this;
        }

        private void DestroyState(StateBehaviour state)
        {
            Type type = state.GetType();

            owners.Remove(type);
            states.Remove(type);

            Destroy(state);
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