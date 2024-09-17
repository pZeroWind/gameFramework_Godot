using Framework;

namespace GameApp;

public partial class PlayerStateMachine : StateMachine
{
    protected override void OnInitialize()
    {
        StateRegister<PlayerIdleState>(State.Idle);
        StateRegister<PlayerMoveState>(State.Move);
    }
}