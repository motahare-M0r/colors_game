using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ColorItem
{
    public string Label;
    public Color ColorValue;
}

public class Game_Maneger : MonoBehaviour
{
    public List<ColorItem> AvailableColors;
    public TextMeshProUGUI ColorNameText;

    public Image[] ColorButtons;

    private int correctColorIndex;
    private int currentScore;
    private float remainingTime;
    private bool isGameActive;
    private int chosenColorIndex;

    [Header("UI Elements")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimerText;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    [Header("Audio")]
    public AudioSource CorrectSound;
    public AudioSource IncorrectSound;

    [Header("Settings")]
    public float initialTime = 5f;
    public float delayBeforeStart = 3f;

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        currentScore = 0;
        StartCoroutine(BeginGameAfterDelay(delayBeforeStart));
    }

    private IEnumerator BeginGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetupColorButtons();
        isGameActive = true;
        remainingTime = initialTime;
    }

    private void SetupColorButtons()
    {
        List<int> chosenIndices = new List<int>();

        for (int i = 0; i < ColorButtons.Length; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, AvailableColors.Count);
            } while (chosenIndices.Contains(randomIndex));

            chosenIndices.Add(randomIndex);

            ColorButtons[i].color = AvailableColors[randomIndex].ColorValue;
            ColorButtons[i].GetComponent<Breik_Controller>().ColorName = AvailableColors[randomIndex].Label;
        }

        correctColorIndex = UnityEngine.Random.Range(0, chosenIndices.Count);
        ColorNameText.text = AvailableColors[chosenIndices[correctColorIndex]].Label;
        chosenColorIndex = chosenIndices[correctColorIndex];
    }

    void Update()
    {
        if (isGameActive)
        {
           
            remainingTime -= Time.deltaTime;
            TimerText.text = $"{remainingTime:F2}";

            if (remainingTime <= 0)
            {
                HandleIncorrectChoice();
            }
        }
    }

    public void VerifyColorChoice(string selectedColor)
    {
        if (selectedColor == AvailableColors[chosenColorIndex].Label)
        {
            HandleCorrectChoice();
        }
        else
        {
            HandleIncorrectChoice();
        }
    }

    private void HandleCorrectChoice()
    {
        CorrectSound.Play();
        UpdateScore(5);
        RestartRound();
    }

    private void HandleIncorrectChoice()
    {
        IncorrectSound.Play();
        UpdateScore(-5);
        RestartRound();
    }

  
    private void UpdateScore(int amount)
    {
        currentScore += amount;
        ScoreText.text = $"{currentScore}";

        if (currentScore >= 100)
        {
            WinScreen.SetActive(true);
            isGameActive = false;
        }
        else if (currentScore <= 0)
        {
            currentScore = 0;
            LoseScreen.SetActive(true);
            isGameActive = false;
        }
    }

    private void RestartRound()
    {
        if (isGameActive)
        {
            remainingTime = initialTime;
            SetupColorButtons();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
