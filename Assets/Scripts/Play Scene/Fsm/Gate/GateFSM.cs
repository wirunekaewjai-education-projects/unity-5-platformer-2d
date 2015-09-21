using UnityEngine;
using System.Collections;

namespace devdayo.Fsm.Gate
{
	[RequireComponent(typeof(Tween))]
	public class GateFSM : StateMachineBehaviour 
	{
		internal Tween tween;
		public int requireCoins = 1;
		
		void Awake()
		{
			tween = GetComponent<Tween>();	
			tween.autoPlay = false;
		}
		
		void Start()
		{
			AddTransition(Transition.OnClosed,  typeof(State.OnClosed));
			AddTransition(Transition.OnClosing, typeof(State.OnClosing));
			AddTransition(Transition.OnOpened,  typeof(State.OnOpened));
			AddTransition(Transition.OnOpening, typeof(State.OnOpening));
			
			DoTransition(Transition.OnClosed);
		}

	}
	
	public static class Transition
	{
		public const int OnClosed  = 1;
		public const int OnClosing = 2;
		public const int OnOpened  = 3;
		public const int OnOpening = 4;
	}
}