using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Add to an empty GameObject named GameManager.
// Optional: connect TMP labels in the Inspector.
public class RaceGameManager : MonoBehaviour
{
    public TMP_Text coinsLabel;
    public TMP_Text learningPointsLabel;
    public TMP_Text statusLabel;

    public int coins;
    public int learningPoints;
    public int raceWins;

    private const string CoinsKey = "JaseDriveCoins";
    private const string PointsKey = "JaseDriveLearningPoints";
    private const string WinsKey = "JaseDriveWins";

    private void Start()
    {
        coins = PlayerPrefs.GetInt(CoinsKey, 0);
        learningPoints = PlayerPrefs.GetInt(PointsKey, 0);
        raceWins = PlayerPrefs.GetInt(WinsKey, 0);
        RefreshLabels();
        SetStatus("JASE GES DRIVE & LEARN — Ready to race!");
    }

    public void CollectCoin(int amount = 5)
    {
        coins += Mathf.Max(0, amount);
        Save();
        RefreshLabels();
        SetStatus("+" + amount + " coins!");
    }

    public void AnswerCorrectly(int coinReward = 10, int pointReward = 10)
    {
        coins += Mathf.Max(0, coinReward);
        learningPoints += Mathf.Max(0, pointReward);
        Save();
        RefreshLabels();
        SetStatus("Correct answer! +" + coinReward + " coins, +" + pointReward + " learning points.");
    }

    public void FinishRace()
    {
        raceWins++;
        coins += 50;
        Save();
        RefreshLabels();
        SetStatus("Race finished! +50 bonus coins. Total wins: " + raceWins);
    }

    public bool TrySpendCoins(int amount)
    {
        if (amount < 0 || coins < amount) return false;
        coins -= amount;
        Save();
        RefreshLabels();
        return true;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(CoinsKey, coins);
        PlayerPrefs.SetInt(PointsKey, learningPoints);
        PlayerPrefs.SetInt(WinsKey, raceWins);
        PlayerPrefs.Save();
    }

    private void RefreshLabels()
    {
        if (coinsLabel != null) coinsLabel.text = "Coins: " + coins;
        if (learningPointsLabel != null) learningPointsLabel.text = "Learn points: " + learningPoints;
    }

    public void SetStatus(string message)
    {
        if (statusLabel != null) statusLabel.text = message;
    }
}