using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;
    [SerializeField] private GameObject player;
    [SerializeField] private int playerLife = 5;
    private GameObject loseGameOver, winGameOver;
    public TextMeshProUGUI scoreText;
    [SerializeField]
    int _score = 0;

    public void setPlayer(GameObject player) { this.player = player; }
    public GameObject getPlayer() { return player; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (gameManager == null)
        {
            gameManager = this;
            Debug.Log(gameManager + "  gamemanager");
        }
        else
            Destroy(this.gameObject);
        if (player != null)
        {
            player.GetComponent<playerHealth>().maxHealth = playerLife;
        }

    }

    public int getMaxLife()
    {
        return playerLife;
    }


    public void Score() { _score++; updateScore(); }
    public void Score(int score) { _score = _score + score; updateScore(); }
    public int GetScore() { return _score; }

    public void checkGame(int hp)
    {
        if (hp <= 0) GameOver();
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // winGameOver.SetActive(false);
        // loseGameOver.SetActive(true);
    }

    public void winGame()
    {
        loseGameOver.SetActive(true);
        winGameOver.SetActive(false);
    }

    void checkRanking()
    {
        GetInformation();
    }

    void updateScore()
    {
        scoreText.text = "Score: " + _score;
    }

    private void OnValidate()
    {
        updateScore();
    }

    private void OnEnable()
    {
        enemyHealth.OnEnemyDie += Score;
    }

    private void GetInformation()
    {
        StartCoroutine(cGetInformation());
    }
    IEnumerator cGetInformation()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://roominvaders.ddns.net/extract.php");
        yield return www.SendWebRequest();

        string sRanking = www.downloadHandler.text;
        Player[] player = JsonHelper.FromJson<Player>(sRanking);
        foreach (Player p in player)
        {
            Debug.Log("User: " + p.name + " score: " + p.score);
        }
    }
    private void sendInformation()
    {
        StartCoroutine(cSendInformation());
    }
    IEnumerator cSendInformation()
    {
        WWWForm form = new WWWForm();
        form.AddField("playerName", scoreText.text);
        form.AddField("score", _score);
        UnityWebRequest www = UnityWebRequest.Post("http://roominvaders.ddns.net/insert.php", form);
        yield return www.SendWebRequest();
    }

    private class Player
    {
        public string name;
        public int score;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.HighScores;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] HighScores;
        }
    }

}
