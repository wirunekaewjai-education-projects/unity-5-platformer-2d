using UnityEngine;
using System.Collections;

namespace Devdayo.Platformer2D.Player
{
    public class OnFlop : FsmStateBehaviour
    {
        PlayerFSM player;

        public override void OnEnter()
        {
            player = fsm.owner as PlayerFSM;
            player.animator.SetTrigger("Flopping");

            player.boxCollider.enabled = false;
            player.polyCollider.enabled = true;
        }

        public override void OnExit()
        {
            player.rigidbody.Sleep();
        }
        
        IEnumerator OnFlipColliderState()
        {
            yield return new WaitForEndOfFrame();
			player.polyCollider.enabled = !player.polyCollider.enabled;
        }

        public override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);

            if (other.gameObject.CompareTag(Tag.Bot))
            {
                player.polyCollider.enabled = false;
                player.StartCoroutine(OnFlipColliderState());
            }
        }

        public override void OnCollisionStay2D(Collision2D other)
        {
            base.OnCollisionStay2D(other);

            if (other.gameObject.CompareTag(Tag.Platform) ||
                other.gameObject.CompareTag(Tag.Elevator) ||
                other.gameObject.CompareTag(Tag.Ladder))
            {
                player.rigidbody.Sleep();

                player.boxCollider.enabled = false;
                player.polyCollider.enabled = false;

                fsm.Go<OnDied>();
            }
        }
    }
}
