public class IAInputs : CMF.CharacterInput {
    public override float GetHorizontalMovementInput() {
        throw new System.NotImplementedException();
    }

    public override float GetVerticalMovementInput() {
        throw new System.NotImplementedException();
    }

    public override bool IsJumpKeyPressed() {
        throw new System.NotImplementedException();
    }

    public override bool IsShootKeyPressed() {
        return true;
    }
}
