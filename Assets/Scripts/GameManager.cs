using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;
    private const int DAILY_REWARD_AMOUNT = 15; // Amount of coins rewarded
    private const string LAST_REWARD_TIME_KEY = "LastRewardTime"; // PlayerPrefs key for saving last reward time

    public static GameManager Instance { set; get; }

    public bool IsDead { set; get; }

    // Keep this false until a material is chosen
    public bool isGameStarted = false;
    private PlayerMotor motor;

    // UI and UI fields
    public Animator gameCanvasAnim, menuAnim, coinPop;
    public Text scoreText, coinText, modifierText, highscoreText, coinTextMain;
    public Button dailyRewardButton; // The daily reward button
    private float score, modifierScore;
    private int lastScore;
    private int coinScore;

    // Death Menu
    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    //SFX
    public AudioClip coinSound;
    private AudioSource playerAudio;
    public AudioClip deathSound;
    public GameObject musicGameObject;

    private void Awake()
    {
        Instance = this;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        modifierScore = 1;
        highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();

        // Load coinScore from PlayerPrefs
        coinScore = PlayerPrefs.GetInt("CoinScore", 0); // Default to 0 if no saved score
        coinText.text = coinScore.ToString("0"); // Update UI with the saved coinScore
        coinTextMain.text = coinScore.ToString("0");

        // Check if daily reward button should be enabled
        CheckDailyRewardAvailability();
    }

    private void Update()
    {
        // Only start the game after material is selected
        if (MobileInputs.Instance.Tap && !isGameStarted)
        {
            return; // Don't start the game until material is selected
        }

        if (isGameStarted && !IsDead)
        {
            // Increase score
            score += (Time.deltaTime * modifierScore);
            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
        }
        playerAudio = GetComponent<AudioSource>();
    }

    // Method to trigger game start after material selection
    public void StartGameAfterMaterialSelection()
    {
        isGameStarted = true;
        motor.StartRunning();

        FindObjectOfType<GlacierSpawner>().IsScrolling = true;
        FindObjectOfType<CameraMotor>().IsMoving = true;

        gameCanvasAnim.SetTrigger("Show");
        menuAnim.SetTrigger("Hide");
    }

    public void GetCoin()
    {
        coinPop.SetTrigger("Collect");
        coinScore++; // Increase the coin score
        score += COIN_SCORE_AMOUNT;
        coinText.text = coinScore.ToString("0");
        coinTextMain.text = coinScore.ToString("0");
        scoreText.text = score.ToString("0");

        // Save updated coinScore to PlayerPrefs
        PlayerPrefs.SetInt("CoinScore", coinScore);

        playerAudio.PlayOneShot(coinSound);
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OnDeath()
    {
        IsDead = true;
        playerAudio.PlayOneShot(deathSound);
        musicGameObject.SetActive(false);
        deadCoinText.text = score.ToString("0");
        deadScoreText.text = coinScore.ToString("0");

        deathMenuAnim.SetTrigger("Dead");

        FindObjectOfType<GlacierSpawner>().IsScrolling = false;

        gameCanvasAnim.SetTrigger("Hide");

        // Check if this is a highscore
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            float s = score;
            if (s % 1 == 0)
                s++;

            PlayerPrefs.SetInt("Highscore", (int)s);
        }
    }

    // Method to check if the daily reward can be claimed
    private void CheckDailyRewardAvailability()
    {
        string lastRewardTimeString = PlayerPrefs.GetString(LAST_REWARD_TIME_KEY, string.Empty);

        if (!string.IsNullOrEmpty(lastRewardTimeString))
        {
            DateTime lastRewardTime = DateTime.Parse(lastRewardTimeString);
            TimeSpan timeSinceLastReward = DateTime.Now - lastRewardTime;

            if (timeSinceLastReward.TotalHours >= 24)
            {
                // 24 hours have passed, enable the button
                dailyRewardButton.interactable = true;
            }
            else
            {
                // Less than 24 hours have passed, disable the button
                dailyRewardButton.interactable = false;
            }
        }
        else
        {
            // No reward has been claimed yet, enable the button
            dailyRewardButton.interactable = true;
        }
    }

    // Method to claim the daily reward
    public void ClaimDailyReward()
    {
        // Add 15 coins to the coin score
        coinScore += DAILY_REWARD_AMOUNT;
        coinText.text = coinScore.ToString("0");
        coinTextMain.text = coinScore.ToString("0");

        // Save the updated coin score and the current time
        PlayerPrefs.SetInt("CoinScore", coinScore);
        PlayerPrefs.SetString(LAST_REWARD_TIME_KEY, DateTime.Now.ToString());

        // Disable the button until 24 hours have passed
        dailyRewardButton.interactable = false;
    }
}
