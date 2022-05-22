using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ResultWindow : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Start()
    {
        text.text = "Game Over\nScore : " + string.Format("{0}", GameManager.Instance.Score.ToString("#,##0"));
    }

    public void ReStart()
    {
        SceneManager.LoadScene("InGame");
    }
    public void GoTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
