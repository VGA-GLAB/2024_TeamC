namespace SoulRunProject.Common
{
    public class FadeSceneChangeModel
    {
        public async void FadeSceneChange(string sceneName)
        {
            await SceneController.Instance.LoadSceneAsync(sceneName, 1.0f);
        }
    }
}