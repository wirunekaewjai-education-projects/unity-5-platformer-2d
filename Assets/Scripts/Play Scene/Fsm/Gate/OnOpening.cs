using UnityEngine;

namespace Devdayo.Platformer2D.Gate
{
    public class OnOpening : FsmStateBehaviour
    {
        GateFSM gate;

        public override void OnEnter()
        {
            gate = GetOwner<GateFSM>();
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            int index = gate.tween.currentIndex;
            if (index == 1)
                return;

            fsm.Go<OnOpened>();
        }

        public override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);

            if (!other.CompareTag(Tag.Player))
                return;
            
            gate.tween.MoveTo(1);

            fsm.Go<OnClosing>();
        }
    }
}
