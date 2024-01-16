namespace GlassyCode.TTT.Game.TicTacToe.Logic.Movement
{
    public interface IMoveHistory
    {
        Move? UndoMove();
        bool IsEmpty { get; }
        void AddMove(Move move);
        void Clear();
    }
}