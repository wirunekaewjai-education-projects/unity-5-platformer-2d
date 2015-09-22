using UnityEngine;

namespace Devdayo.Platformer2D.Player
{
    public class OnElevator : FsmStateBehaviour
    {
        PlayerFSM player;

        public override void OnEnter()
        {
            player = fsm.owner as PlayerFSM;
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            player.UpdateHorizontal();
            player.Jump(false);
        }

        public override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);

            if (other.gameObject.CompareTag(Tag.Bot))
            {
                Vector3 direction = other.relativeVelocity.normalized;
                float angle = Vector3.Angle(Vector3.down, direction);

                // Bounce Up (Jump) or Die
                if (angle >= 45f)
                {
                    fsm.Go<OnFlop>();
                }
            }
        }

        public override void OnCollisionStay2D(Collision2D other)
        {
            base.OnCollisionStay2D(other);

            if (other.gameObject.CompareTag(Tag.Platform))
            {
                float vy = player.rigidbody.velocity.y;
                if (Mathf.Abs(vy) < Mathf.Epsilon)
                {
                    fsm.Go<OnGround>();
                }
            }
        }

        public override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);

            if (other.gameObject.CompareTag(Tag.Elevator))
            {
                fsm.Go<OnSoar>();
            }
        }
    }
}
