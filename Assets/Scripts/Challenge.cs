using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Challenge : MonoBehaviour
{
    public Text tittleTimer;
    public Text timerText;
    public Text resultText;
    public Button BackButton;
    public int time;

    private int _time;
    private int highscore;

    void Start()
    {
        _time = time;
        highscore = PlayerPrefs.GetInt("_highscore");
        StartCoroutine(StartChallenge());
    }

    IEnumerator StartChallenge()
    {
        while(time >= 0)
        {
            timerText.text = $"{time / 60:00} : {time % 60:00}";
            time--;
            if (time > _time * 0.25)
                timerText.color = Color.black;
            else
            {
                if (time % 2 == 0)
                    timerText.color = Color.red;
                else
                    timerText.color = Color.black;
            }
            yield return new WaitForSeconds(1f);
        }
        ShowResult();
    }

    void ShowResult()
    {
        //int gold = (int)GameManager.Instance.TotalGold;
        /*if (highscore < gold)
            PlayerPrefs.SetInt("_highscore", gold);*/

        /*if (*//*GameManager.Instance.TotalGold*//* 1 >= 500000)
        {
            resultText.text = $"<b>KAMU BERHASIL!</b>\nSKOR TERTINGGI : {PlayerPrefs.GetInt("_highscore")}\nSKOR KAMU : {gold}";
            AchievementController.Instance.UnlockAchievement(AchievementType.Challenge, "ultimate");   
        }
        else
        {
            resultText.text = $"<b>KAMU GAGAL!</b>\nSKOR TERTINGGI : {PlayerPrefs.GetInt("_highscore")}\nSKOR KAMU : {gold}";
        }*/

        tittleTimer.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        resultText.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
