using UnityEngine;

namespace Devdayo.Platformer2D.Gate
{
	public class OnOpened : FsmStateBehaviour
	{
        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);

            if (!other.CompareTag(Tag.Player))
                return;

            GateFSM gate = GetOwner<GateFSM>();
            gate.tween.MoveTo(1);

            fsm.Go<OnClosing>();
        }
    }
}
