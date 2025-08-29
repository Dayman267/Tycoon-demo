using TMPro;
using UI;
using UnityEngine;

public sealed class GarageUpgradeButtonController : MonoBehaviour
{
    private int _garageUpgradeCost = ProjectNames.GarageUpgradeCost;
    private int _garageUpgradedCost = ProjectNames.GarageUpgradedCost;
    private int _garageLevel = ProjectNames.GarageLevel;
    
    [SerializeField] private TextMeshProUGUI currentPriceText;
    [SerializeField] private TextMeshProUGUI upgradedText;
    [SerializeField] private TextMeshProUGUI upgradePriceText;
    
    private SavingsManager _savingsManager;
    private WarningTextController _warningTextController;

    private void Awake()
    {
        _savingsManager = GameObject.FindWithTag("SavingsManager").GetComponent<SavingsManager>();
        _warningTextController = GameObject.FindWithTag("WarningText").GetComponent<WarningTextController>();
    }
    
    public void TryUpgrade()
    {
        if (CheckUpgradeAbility())
        {
            _savingsManager.AddDetailsPerCar(_garageUpgradedCost);
            _garageUpgradedCost += 1;
            currentPriceText.text = $"{_savingsManager.GetDetailsPerCar()} per car";
            upgradedText.text = $"+{_garageUpgradedCost} per car";

            _garageUpgradeCost += 5;
            upgradePriceText.text = $"${_garageUpgradeCost}";
            
            _garageLevel += 1;
        }
        else
        {
            _warningTextController.ShowWarningText($"You need ${_garageUpgradeCost} to upgrade this building!");
        }
    }
    
    private bool CheckUpgradeAbility() => _savingsManager.GetMoney() >= _garageUpgradeCost;
    
    public void LoadFromSave(GameSave s)
    {
        if (s == null) return;

        _garageUpgradeCost = s.GarageUpgradeCost;
        _garageUpgradedCost = s.GarageUpgradedCost;
        _garageLevel = s.GarageLevel;

        currentPriceText.text = $"{_savingsManager.GetDetailsPerCar()} per car";
        upgradedText.text = $"+{_garageUpgradedCost} per car";
        upgradePriceText.text = $"${_garageUpgradeCost}";
    }
}
