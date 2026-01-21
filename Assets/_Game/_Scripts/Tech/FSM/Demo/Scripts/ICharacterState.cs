using Core.FSM;

namespace FSM.Demo
{
    public interface ICharacterState : IState<ICharacterState, ICharacterTrigger> { }
}