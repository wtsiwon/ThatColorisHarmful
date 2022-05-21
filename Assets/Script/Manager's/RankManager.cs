using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankManager : MonoBehaviour
{
    public static RankManager Instance { get; private set; }

    public Text ScoreText;
    private int textScore;
    private void Awake()
    {
        Instance = this;
        textScore = PlayerPrefs.GetInt("Score");
        ScoreText.text = string.Format("{0}", textScore.ToString("#,##0"));

    }
}
