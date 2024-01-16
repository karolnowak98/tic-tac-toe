using GlassyCode.TTT.Game.TicTacToe.Data;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;
using UnityEngine;
using Zenject;

namespace GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Installers
{
    public class BoardTestInstaller : Installer<BoardTestInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IBoard>().FromMethod(CreateBoard).AsSingle();
        }
        
        private static IBoard CreateBoard(InjectContext context)
        {
            var config = context.Container.TryResolve<ITicTacToeConfig>();

            if (config != null)
            {
                return new Board(config.BoardSize);
            }

            Debug.LogError("Make sure that TicTacToeConfig binding is done before board binding!!");
            return null;
        }
    }
}