using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public sealed class MenuButtonsController : MonoBehaviour
    {
        public void OpenGameScene() => SceneManager.LoadScene("GameScene");
        public void OpenMenuScene() => SceneManager.LoadScene("MainMenu");

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
