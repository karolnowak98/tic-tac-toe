namespace GlassyCode.TTT.Core.States
{
    public interface IState<in T>
    {
        void Enter(T owner, params object[] optionalParams);
        void Exit(T owner, params object[] optionalParams);
        void Tick();
    }
}
