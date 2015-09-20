using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnDied : StateBehaviour
    {
        void OnEnable()
        {
            Invoke("OnRestartLevel", 3);
        }

        void OnRestartLevel()
        {
            int level = Application.loadedLevel;
            Application.LoadLevel(level);
        }
    }
}
