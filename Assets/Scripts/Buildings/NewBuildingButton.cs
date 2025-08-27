using System.Collections;
using TMPro;
using UnityEngine;

public sealed class NewBuildingButton : MonoBehaviour
{
    [SerializeField] private int buildingPrice = ProjectNames.GaragePrice;
    [SerializeField] private GameObject[] buildings;

    [SerializeField] private GameObject textCantBuild;

    private SavingsManager _savingsManager;

    private void Awake()
    {
        _savingsManager = GameObject.FindWithTag("SavingsManager").GetComponent<SavingsManager>();
    }

    private void BuildBuilding()
    {
        foreach (GameObject building in buildings)
        {
            building.SetActive(true);
        }
        _savingsManager.SpentMoney(buildingPrice);
        textCantBuild.SetActive(false);
        Destroy(this);
    }

    public void CheckBuildPossibilityAndBuild()
    {
        if (_savingsManager.GetMoney() < buildingPrice)
        {
            StartCoroutine(ShowWarningText());
        }
        else
        {
            BuildBuilding();
        }
    }

    private IEnumerator ShowWarningText()
    {
        if (textCantBuild.activeSelf)
        {
            yield break;
        }

        textCantBuild.GetComponent<TextMeshProUGUI>().text = $"You need ${buildingPrice} to build this!";
        textCantBuild.SetActive(true);
        yield return new WaitForSeconds(2);
        textCantBuild.SetActive(false);
    }
}
