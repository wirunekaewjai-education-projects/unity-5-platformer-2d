using UnityEngine;
using System.Collections;

namespace devdayo.Fsm
{
    public class StateBehaviour : MonoBehaviour
    {
        private StateMachineBehaviour _fsm;
        public StateMachineBehaviour fsm
        {
            get
            {
                if (null == _fsm)
                    _fsm = GetComponent<StateMachineBehaviour>();

                return _fsm;
            }
        }
    }
}
