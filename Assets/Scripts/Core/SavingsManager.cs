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

    // public static event Action<string> OnMoneyChanged;
    // public static event Action<string> OnMoneyPerSecondChanged;
    // public static event Action<string> OnDetailsChanged;
    // public static event Action<string> OnDetailsPerSecondChanged;

    public float GetMoney() => _money;
    public float GetMoneyPerCar() => _moneyPerCar;
    public float GetDetails() => _details;
    public float GetDetailsPerCar() => _detailsPerCar;
    
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
        SyncToProjectNames();
    }

    public void AddMoneyPerCar(int money)
    {
        _moneyPerCar += money;
        UpdateMoneyPerCar();
        SyncToProjectNames();
    }
    
    private void AddDetails()
    {
        _details += _detailsPerCar;
        UpdateDetails();
        SyncToProjectNames();
    }
    
    public void AddDetailsPerCar(int details)
    {
        _detailsPerCar += details;
        UpdateDetailsPerCar();
        SyncToProjectNames();
    }

    public void SpentMoney(int money)
    {
        _money -= money;
        UpdateMoney();
        SyncToProjectNames();
    }
    public void SpentDetails(int details)
    {
        _details -= details;
        UpdateDetails();
        SyncToProjectNames();
    }
    
    private void UpdateMoney() => moneyTextMP.text = _money.ToString();
    private void UpdateMoneyPerCar() => moneyPerCarTextMP.text = _moneyPerCar.ToString();
    private void UpdateDetails() => detailsTextMP.text = _details.ToString();
    private void UpdateDetailsPerCar() => detailsPerCarTextMP.text = _detailsPerCar.ToString();
    
    public void LoadFromSave(GameSave save)
    {
        if (save == null) return;

        _money = save.Money;
        _moneyPerCar = save.MoneyPerCar;
        _details = save.Details;
        _detailsPerCar = save.DetailsPerCar;

        ProjectNames.Money = _money;
        ProjectNames.MoneyPerCar = _moneyPerCar;
        ProjectNames.Details = _details;
        ProjectNames.DetailsPerCar = _detailsPerCar;

        UpdateMoney();
        UpdateMoneyPerCar();
        UpdateDetails();
        UpdateDetailsPerCar();
    }

    public void SyncToProjectNames()
    {
        ProjectNames.Money = _money;
        ProjectNames.MoneyPerCar = _moneyPerCar;
        ProjectNames.Details = _details;
        ProjectNames.DetailsPerCar = _detailsPerCar;
    }

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