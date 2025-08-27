using TMPro;
using UnityEngine;

public sealed class SavingsManager : MonoBehaviour
{
    private float _money = ProjectNames.Money;
    private float _moneyPerCar = ProjectNames.MoneyPerCar;
    private float _details = ProjectNames.Details;
    private float _detailsPerCar = ProjectNames.DetailsPerCar;

    [SerializeField] private TextMeshProUGUI moneyTextMP;
    [SerializeField] private TextMeshProUGUI moneyPerCarTextMP;
    [SerializeField] private TextMeshProUGUI detailsTextMP;
    [SerializeField] private TextMeshProUGUI detailsPerCarTextMP;

    public float GetMoney() => _money;
    public float GetDetails() => _details;
    
    private void Awake()
    {
        moneyTextMP.text = _money.ToString();
        moneyPerCarTextMP.text = $"{_moneyPerCar}/car";
        detailsTextMP.text = _details.ToString();
        detailsPerCarTextMP.text = $"{_detailsPerCar}/car";
    }


    private void AddMoney()
    {
        _money += _moneyPerCar;
        UpdateMoney();
    }
    
    private void AddDetails()
    {
        _details += _detailsPerCar;
        UpdateDetails();
    }

    public void SpentMoney(int money)
    {
        _money -= money;
        UpdateMoney();
    }
    public void SpentDetails(int details)
    {
        _details -= details;
        UpdateDetails();
    }
    
    private void UpdateMoney() => moneyTextMP.text = _money.ToString();
    private void UpdateDetails() => detailsTextMP.text = _details.ToString();

    private void OnEnable()
    {
        CarBehaviour.OnRefueled += AddMoney;
        LorryDespawnController.OnLorryEntered += AddDetails;
    }

    private void OnDisable()
    {
        CarBehaviour.OnRefueled -= AddMoney;
        LorryDespawnController.OnLorryEntered -= AddDetails;
    }
}
