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

        void OnFlipColliderState()
        {
			player.polyCollider.enabled = !player.polyCollider.enabled;
        }

        void OnCollisionEnter2D(Collision2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Bot))
            {
                player.polyCollider.enabled = false;
				Invoke("OnFlipColliderState", Time.deltaTime);
				Invoke("OnFlipColliderState", Time.deltaTime * 3f);
            }
        }


        void OnCollisionStay2D(Collision2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Platform) ||
                c.gameObject.CompareTag(Tag.Elevator) ||
                c.gameObject.CompareTag(Tag.Ladder))
            {
                Rigidbody2D rb = player.rigidbody;

                rb.gravityScale = 0;
                rb.Sleep();

                player.boxCollider.enabled = false;
                player.polyCollider.enabled = false;

                player.DoTransition(Transition.OnDied);
            }
        }
    }
}
