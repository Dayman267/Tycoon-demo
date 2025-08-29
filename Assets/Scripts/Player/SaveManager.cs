using UnityEngine;
using System;
using System.IO;

[Serializable]
public class GameSave
{
    public float Money;
    public float MoneyPerCar;
    public float Details;
    public float DetailsPerCar;

    public int GaragePrice;

    public int UpgradeCost;
    public int UpgradeDetailsCost;
    public int GarageUpgradeCost;
    public int UpgradedCost;
    public int GarageUpgradedCost;

    public int GasLevel;
    public int GarageLevel;
    public bool IsGarageBuilt;

    public string Timestamp;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    string savePath => Path.Combine(Application.persistentDataPath, "save.json");

    [Header("Auto save")]
    public float autoSaveInterval = 30f;
    Coroutine _autoSaveRoutine;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (autoSaveInterval > 0f)
            _autoSaveRoutine = StartCoroutine(AutoSaveRoutine());
    }

    System.Collections.IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveInterval);
            Save();
        }
    }

    void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        try
        {
            var root = GatherSave();
            root.Timestamp = DateTime.UtcNow.ToString("o");
            var json = JsonUtility.ToJson(root, true);
            File.WriteAllText(savePath, json);
            Debug.Log($"[SaveManager] Saved to {savePath}");
        }
        catch (Exception e)
        {
            Debug.LogError("[SaveManager] Save failed: " + e);
        }
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("[SaveManager] No save file found.");
            return;
        }

        try
        {
            var json = File.ReadAllText(savePath);
            var root = JsonUtility.FromJson<GameSave>(json);
            ApplySave(root);
            Debug.Log($"[SaveManager] Loaded save from {root.Timestamp}");
        }
        catch (Exception e)
        {
            Debug.LogError("[SaveManager] Load failed: " + e);
        }
    }

    GameSave GatherSave()
    {
        var s = new GameSave();

        s.Money = ProjectNames.Money;
        s.MoneyPerCar = ProjectNames.MoneyPerCar;
        s.Details = ProjectNames.Details;
        s.DetailsPerCar = ProjectNames.DetailsPerCar;

        s.GaragePrice = ProjectNames.GaragePrice;

        s.UpgradeCost = ProjectNames.UpgradeCost;
        s.UpgradeDetailsCost = ProjectNames.UpgradeDetailsCost;
        s.GarageUpgradeCost = ProjectNames.GarageUpgradeCost;
        s.UpgradedCost = ProjectNames.UpgradedCost;
        s.GarageUpgradedCost = ProjectNames.GarageUpgradedCost;

        s.GasLevel = ProjectNames.GasLevel;
        s.GarageLevel = ProjectNames.GarageLevel;
        s.IsGarageBuilt = ProjectNames.isGarageBuilt;

        return s;
    }

    void ApplySave(GameSave s)
    {
        ProjectNames.Money = s.Money;
        ProjectNames.MoneyPerCar = s.MoneyPerCar;
        ProjectNames.Details = s.Details;
        ProjectNames.DetailsPerCar = s.DetailsPerCar;

        ProjectNames.GaragePrice = s.GaragePrice;

        ProjectNames.UpgradeCost = s.UpgradeCost;
        ProjectNames.UpgradeDetailsCost = s.UpgradeDetailsCost;
        ProjectNames.GarageUpgradeCost = s.GarageUpgradeCost;
        ProjectNames.UpgradedCost = s.UpgradedCost;
        ProjectNames.GarageUpgradedCost = s.GarageUpgradedCost;

        ProjectNames.GasLevel = s.GasLevel;
        ProjectNames.GarageLevel = s.GarageLevel;
        ProjectNames.isGarageBuilt = s.IsGarageBuilt;

        var savings = FindObjectOfType<SavingsManager>();
        if (savings != null) savings.LoadFromSave(s);
        
        var newBuildingBtn = FindObjectOfType<NewBuildingButton>();
        if (newBuildingBtn != null && ProjectNames.isGarageBuilt)
        {
            newBuildingBtn.RestoreBuilding();
        }

        var buildingUpgradeButtons = FindObjectsOfType<BuildingUpgradeButtonController>();
        foreach (var b in buildingUpgradeButtons) b.LoadFromSave(s);

        var garageUpgradeButtons = FindObjectsOfType<GarageUpgradeButtonController>();
        foreach (var g in garageUpgradeButtons) g.LoadFromSave(s);
    }
}
