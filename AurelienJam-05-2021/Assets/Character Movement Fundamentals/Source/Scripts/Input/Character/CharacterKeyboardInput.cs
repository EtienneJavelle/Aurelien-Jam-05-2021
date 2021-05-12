using UnityEngine;

namespace CMF {
    //This character movement input class is an example of how to get input from a keyboard to control the character;
    public class CharacterKeyboardInput : CharacterInput {
        [SerializeField] private string horizontalInputAxis = "Horizontal";
        [SerializeField] private string verticalInputAxis = "Vertical";
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;
        [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
        [SerializeField] private KeyCode specialKey = KeyCode.Mouse1;

        //If this is enabled, Unity's internal input smoothing is bypassed;
        [SerializeField] private bool useRawInput = true;

        public override float GetHorizontalMovementInput() {
            if(useRawInput) {
                return Input.GetAxisRaw(horizontalInputAxis);
            } else {
                return Input.GetAxis(horizontalInputAxis);
            }
        }

        public override float GetVerticalMovementInput() {
            if(useRawInput) {
                return Input.GetAxisRaw(verticalInputAxis);
            } else {
                return Input.GetAxis(verticalInputAxis);
            }
        }

        public override bool IsJumpKeyPressed() {
            return Input.GetKey(jumpKey);
        }

        public override bool IsShootKeyPressed() {
            return Input.GetKey(shootKey);
        }

        public override bool IsSpecialKeyPressed() {
            return Input.GetKey(specialKey);
        }
    }
}
