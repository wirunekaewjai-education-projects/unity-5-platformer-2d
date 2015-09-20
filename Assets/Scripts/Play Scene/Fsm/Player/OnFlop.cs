using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnFlop : StateBehaviour
    {
        PlayerFSM player;

        void OnEnable()
        {
            player = fsm as PlayerFSM;
            player.animator.SetTrigger("Flopping");

            player.boxCollider.enabled = false;
            player.polyCollider.enabled = true;
        }

        void Update()
        {
            if (!enabled)
                return;

            Rigidbody2D rb = player.rigidbody;

            if (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon)
                return;

            rb.gravityScale = 0;
            rb.Sleep();

            player.boxCollider.enabled = false;
            player.polyCollider.enabled = false;

            player.DoTransition(Transition.OnDied);
        }
    }
}
