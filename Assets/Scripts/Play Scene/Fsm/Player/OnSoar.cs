using UnityEngine;

namespace Devdayo.Platformer2D.Player
{
    public class OnSoar : FsmStateBehaviour
    {
        PlayerFSM player;

        public override void OnEnter()
        {
            player = fsm.owner as PlayerFSM;
            player.animator.SetBool("Soaring", true);
        }

        public override void OnExit()
        {
            player.animator.SetBool("Soaring", false);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            player.UpdateHorizontal();
        }

        public override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);

            if (other.gameObject.CompareTag(Tag.Bot))
            {
                Vector3 p1 = player.transform.position;
                Vector3 p2 = other.transform.position;

                Vector3 direction = (p1 - p2).normalized;
                float angle = Vector3.Angle(Vector3.up, direction);

                if (angle > 30f)
                    return;

                player.Jump(true);
            }
        }

        public override void OnCollisionStay2D(Collision2D other)
        {
            base.OnCollisionStay2D(other);

            if (other.gameObject.CompareTag(Tag.Elevator))
            {
                fsm.Go<OnElevator>();
            }
            else if (other.gameObject.CompareTag(Tag.Platform))
            {
                float vy = player.rigidbody.velocity.y;
                if (Mathf.Abs(vy) < Mathf.Epsilon)
                {
                    fsm.Go<OnGround>();
                }
            }
            else if (other.gameObject.CompareTag(Tag.Bot))
            {
                Vector3 p1 = player.transform.position;
                Vector3 p2 = other.transform.position;

                Vector3 direction = (p1 - p2).normalized;
                float angle = Vector3.Angle(Vector3.up, direction);

                if (angle <= 30f)
                    return;

                // Die !!!
                player.rigidbody.velocity = Physics2D.gravity;

                fsm.Go<OnFlop>();
            }
        }

        public override void OnTriggerStay2D(Collider2D other)
        {
            base.OnTriggerStay2D(other);

            if (other.gameObject.CompareTag(Tag.Ladder))
            {
                fsm.Go<OnLadder>();
            }
        }

    }
}
