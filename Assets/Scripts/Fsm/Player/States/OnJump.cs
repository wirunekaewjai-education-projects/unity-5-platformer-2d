using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnJump : StateBehaviour
    {
        PlayerFSM player;

        Action.HorizontalInput horizontal;
        Action.FaceDetect face;
        Action.JumpAction jump;

        void Awake()
        {
            horizontal = new Action.HorizontalInput();
            face = new Action.FaceDetect();
            jump = new Action.JumpAction();
        }

        void OnEnable()
        {
            player = fsm as PlayerFSM;
            jump.Action(player, Transition.OnAir);
        }

        void Update()
        {
            if (!enabled)
                return;

            horizontal.Action(player);
            face.Action(player);
        }
    }
}
