using UnityEngine;

namespace CMF {
    //This character movement input class is an example of how to get input from a keyboard to control the character;
    public class CharacterKeyboardInput : CharacterInput {
        [SerializeField] private string horizontalInputAxis = "Horizontal";
        [SerializeField] private string verticalInputAxis = "Vertical";
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;
        [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;

        //If this is enabled, Unity's internal input smoothing is bypassed;
        [SerializeField] private bool useRawInput = true;

        public override float GetHorizontalMovementInput() {
            if(this.useRawInput) {
                return Input.GetAxisRaw(this.horizontalInputAxis);
            } else {
                return Input.GetAxis(this.horizontalInputAxis);
            }
        }

        public override float GetVerticalMovementInput() {
            if(this.useRawInput) {
                return Input.GetAxisRaw(this.verticalInputAxis);
            } else {
                return Input.GetAxis(this.verticalInputAxis);
            }
        }

        public override bool IsJumpKeyPressed() {
            return Input.GetKey(this.jumpKey);
        }

        public override bool IsShootKeyPressed() {
            return Input.GetKey(this.shootKey);
        }
    }
}
