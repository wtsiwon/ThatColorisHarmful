using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ResultWindow : MonoBehaviour
{
    [SerializeField] private Text text;
    private void Start()
    {
        gameObject.GetComponent<Transform>().DOScale(new Vector2(1, 1), 1.5f);
        text.text = "Game Over...";
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
