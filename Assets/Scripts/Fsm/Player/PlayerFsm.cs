using UnityEngine;
using System.Collections;

namespace devdayo.Fsm.Player
{
    public class PlayerFSM : StateMachineBehaviour
    {
        internal Rigidbody2D rigidbody;
        internal Animator animator;

        internal Collider2D boxCollider;
        internal Collider2D polyCollider;

        public float moveSpeed = 3;
        public float jumpPower = 6;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            boxCollider = GetComponent<BoxCollider2D>();
            polyCollider = GetComponent<PolygonCollider2D>();
        }

        void Start()
        {
            AddTransition(Transition.OnAir,     typeof(State.OnAir));
            AddTransition(Transition.OnGround,  typeof(State.OnGround));
            AddTransition(Transition.OnLadder,  typeof(State.OnLadder));
            AddTransition(Transition.OnFlop,    typeof(State.OnFlop));
            AddTransition(Transition.OnDied,    typeof(State.OnDied));

            DoTransition(Transition.OnAir);
        }
    }

    public static class Transition
    {
        public const int OnAir      = 1;
        public const int OnGround   = 2;
        public const int OnLadder   = 3;
        public const int OnFlop     = 4;
        public const int OnDied     = 5;
    }
}