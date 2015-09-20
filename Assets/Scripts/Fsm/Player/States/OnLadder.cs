using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnLadder : StateBehaviour
    {
        PlayerFSM player;

        Action.HorizontalInput horizontal;
        Action.VerticalInput vertical;

        Action.FaceDetect face;
        
        Action.LadderDetect ladder;
        Action.BotDetect bot;

        private float gravityScale;

        void Awake()
        {
            horizontal = new Action.HorizontalInput();
            vertical = new Action.VerticalInput();

            face = new Action.FaceDetect();
            
            ladder = new Action.LadderDetect();
            bot = new Action.BotDetect();
        }

        void OnEnable()
        {
            player = fsm as PlayerFSM;

            gravityScale = player.rigidbody.gravityScale;
            player.rigidbody.gravityScale = 0;
        }

        void OnDisable()
        {
            player.rigidbody.gravityScale = gravityScale;
            gravityScale = 0;
        }

        void Update()
        {
            if (!enabled)
                return;

            horizontal.Action(player);
            vertical.Action(player);

            face.Action(player);
        }

        void OnCollsionEnter2D(Collision2D c)
        {
            if (!enabled)
                return;

            int flopId = Transition.OnFlop;
            bot.Collision(player, c, flopId, flopId);
        }

        void OnTriggerExit2D(Collider2D c)
        {
            if (!enabled)
                return;

            ladder.Exit(player, c, Transition.OnAir);
        }
    }
}
