using UnityEngine;

namespace devdayo.Fsm.Gate.State
{
    public class OnClosed : StateBehaviour
    {
		GateFSM gate;

        void OnEnable()
        {
			gate = fsm as GateFSM;
        }

		void OnTriggerEnter2D(Collider2D c)
		{
			if (!enabled)
				return;

			if (!c.CompareTag (Tag.Player))
				return;
			
			int coin = CoinManager.Instance.coin;
			if (coin < gate.requireCoins)
				return;

			gate.tween.MoveTo(0);
			gate.DoTransition(Transition.OnOpening);
		}
    }
}
