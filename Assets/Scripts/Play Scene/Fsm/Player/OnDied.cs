using UnityEngine;
using System.Collections;
using System;

namespace Devdayo.Platformer2D.Player
{
    public class OnDied : FsmState
    {
        public override void OnEnter()
        {
            fsm.owner.StartCoroutine(OnRestartLevel());
        }

        public override void OnExit()
        {
            
        }

        IEnumerator OnRestartLevel()
        {
            yield return new WaitForSeconds(3);

            int level = Application.loadedLevel;
            Application.LoadLevel(level);
        }
    }
}
