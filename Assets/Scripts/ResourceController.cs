﻿using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public Button ResourceButton;
    public Image ResourceImage;
    public Text ResourceDescription;
    public Text ResourceUpgradeCost;
    public Text ResourceUnlockCost;
    public AudioClip upgradeSfx;

    private ResourceConfig _config;

    public bool IsUnlocked { get; private set; }

    private int _index;

    private int _level

    {

        set

        {

            // Menyimpan value yang di set ke _level pada Progress Data

            UserDataManager.Progress.ResourcesLevels[_index] = value;

            UserDataManager.Save();

        }



        get

        {

            // Mengecek apakah index sudah terdapat pada Progress Data

            if (!UserDataManager.HasResources(_index))

            {

                // Jika tidak maka tampilkan level 1

                return 1;

            }



            // Jika iya maka tampilkan berdasarkan Progress Data

            return UserDataManager.Progress.ResourcesLevels[_index];

        }

    }

    private void Start()
    {
        ResourceButton.onClick.AddListener(() =>
        {
            if (IsUnlocked)
            {
                UpgradeLevel();
            }
            else
            {
                UnlockResource();
            }
        });
    }

    public void SetConfig(int index, ResourceConfig config)
    {
        _index = index;
        _config = config;

        // ToString("0") berfungsi untuk membuang angka di belakang koma
        ResourceDescription.text = $"{ _config.Name } Lvl. { _level }\n+{ GetOutput().ToString("0") }";
        ResourceUnlockCost.text = $"BUKA\n{ _config.UnlockCost }";
        ResourceUpgradeCost.text = $"TINGKATKAN\n{ GetUpgradeCost() }";

        SetUnlocked(_config.UnlockCost == 0 || UserDataManager.HasResources(_index));
    }

    public double GetOutput()
    {
        return _config.Output * _level;
    }

    public double GetUpgradeCost()
    {
        return _config.UpgradeCost * _level;
    }

    public double GetUnlockCost()
    {
        return _config.UnlockCost;
    }

    public void UpgradeLevel()
    {
        double upgradeCost = GetUpgradeCost();
        if (UserDataManager.Progress.Gold < upgradeCost)
        {
            return;
        }

        GameObject sfx = GameObject.Find("Upgradesfx");
        sfx.GetComponent<AudioSource>().PlayOneShot(upgradeSfx);

        GameManager.Instance.AddGold(-upgradeCost);
        _level++;

        ResourceUpgradeCost.text = $"TINGKATKAN\n{ GetUpgradeCost() }";
        ResourceDescription.text = $"{ _config.Name } Lvl. { _level }\n+{ GetOutput().ToString("0") }";
    }

    public void UnlockResource()
    {
        double unlockCost = GetUnlockCost();
        if (UserDataManager.Progress.Gold < unlockCost)
        {
            return;
        }
        SetUnlocked(true);
        GameManager.Instance.ShowNextResource();
        AchievementController.Instance.UnlockAchievement(AchievementType.UnlockResource, _config.Name);
    }

    public void LockedResource()
    {
        SetUnlocked(false);
    }

    public void SetUnlocked(bool unlocked)
    {
        IsUnlocked = unlocked;
        if (unlocked)
        {
            // Jika resources baru di unlock dan belum ada di Progress Data, maka tambahkan data
            if (!UserDataManager.HasResources(_index))
            {
                UserDataManager.Progress.ResourcesLevels.Add(_level);
                UserDataManager.Save();
            }
        }
        ResourceImage.color = IsUnlocked ? Color.white : Color.grey;
        ResourceUnlockCost.gameObject.SetActive(!unlocked);
        ResourceUpgradeCost.gameObject.SetActive(unlocked);
    }
}