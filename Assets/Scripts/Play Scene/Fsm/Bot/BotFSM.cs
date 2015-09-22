using UnityEngine;
using System.Collections;

namespace Devdayo.Platformer2D.Bot
{
    [RequireComponent(typeof(Tween))]
    public class BotFSM : MonoBehaviour
    {
        internal Rigidbody2D rigidbody;
        internal Animator animator;

        internal Fsm fsm;

        public Collider2D boxCollider;
        public Collider2D polyCollider;

        public Tween tween;

        public float moveSpeed = 3;
        public float jumpPower = 6;

        public bool immortal = false;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            fsm = new Fsm(this);
        }

        void Start()
        {
            fsm.Go<OnMove>();
        }
    }
}