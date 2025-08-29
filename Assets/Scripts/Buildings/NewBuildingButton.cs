using UI;
using UnityEngine;

public sealed class NewBuildingButton : MonoBehaviour
{
    private bool _isGarageBuilt = ProjectNames.isGarageBuilt;
    private int buildingPrice = ProjectNames.GaragePrice;
    [SerializeField] private GameObject[] buildings;

    [SerializeField] private GameObject textCantBuild;

    private SavingsManager _savingsManager;
    private WarningTextController _warningTextController;

    private void Awake()
    {
        _savingsManager = GameObject.FindWithTag("SavingsManager").GetComponent<SavingsManager>();
        _warningTextController = GameObject.FindWithTag("WarningText").GetComponent<WarningTextController>();
        if (ProjectNames.isGarageBuilt)
        {
            RestoreBuilding();
        }
    }

    private void BuildBuilding()
    {
        foreach (GameObject building in buildings)
        {
            building.SetActive(true);
        }
        _savingsManager.SpentMoney(buildingPrice);
        ProjectNames.isGarageBuilt = true;
        _isGarageBuilt = true;
        _savingsManager.SyncToProjectNames();
        textCantBuild.SetActive(false);
        Destroy(this);
    }

    public void CheckBuildPossibilityAndBuild()
    {
        if (_savingsManager.GetMoney() < buildingPrice)
        {
            _warningTextController.ShowWarningText($"You need ${buildingPrice} to build this!");
        }
        else
        {
            BuildBuilding();
        }
    }

    public void RestoreBuilding()
    {
        foreach (GameObject building in buildings)
        {
            building.SetActive(true);
        }
        Destroy(this);
    }
}
