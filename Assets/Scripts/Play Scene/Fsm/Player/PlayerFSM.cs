using UnityEngine;
using System.Collections;

namespace devdayo.Fsm.Player
{
    public class PlayerFSM : StateMachineBehaviour
    {
        internal Rigidbody2D rigidbody;
        internal Animator animator;

        public Collider2D boxCollider;
        public Collider2D polyCollider;

        public float moveSpeed = 3;
        public float jumpPower = 6;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            AddTransition(Transition.OnSoar,     typeof(State.OnSoar));
            AddTransition(Transition.OnGround,   typeof(State.OnGround));
            AddTransition(Transition.OnLadder,   typeof(State.OnLadder));
            AddTransition(Transition.OnElevator, typeof(State.OnElevator));
			AddTransition(Transition.OnFlop,     typeof(State.OnFlop));
			AddTransition(Transition.OnDied,     typeof(State.OnDied));
			AddTransition(Transition.OnLoad,     typeof(State.OnLoad));

            DoTransition(Transition.OnLoad);
        }

        public void UpdateHorizontal()
        {
            float h = Input.GetAxisRaw("Horizontal");

            // Calculate Move Velocity
            Vector3 velocity = rigidbody.velocity;
            velocity.x = moveSpeed * h;

            // Apply Move Velocity
            rigidbody.velocity = velocity;

            // Apply Running Animation | Idle
            animator.SetBool("Running", h != 0);

            // Moving ?
            if (velocity.x == 0)
                return;

            // Calculate Face Direction
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(velocity.x);

            // Flip Face Direction
            transform.localScale = scale;
        }

        public void UpdateVertical()
        {
            float v = Input.GetAxisRaw("Vertical");

            // Calculate Move Velocity
            Vector3 velocity = rigidbody.velocity;
            velocity.y = moveSpeed * v;

            // Apply Move Velocity
            rigidbody.velocity = velocity;
        }

        public void Jump(bool ignoreInput)
        {
            if (ignoreInput || Input.GetKeyDown(KeyCode.Space))
            {
                // Calculate Move Velocity
                Vector3 velocity = rigidbody.velocity;
                velocity.y = jumpPower;

                // Apply Move Velocity
                rigidbody.velocity = velocity;
            }
        }

    }

    public static class Transition
    {
        public const int OnSoar     = 1;
        public const int OnGround   = 2;
        public const int OnLadder   = 3;
        public const int OnElevator = 4;
        public const int OnFlop     = 5;
		public const int OnDied     = 6;
		public const int OnLoad     = 7;
    }
}