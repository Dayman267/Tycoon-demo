using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class WarningTextController : MonoBehaviour
    {
        [SerializeField] private GameObject warningText;

        public void ShowWarningText(string text) => StartCoroutine(ShowWarningTextCoroutine(text));
        private IEnumerator ShowWarningTextCoroutine(string text)
        {
            if (warningText.activeSelf)
            {
                yield break;
            }

            warningText.GetComponent<TextMeshProUGUI>().text = text;
            warningText.SetActive(true);
            yield return new WaitForSeconds(2);
            warningText.SetActive(false);
        }
    }
}