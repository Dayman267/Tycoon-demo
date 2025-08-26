using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public sealed class MenuButtonsController : MonoBehaviour
    {
        public void OpenGameScene() => SceneManager.LoadScene(1);

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
