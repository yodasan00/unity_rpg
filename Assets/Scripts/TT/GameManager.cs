using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManage : MonoBehaviour
{
    public int playerScore = 0;
    public int opponentScore = 0;
    public int winningScore = 10;

    public TMP_Text playerScoreText;
    public TMP_Text opponentScoreText;
    public GameObject winPanel;
    public TMP_Text winMessage;

    private BallController ball;

    void Start()
    {
        ball = FindFirstObjectByType<BallController>();
        UpdateScoreUI();
        winPanel.SetActive(false);
    }

    public void PlayerScored()
    {
        playerScore++;
        UpdateScoreUI();
        CheckWinner();
    }

    public void OpponentScored()
    {
        opponentScore++;
        UpdateScoreUI();
        CheckWinner();
    }

    private void UpdateScoreUI()
    {
        string p = playerScore.ToString();
        string ai = opponentScore.ToString();
        playerScoreText.text = $"Player: {p}";
        opponentScoreText.text = $"Opponent: {ai}";
    }

    private void CheckWinner()
    {
        if (playerScore >= winningScore){
            MusicManager.Instance.PlayUISound("success");
            EndGame("You Win!");
       

        }
        else if (opponentScore >= winningScore){
            MusicManager.Instance.PlayUISound("failure");
            EndGame("You Lose!");
        }
    }

    private void EndGame(string message)
    {
        winMessage.text = message;
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        QuestManager.Instance.CompleteQuest(QuestType.PlayTableTennis);
    }
}
