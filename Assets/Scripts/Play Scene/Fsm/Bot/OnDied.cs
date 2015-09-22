using System;
using UnityEngine;

namespace Devdayo.Platformer2D.Bot
{
    public class OnDied : FsmState
    {
        public override void OnEnter()
        {
            GameObject.Destroy(fsm.gameObject, 3);
        }

        public override void OnExit()
        {
            
        }
    }
}
