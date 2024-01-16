namespace GlassyCode.TTT.Core.SceneLoader
{
    public interface ISceneLoader
    {
         void LoadMenuScene();
         void LoadMenuSceneAsync();
         void LoadGameSceneAsync();
         void LoadGameScene();
    }
}
