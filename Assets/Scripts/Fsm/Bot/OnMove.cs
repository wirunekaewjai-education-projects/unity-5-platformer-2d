using UnityEngine;

namespace devdayo.Fsm.Bot.State
{
    public class OnMove : StateBehaviour
    {
        BotFSM bot;
        
        void OnEnable()
        {
            bot = fsm as BotFSM;
        }

        void Update()
        {
            Rigidbody2D rb = bot.rigidbody;
            Vector2 velocity = rb.velocity;

            float h = Mathf.Abs(velocity.x);

            // Apply Running Animation | Idle
            bot.animator.SetBool("Running", h > 0.1f);

            // Moving ?
            if (velocity.x == 0)
                return;

            // Calculate Face Direction
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(velocity.x);

            // Flip Face Direction
            transform.localScale = scale;
        }

        void OnCollisionEnter2D(Collision2D c)
        {
            if (!enabled)
                return;

            if (!c.gameObject.CompareTag("Player"))
                return;

            Vector3 direction = c.relativeVelocity.normalized;
            float angle = Vector3.Angle(Vector3.up, direction);

            if (angle > 45f)
                return;

            // Flop & Died
            bot.tween.enabled = false;
            bot.DoTransition(Transition.OnFlop);
        }

    }
}
