using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Ranking : MonoBehaviour
{
    [SerializeField]
    private GameObject container;
    [SerializeField] private List<Transform> list = new List<Transform>();

    void Start()
    {
        foreach (Transform item in container.transform)
        {
            list.Add(item);
        }
        StartCoroutine(cGetInformation());
    }

    IEnumerator cGetInformation()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://roominvaders.ddns.net/extract.php");
        yield return www.SendWebRequest();

        string sRanking = www.downloadHandler.text;
        Player[] player = JsonHelper.FromJson<Player>(sRanking);
        //foreach (Player p in player)
        //{
        //    Debug.Log("User: " + p.name + " score: " + p.score);

        //}
        for (int i = 1; i < list.Count && i < player.Length - 1; i++)
        {
            Debug.Log(i-1 + ": " + list.Count + " " + player.Length);
            list[i-1].GetComponent<TextMeshProUGUI>().text = player[i-1].nombre + "  " + player[i-1].score;
        }
    }

    [Serializable]
    private class Player
    {
        public string nombre;
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
