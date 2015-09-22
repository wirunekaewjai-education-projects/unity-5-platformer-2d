using System;
using UnityEngine;

namespace Devdayo.Platformer2D.Bot
{
    public class OnMove : FsmStateBehaviour
    {
        BotFSM bot;

        public override void OnEnter()
        {
            bot = GetOwner<BotFSM>();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            Rigidbody2D rb = bot.rigidbody;
            Vector2 velocity = rb.velocity;

            float h = Mathf.Abs(velocity.x);

            // Apply Running Animation | Idle
            bot.animator.SetBool("Running", h > 0.1f);

            // Moving ?
            if (velocity.x == 0)
                return;

            // Calculate Face Direction
            Vector3 scale = bot.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(velocity.x);

            // Flip Face Direction
            bot.transform.localScale = scale;
        }

        public override void OnExit()
        {
            
        }

        public override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);

            if (bot.immortal)
                return;

            if (!other.gameObject.CompareTag(Tag.Player))
                return;

            Vector3 p1 = other.transform.position;
            Vector3 p2 = bot.transform.position;

            Vector3 direction = (p1 - p2).normalized;
            float angle = Vector3.Angle(Vector3.up, direction);

            if (angle > 30f)
                return;

            // Flop & Died
            bot.tween.enabled = false;

            fsm.Go<OnFlop>();
        }
    }
}
