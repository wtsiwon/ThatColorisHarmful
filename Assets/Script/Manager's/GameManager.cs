using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
  
    public const int SCORE = 100;

    [Header("UI")]
    public Slider slider;
    public Text scoreText;

    [Header("시간 변수")]
    public float time = 100;
    public float minusTime;
    public int startTime = 3;
    public int hp;
    public float spawnPersent;



    [Header("불리언 변수")]
    private int score;
    private int highScore;
    public bool isEnd;
    public bool OK;

    [Header("Object")]
    [Space(20f)]
    public Transform pos;
    public List<GameObject> greenobj = new List<GameObject>();
    public List<GameObject> otherobj = new List<GameObject>();
    public List<GameObject> hpobj = new List<GameObject>(3);

    private void Start()
    {
        Instance = this;
        slider = GetComponent<Slider>();
        SpawnObj();
    }
    private void Update()
    {
        switch (hp)
        {
            case 3:
                hpobj[0].gameObject.SetActive(true);
                hpobj[1].gameObject.SetActive(true);
                hpobj[2].gameObject.SetActive(true);
                break;
            case 2:
                hpobj[0].gameObject.SetActive(true);
                hpobj[1].gameObject.SetActive(true);
                hpobj[2].gameObject.SetActive(false);
                break;
            case 1:
                hpobj[0].gameObject.SetActive(true);
                hpobj[1].gameObject.SetActive(false);
                hpobj[2].gameObject.SetActive(false);
                break;
            default:

                break;

        }


        if (OK)
        {
            time -= Time.deltaTime;
            slider.value = minusTime / time;
        }



    }
    public int Score
    {
        get => score;
        set
        {
            score = value;
            score += SCORE;
            scoreText.text = "Score: " + score.ToString();
        }
    }
    public int HighScore
    {
        get => highScore;
        set
        {
            highScore = value;
            if (highScore < score)
            {
                highScore = score;
                PlayerPrefs.SetInt("Score", highScore);
            }
        }
    }
    public void SpawnObj()
    {
        Debug.Log("안이");
        int rnd = Random.Range(0, 10);
        if (rnd > spawnPersent)
        {
            int randomIndex = Random.Range(0, greenobj.Count);
            GameObject a = Instantiate(greenobj[randomIndex], pos);
            Debug.Assert(a != null);
        }
        else
        {
            int randomIndex = Random.Range(0, otherobj.Count);
            Instantiate(otherobj[randomIndex], pos);
            Debug.Assert(otherobj[randomIndex] != null);
        }
    }
    public void Next(GameObject obj)
    {
        if (obj.GetComponent<Object>().eColor == EColor.Green)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector3.down * obj.GetComponent<Object>().dspd;
            hp -= 1;
        }
        else if (obj.GetComponent<Object>().eColor == EColor.Other)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector3.left * obj.GetComponent<Object>().dspd;
            Invoke("Destroy", 0.3f);
            score += SCORE;
        }
    }
    public void Break(GameObject obj)
    {
        if (obj.GetComponent<Object>().eColor == EColor.Green)
        {
            score += SCORE;
            Destroy(obj);
        }
        else if (obj.GetComponent<Object>().eColor == EColor.Other)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector3.down * obj.GetComponent<Object>().dspd;
            hp -= 1;
        }
    }
}
