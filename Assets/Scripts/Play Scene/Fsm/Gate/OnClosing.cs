using UnityEngine;

namespace Devdayo.Platformer2D.Gate
{
	public class OnClosing : FsmStateBehaviour
	{
        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (!other.CompareTag(Tag.Player))
                return;

            GateFSM gate = GetOwner<GateFSM>();

            int coin = CoinManager.Instance.coin;
            if (coin < gate.requireCoins)
                return;

            gate.tween.MoveTo(0);

            fsm.Go<OnOpening>();
        }
    }
}
