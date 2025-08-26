using UnityEngine;
using UnityEngine.UI;

public sealed class ButtonPressInvoker : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    public void InvokeOnClick()
    {
        foreach (Button button in _buttons)
            button.onClick.Invoke();
    }
}
