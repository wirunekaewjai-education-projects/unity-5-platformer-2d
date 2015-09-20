using UnityEngine;

namespace devdayo.Fsm.Bot.State
{
    public class OnDied : StateBehaviour
    {
        void OnEnable()
        {
            Destroy(fsm.gameObject, 3);
        }
    }
}
