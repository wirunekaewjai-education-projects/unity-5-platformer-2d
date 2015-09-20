using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnLadder : StateBehaviour
    {
        PlayerFSM player;
        float gravityScale;

        void OnEnable()
        {
            player = fsm as PlayerFSM;

            gravityScale = player.rigidbody.gravityScale;
            player.rigidbody.gravityScale = 0;
        }

        void OnDisable()
        {
            player.rigidbody.gravityScale = gravityScale;
            gravityScale = 0;
        }

        void Update()
        {
            if (!enabled)
                return;

            player.UpdateHorizontal();
            player.UpdateVertical();
        }

        void OnCollsionEnter2D(Collision2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Bot))
            {
                Vector3 direction = c.relativeVelocity.normalized;
                float angle = Vector3.Angle(Vector3.down, direction);

                // Bounce Up (Jump) or Die
                if (angle >= 45f)
                {
                    player.DoTransition(Transition.OnFlop);
                }
            }
        }

        void OnTriggerExit2D(Collider2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Ladder))
            {
                player.DoTransition(Transition.OnAir);
            }
        }
    }
}
