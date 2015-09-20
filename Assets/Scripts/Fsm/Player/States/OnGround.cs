using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnGround : StateBehaviour
    {
        PlayerFSM player;

        Action.HorizontalInput horizontal;
        Action.JumpInput jump;
        Action.FaceDetect face;
        Action.PlatformDetect platform;
        Action.LadderDetect ladder;
        Action.BotDetect bot;

        void Awake()
        {
            horizontal = new Action.HorizontalInput();
            jump = new Action.JumpInput();
            face = new Action.FaceDetect();

            platform = new Action.PlatformDetect();
            ladder = new Action.LadderDetect();
            bot = new Action.BotDetect();
        }

        void OnEnable()
        {
            player = fsm as PlayerFSM;
        }

        void Update()
        {
            if (!enabled)
                return;

            horizontal.Action(player);
            jump.Action(player);

            face.Action(player);
        }

        void OnCollisionEnter2D(Collision2D c)
        {
            if (!enabled)
                return;

            bot.Collision(player, c, -1, Transition.OnFlop);
        }

        void OnCollisionExit2D(Collision2D c)
        {
            if (!enabled)
                return;

            platform.Exit(player, c, Transition.OnAir);
        }

        void OnTriggerStay2D(Collider2D c)
        {
            if (!enabled)
                return;

            ladder.Stay(player, c, Transition.OnLadder);
        }

    }
}
