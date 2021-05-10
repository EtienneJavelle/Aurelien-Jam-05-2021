﻿using UnityEngine;

namespace CMF {
    //This character movement input class is an example of how to get input from a gamepad/joystick to control the character;
    //It comes with a dead zone threshold setting to bypass any unwanted joystick "jitter";
    public class CharacterJoystickInput : CharacterInput {

        public string horizontalInputAxis = "Horizontal";
        public string verticalInputAxis = "Vertical";
        public KeyCode jumpKey = KeyCode.Joystick1Button0;
        public KeyCode shootKey = KeyCode.Joystick1Button14;

        //If this is enabled, Unity's internal input smoothing is bypassed;
        public bool useRawInput = true;

        //If any input falls below this value, it is set to '0';
        //Use this to prevent any unwanted small movements of the joysticks ("jitter");
        public float deadZoneThreshold = 0.2f;

        public override float GetHorizontalMovementInput() {
            float _horizontalInput;

            if(this.useRawInput) {
                _horizontalInput = Input.GetAxisRaw(this.horizontalInputAxis);
            } else {
                _horizontalInput = Input.GetAxis(this.horizontalInputAxis);
            }

            //Set any input values below threshold to '0';
            if(Mathf.Abs(_horizontalInput) < this.deadZoneThreshold) {
                _horizontalInput = 0f;
            }

            return _horizontalInput;
        }

        public override float GetVerticalMovementInput() {
            float _verticalInput;

            if(this.useRawInput) {
                _verticalInput = Input.GetAxisRaw(this.verticalInputAxis);
            } else {
                _verticalInput = Input.GetAxis(this.verticalInputAxis);
            }

            //Set any input values below threshold to '0';
            if(Mathf.Abs(_verticalInput) < this.deadZoneThreshold) {
                _verticalInput = 0f;
            }

            return _verticalInput;
        }

        public override bool IsJumpKeyPressed() {
            return Input.GetKey(this.jumpKey);
        }

        public override bool IsShootKeyPressed() {
            return Input.GetKey(this.shootKey);
        }
    }
}
