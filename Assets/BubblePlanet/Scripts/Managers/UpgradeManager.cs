using System;
using Gameplay;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
namespace Managers
{
	public class UpgradeManager : Singleton<UpgradeManager>
{
    [Header("Upgrade Per Click")]
    [SerializeField] private int currentLevelPerClick = 0;
    [SerializeField] private int[] upgradePerClickCosts;
    [SerializeField] private int[] upgradePerClickValues;
    [SerializeField] private Outline upgradePerClickOutline;
    [SerializeField] private TextMeshProUGUI upgradePerClickCostsText;
    [SerializeField] private TextMeshProUGUI upgradePerClickValuesText;

    private float _lastTime = 0f;

    protected override void Awake()
    {
        base.Awake();
        currentLevelPerClick = Mathf.Clamp(currentLevelPerClick, 0, upgradePerClickCosts.Length - 1);
    }

    private void Update()
    {
        if (Time.time - _lastTime <= 0.1f) return;
        _lastTime = Time.time;
        UpdateUI();
    }

    public void UpgradeLevelPerClick()
    {
        if (currentLevelPerClick >= upgradePerClickCosts.Length)
        {
            Debug.LogWarning("Already at max upgrade level.");
            return;
        }
        
        int score = DataManager.Instance.GetScore();
        int cost = upgradePerClickCosts[currentLevelPerClick];
        int value = upgradePerClickValues[currentLevelPerClick];
        
        if (score > cost)
        {
            DataManager.Instance.DecreaseScore(cost);
            DataManager.Instance.IncreaseAdditionalScorePerClick(value);
            currentLevelPerClick++;
            AudioManager.Instance.PlaySound(SoundName.ShopConfirm);
            UpdateUI();
        }
        else
        {
            AudioManager.Instance.PlaySound(SoundName.ButtonSelect);
        }
    }

    private void UpdateUI()
    {
        bool isMaxLevel = currentLevelPerClick >= upgradePerClickCosts.Length;
        if (isMaxLevel)
        {
            upgradePerClickValuesText.text = "Max";
            upgradePerClickValuesText.color = Color.gray;
            upgradePerClickCostsText.text = string.Empty;
            upgradePerClickOutline.effectColor = Color.gray;
        }
        else
        {
            int score = DataManager.Instance.GetScore();
            int cost = upgradePerClickCosts[currentLevelPerClick];
            int value = upgradePerClickValues[currentLevelPerClick];
            
            upgradePerClickCostsText.text = $"-{cost/10f}%";
            upgradePerClickValuesText.text = $"+{value}PC";
            upgradePerClickOutline.effectColor = score > cost ? Color.green : Color.red;
        }
    }
}
}