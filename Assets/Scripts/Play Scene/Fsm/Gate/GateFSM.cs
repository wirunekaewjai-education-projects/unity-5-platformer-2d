using UnityEngine;

namespace Devdayo.Platformer2D.Gate
{
	[RequireComponent(typeof(Tween))]
	public class GateFSM : MonoBehaviour 
	{
		internal Tween tween;
        internal Fsm fsm;

		public int requireCoins = 1;
		
		void Awake()
		{
			tween = GetComponent<Tween>();	
			tween.autoPlay = false;

            fsm = new Fsm(this);
		}
		
		void Start()
		{
            fsm.Go<OnClosed>();
		}

	}
}