using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    private bool isGameStarted = false;
    private PlayerMotor motor;

    //UI and UI fields
    //public Animator gameCanvasAnim;
    public Text scoreText, coinText, modifierText, highscoreText;
    private float score, coinScore, modifierScore;
    private int lastScore;

    private void Awake()
    {
        Instance = this;
        UpdateScore();
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        //modifierScore = 1;

        //highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
    }

    private void Update()
    {
        if (MobileInputs.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();

            //FindObjectOfType<GlacierSpawner>().IsScrolling = true;
           //FindObjectOfType<CameraMotor>().IsMoving = true;

           // gameCanvasAnim.SetTrigger("Show");
        }

        /*if (isGameStarted && !IsDead)
        {
            //Increase score
            score += (Time.deltaTime * modifierScore);
            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
        }*/
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        coinText.text = coinScore.ToString();
        modifierText.text = modifierScore.ToString();
    }
}
