using System;
using UnityEngine;

namespace Devdayo.Platformer2D.Bot
{
    public class OnFlop : FsmStateBehaviour
    {
        BotFSM bot;
        
        public override void OnEnter()
        {
            bot = GetOwner<BotFSM>();
            bot.animator.SetTrigger("Flopping");

            bot.boxCollider.enabled = false;
            bot.polyCollider.enabled = true;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            Rigidbody2D rb = bot.rigidbody;

            if (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon)
                return;

            rb.gravityScale = 0;
            rb.Sleep();

            bot.boxCollider.enabled = false;
            bot.polyCollider.enabled = false;

            fsm.Go<OnDied>();
        }

        public override void OnExit()
        {
            
        }
    }
}
