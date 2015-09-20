using UnityEngine;

namespace devdayo.Fsm.Bot.State
{
    public class OnFlop : StateBehaviour
    {
        BotFSM bot;

        void OnEnable()
        {
            bot = fsm as BotFSM;
            bot.animator.SetTrigger("Flopping");
            
            bot.boxCollider.enabled = false;
            bot.polyCollider.enabled = true;
        }

        void Update()
        {
            if (!enabled)
                return;

            Rigidbody2D rb = bot.rigidbody;

            if (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon)
                return;

            rb.gravityScale = 0;
            rb.Sleep();
            
            bot.boxCollider.enabled = false;
            bot.polyCollider.enabled = false;

            bot.DoTransition(Transition.OnDied);
        }
    }
}
