using UnityEngine;

namespace devdayo.Fsm.Player.Action
{
    public struct HorizontalInput
    {
        public void Action(PlayerFSM player)
        {
            float moveSpeed = player.moveSpeed;

            float h = Input.GetAxisRaw("Horizontal");

            // Calculate Move Velocity
            Vector3 velocity = player.rigidbody.velocity;
            velocity.x = moveSpeed * h;

            // Apply Move Velocity
            player.rigidbody.velocity = velocity;

            // Apply Running Animation | Idle
            player.animator.SetBool("Running", h != 0);
        }
    }

    public class VerticalInput
    {
        public void Action(PlayerFSM player)
        {
            float moveSpeed = player.moveSpeed;

            float v = Input.GetAxisRaw("Vertical");

            // Calculate Move Velocity
            Vector3 velocity = player.rigidbody.velocity;
            velocity.y = moveSpeed * v;

            // Apply Move Velocity
            player.rigidbody.velocity = velocity;
        }
    }

    public class JumpInput
    {
        public void Action(PlayerFSM player)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                float jumpPower = player.jumpPower;

                // Calculate Move Velocity
                Vector3 velocity = player.rigidbody.velocity;
                velocity.y = jumpPower;

                // Apply Move Velocity
                player.rigidbody.velocity = velocity;
            }
        }
    }

    public class JumpAction
    {
        public void Action(PlayerFSM player, int transitionId)
        {
            float jumpPower = player.jumpPower;

            // Calculate Move Velocity
            Vector3 velocity = player.rigidbody.velocity;
            velocity.y = jumpPower;

            // Apply Move Velocity
            player.rigidbody.velocity = velocity;
            
            player.DoTransition(transitionId);
        }
    }

    public struct FaceDetect
    {
        public void Action(PlayerFSM player)
        {
            Rigidbody2D rb = player.rigidbody;

            // Moving ?
            if (rb.velocity.x == 0)
                return;

            // Calculate Face Direction
            Vector3 scale = player.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(rb.velocity.x);

            // Flip Face Direction
            player.transform.localScale = scale;
        }
    }

    public struct PlatformDetect
    {
        public bool Stay(PlayerFSM player, Collision2D other, int transitionId)
        {
            if (!other.gameObject.CompareTag(Tag.Platform))
                return false;
            
            float vy = player.rigidbody.velocity.y;

            if (Mathf.Abs(vy) > Mathf.Epsilon)
                return false;

            player.DoTransition(transitionId);
            return true;
        }

        public bool Exit(PlayerFSM player, Collision2D other, int transitionId)
        {
            if (!other.gameObject.CompareTag(Tag.Platform))
                return false;

            player.DoTransition(transitionId);
            return true;
        }
    }

    public struct LadderDetect
    {
        public bool Stay(PlayerFSM player, Collider2D other, int transitionId)
        {
            if (!other.gameObject.CompareTag(Tag.Ladder))
                return false;
            
            player.DoTransition(transitionId);
            return true;
        }

        public bool Exit(PlayerFSM player, Collider2D other, int transitionId)
        {
            if (!other.gameObject.CompareTag(Tag.Ladder))
                return false;

            player.DoTransition(transitionId);
            return true;
        }
    }

    public class BotDetect
    {
        public bool Collision(PlayerFSM player, Collision2D other, int onHeadTransitionId, int onBodyTransitionId)
        {
            if (!other.gameObject.CompareTag(Tag.Bot))
                return false;

            Vector3 direction = other.relativeVelocity.normalized;
            float angle = Vector3.Angle(Vector3.down, direction);

            // Bounce Up (Jump) or Die
            if (angle < 45f)
            {
                if(onHeadTransitionId > -1)
                    player.DoTransition(onHeadTransitionId);
            }
            else if(onBodyTransitionId > -1)
            {
                player.DoTransition(onBodyTransitionId);
            }

            return true;
        }
    }
    

}
