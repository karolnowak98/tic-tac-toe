using UnityEngine.SceneManagement;

namespace GlassyCode.TTT.Core.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private const int MenuSceneId = 0;
        private const int GameSceneId = 1;

        public void LoadMenuScene()
        {
            SceneManager.LoadScene(MenuSceneId);
        }
        
        public void LoadMenuSceneAsync()
        {
            SceneManager.LoadScene(MenuSceneId);
        }
        
        public void LoadGameScene()
        {
            SceneManager.LoadScene(GameSceneId, LoadSceneMode.Single);
        }
        
        public void LoadGameSceneAsync()
        {
            SceneManager.LoadSceneAsync(GameSceneId);
        }
    }
}