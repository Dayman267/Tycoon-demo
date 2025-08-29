using TMPro;
using UI;
using UnityEngine;

public sealed class BuildingUpgradeButtonController : MonoBehaviour
{
    private int _upgradeCost = ProjectNames.UpgradeCost;
    private int _upgradeDetailsCost = ProjectNames.UpgradeDetailsCost;
    private int _upgradedCost = ProjectNames.UpgradedCost;
    private int _gasLevel = ProjectNames.GasLevel;
    
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
            _savingsManager.SpentMoney(_upgradeCost);
            if (_gasLevel % 5 == 0)
            {
                _savingsManager.SpentDetails(_upgradeDetailsCost);
            }
            
            _savingsManager.AddMoneyPerCar(_upgradedCost);
            _upgradedCost += 1;
            currentPriceText.text = $"${_savingsManager.GetMoneyPerCar()} per car";
            upgradedText.text = $"+${_upgradedCost} per car";

            _upgradeCost += 5;

            _gasLevel += 1;
            if (_gasLevel % 5 == 0)
            {
                upgradePriceText.text = $"${_upgradeCost} and {_upgradeDetailsCost} details";
                _upgradeDetailsCost += 1;
            }
            else
            {
                upgradePriceText.text = $"${_upgradeCost}";
            }
        }
        else
        {
            if (_gasLevel % 5 == 0)
            {
                _warningTextController.ShowWarningText($"You need ${_upgradeCost} and {_upgradeDetailsCost} details to upgrade this building!");
            }
            else
            {
                _warningTextController.ShowWarningText($"You need ${_upgradeCost} to upgrade this building!");
            }
        }
    }
    
    private bool CheckUpgradeAbility()
    {
        if (_gasLevel % 5 == 0)
        {
            return _savingsManager.GetMoney() >= _upgradeCost && _savingsManager.GetDetails() >= _upgradeDetailsCost;
        }
        return _savingsManager.GetMoney() >= _upgradeCost;
    }
    
    public void LoadFromSave(GameSave s)
    {
        if (s == null) return;

        _upgradeCost = s.UpgradeCost;
        _upgradeDetailsCost = s.UpgradeDetailsCost;
        _upgradedCost = s.UpgradedCost;
        _gasLevel = s.GasLevel;

        // Обновляем тексты, используя текущие значения в SavingsManager
        currentPriceText.text = $"${_savingsManager.GetMoneyPerCar()} per car";
        upgradedText.text = $"+${_upgradedCost} per car";
        if (_gasLevel % 5 == 0)
            upgradePriceText.text = $"${_upgradeCost} and {_upgradeDetailsCost} details";
        else
            upgradePriceText.text = $"${_upgradeCost}";
    }
}
