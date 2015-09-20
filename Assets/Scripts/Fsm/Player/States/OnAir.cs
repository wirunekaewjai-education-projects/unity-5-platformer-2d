using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnAir : StateBehaviour
    {
        PlayerFSM player;

        Action.HorizontalInput horizontal;
        Action.FaceDetect face;
        Action.PlatformDetect platform;
        Action.LadderDetect ladder;
        Action.BotDetect bot;

        void Awake ()
        {
            horizontal = new Action.HorizontalInput();
            face = new Action.FaceDetect();

            platform = new Action.PlatformDetect();
            ladder = new Action.LadderDetect();
            bot = new Action.BotDetect();
        }

        void OnEnable ()
        {
            player = fsm as PlayerFSM;
        }

        void Update ()
        {
            if (!enabled)
                return;

            horizontal.Action(player);
            face.Action(player);
        }

        void OnCollisionEnter2D(Collision2D c)
        {
            if (!enabled)
                return;

            bot.Collision(player, c, Transition.OnJump, Transition.OnFlop);
        }

        void OnCollisionStay2D(Collision2D c)
        {
            if (!enabled)
                return;

            platform.Stay(player, c, Transition.OnGround);
        }
        
        void OnTriggerStay2D(Collider2D c)
        {
            if (!enabled)
                return;

            ladder.Stay(player, c, Transition.OnLadder);
        }
        
    }
}
