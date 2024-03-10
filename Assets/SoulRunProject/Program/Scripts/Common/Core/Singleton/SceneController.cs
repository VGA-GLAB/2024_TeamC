using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Cysharp.Threading.Tasks;

namespace SoulRunProject.Common
{
    public class SceneController : AbstractSingletonMonoBehaviour<SceneController>
    {
        protected override bool UseDontDestroyOnLoad => true;

        public enum SceneType
        {
            None,
            TitleScene,
            InGameScene,
            SoulMixScene,
        }


        /// <summary>
        /// シーンを普通にロードします。
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// シーンを指定した時間後普通にロードします。
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        /// <param name="delay"> 遅延時間</param>
        public void LoadScene(string sceneName, float delay)
        {
            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ =>
                SceneManager.LoadScene(sceneName));
        }

        /// <summary>
        /// シーンを列挙型で普通にロードします。
        /// </summary>
        /// <param name="sceneType">シーンの列挙型</param>
        public void LoadScene(SceneType sceneType)
        {
            SceneManager.LoadScene(sceneType.ToString());
        }

        /// <summary>
        /// シーンを指定した時間後普通に列挙型でロードします。
        /// </summary>
        /// <param name="sceneType"> シーンの列挙型</param>
        /// <param name="delay"> 遅延時間</param>
        public void LoadScene(SceneType sceneType, float delay)
        {
            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ =>
                SceneManager.LoadScene(sceneType.ToString()));
        }


        /// <summary>
        /// シーンを非同期でロードします。
        /// </summary>
        /// <param name="sceneName"> シーン名</param>
        public async UniTask LoadSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
        }

        /// <summary>
        /// シーンを指定した時間後に非同期でロードします。
        /// </summary>
        /// <param name="sceneName"> シーン名</param>
        /// <param name="delay"> 遅延時間</param>
        public async UniTask LoadSceneAsync(string sceneName, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
        }

        /// <summary>
        /// シーンを列挙型で非同期でロードします。
        /// </summary>
        /// <param name="sceneType"> シーンの列挙型</param>
        public async UniTask LoadSceneAsync(SceneType sceneType)
        {
            await SceneManager.LoadSceneAsync(sceneType.ToString()).ToUniTask();
        }

        /// <summary>
        /// シーンを指定した時間後に列挙型で非同期でロードします。
        /// </summary>
        /// <param name="sceneType"> シーンの列挙型</param>
        /// <param name="delay"> 遅延時間</param>
        public async UniTask LoadSceneAsync(SceneType sceneType, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            await SceneManager.LoadSceneAsync(sceneType.ToString()).ToUniTask();
        }

        /// <summary>
        /// シーンを追加で普通にロードします。
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadAddScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        /// <summary>
        /// シーンを指定した時間後に追加で普通にロードします。
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="delay"></param>
        public void LoadAddScene(string sceneName, float delay)
        {
            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ =>
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive));
        }

        /// <summary>
        /// シーンを列挙型で追加で普通にロードします。
        /// </summary>
        /// <param name="sceneType"></param>
        public void LoadAddScene(SceneType sceneType)
        {
            SceneManager.LoadScene(sceneType.ToString(), LoadSceneMode.Additive);
        }

        /// <summary>
        /// シーンを指定した時間後に列挙型で追加で普通にロードします。
        /// </summary>
        /// <param name="sceneType"></param>
        /// <param name="delay"></param>
        public void LoadAddScene(SceneType sceneType, float delay)
        {
            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ =>
                SceneManager.LoadScene(sceneType.ToString(), LoadSceneMode.Additive));
        }

        /// <summary>
        /// シーンを追加で非同期でロードします。
        /// </summary>
        /// <param name="sceneName"></param>
        public async UniTask LoadAddSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).ToUniTask();
        }

        /// <summary>
        /// シーンを指定した時間後に追加で非同期でロードします。
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="delay"></param>
        public async UniTask LoadAddSceneAsync(string sceneName, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).ToUniTask();
        }

        /// <summary>
        /// シーンを列挙型で追加で非同期でロードします。
        /// </summary>
        /// <param name="sceneType"></param>
        public async UniTask LoadAddSceneAsync(SceneType sceneType)
        {
            await SceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Additive).ToUniTask();
        }

        /// <summary>
        /// シーンを指定した時間後に列挙型で追加で非同期でロードします。
        /// </summary>
        /// <param name="sceneType"></param>
        /// <param name="delay"></param>
        public async UniTask LoadAddSceneAsync(SceneType sceneType, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            await SceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Additive).ToUniTask();
        }

        /// <summary>
        /// シーンを普通にアンロードします。
        /// </summary>
        /// <param name="sceneName"></param>
        public void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        /// <summary>
        /// シーンを指定した時間後普通にアンロードします。
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="delay"></param>
        public void UnloadScene(string sceneName, float delay)
        {
            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ =>
                SceneManager.UnloadSceneAsync(sceneName));
        }

        /// <summary>
        /// シーンを列挙型で普通にアンロードします。
        /// </summary>
        /// <param name="sceneType"></param>
        public void UnloadScene(SceneType sceneType)
        {
            SceneManager.UnloadSceneAsync(sceneType.ToString());
        }

        /// <summary>
        /// シーンを指定した時間後列挙型で普通にアンロードします。
        /// </summary>
        /// <param name="sceneType"></param>
        /// <param name="delay"></param>
        public void UnloadScene(SceneType sceneType, float delay)
        {
            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ =>
                SceneManager.UnloadSceneAsync(sceneType.ToString()));
        }

        /// <summary>
        /// シーンを非同期でアンロードします。
        /// </summary>
        /// <param name="sceneName"></param>
        public async UniTask UnloadSceneAsync(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName).ToUniTask();
        }

        /// <summary>
        /// シーンを指定した時間後に非同期でアンロードします。
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="delay"></param>
        public async UniTask UnloadSceneAsync(string sceneName, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            await SceneManager.UnloadSceneAsync(sceneName).ToUniTask();
        }

        /// <summary>
        /// シーンを列挙型で非同期でアンロードします。
        /// </summary>
        /// <param name="sceneType"></param>
        public async UniTask UnloadSceneAsync(SceneType sceneType)
        {
            await SceneManager.UnloadSceneAsync(sceneType.ToString()).ToUniTask();
        }

        /// <summary>
        /// シーンを指定した時間後に列挙型で非同期でアンロードします。
        /// </summary>
        /// <param name="sceneType"></param>
        /// <param name="delay"></param>
        public async UniTask UnloadSceneAsync(SceneType sceneType, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            await SceneManager.UnloadSceneAsync(sceneType.ToString()).ToUniTask();
        }

        /// <summary>
        /// 現在のシーンを普通にアンロードします。
        /// </summary>
        public void UnloadCurrentScene()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// 現在のシーンを指定した時間後普通にアンロードします。
        /// </summary>
        /// <param name="delay"></param>
        public void UnloadCurrentScene(float delay)
        {
            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ =>
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name));
        }


        /// <summary>
        /// 現在のシーンを指定した時間後非同期にアンロードします。
        /// </summary>
        public async UniTask UnloadCurrentSceneAsync()
        {
            await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name).ToUniTask();
        }

        /// <summary>
        /// 現在のシーンを指定した時間後非同期にアンロードします。
        /// </summary>
        /// <param name="delay"></param>
        public async UniTask UnloadCurrentSceneAsync(float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name).ToUniTask();
        }

        /// <summary>
        /// 現在のシーンを普通に再読み込みします。
        /// </summary>
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// 現在のシーンを指定した時間後非同期に再読み込みします。
        /// </summary>
        public async UniTask ReloadSceneAsync()
        {
            await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name).ToUniTask();
        }

        /// <summary>
        /// 現在のシーンを指定した時間後普通に再読み込みします。
        /// </summary>
        /// <param name="delay"></param>
        public void ReloadScene(float delay)
        {
            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ =>
                SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        }

        /// <summary>
        /// 現在のシーンを指定した時間後非同期に再読み込みします。
        /// </summary>
        /// <param name="delay"></param>
        public async UniTask ReloadSceneAsync(float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name).ToUniTask();
        }

        public bool IsSceneLoaded(string sceneName)
        {
            return SceneManager.GetSceneByName(sceneName).isLoaded;
        }

        public bool IsSceneLoaded(SceneType sceneType)
        {
            return SceneManager.GetSceneByName(sceneType.ToString()).isLoaded;
        }
    }
}