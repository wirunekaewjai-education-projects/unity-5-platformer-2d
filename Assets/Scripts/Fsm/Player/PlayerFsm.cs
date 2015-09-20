using UnityEngine;
using System.Collections;

namespace devdayo.Fsm.Player
{
    public class PlayerFSM : StateMachineBehaviour
    {
        public Rigidbody2D rigidbody;
        public Animator animator;

        public Collider2D boxCollider;
        public Collider2D polyCollider;

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
            AddTransition(Transition.OnJump,    typeof(State.OnJump));
            AddTransition(Transition.OnGround,  typeof(State.OnGround));
            AddTransition(Transition.OnLadder,  typeof(State.OnLadder));
            AddTransition(Transition.OnFlop,    typeof(State.OnFlop));
            AddTransition(Transition.OnDied,    typeof(State.OnDied));

            DoTransition(Transition.OnAir);
        }
    }

    public static class Transition
    {
        public const int OnAir      = 0;
        public const int OnJump     = 1;
        public const int OnGround   = 2;
        public const int OnLadder   = 3;
        public const int OnFlop     = 4;
        public const int OnDied     = 5;
    }
}