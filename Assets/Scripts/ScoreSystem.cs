using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  private void Start()
    {
        UpdateScoreText();
    }

    public void IncreaseScore(int points)
    {
        score += points;
        UpdateScoreText();

    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
