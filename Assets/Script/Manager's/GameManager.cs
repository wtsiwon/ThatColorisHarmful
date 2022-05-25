using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Single<GameManager>
{
    public const int SCORE = 100;

    [Header("UI")]
    public Slider slider;
    public TextMeshProUGUI scoreText;
    public GameObject board;
    public GameObject board2;

    [Header("시간 변수")]
    public float limittime;
    public float time = 100;
    public float minusTime;
    public int startTime = 3;
    public int hp;
    public float spawnPersent;
    private int activeCount = 1;

    [Space(30f)]
    [Header("player")]
    public GameObject playerMotion;
    public GameObject player;
    public GameObject desParticle;

    [Header("불리언 변수")]
    private int score;
    public int highScore;
    public bool isEnd;


    [Header("Object")]
    [Space(20f)]
    public Transform pos;
    public List<GameObject> greenobj = new List<GameObject>();
    public List<GameObject> otherobj = new List<GameObject>();
    public List<GameObject> hpobj = new List<GameObject>(3);

    private void OnEnable()
    {
        activeCount = 1;
        highScore = PlayerPrefs.GetInt("Score");
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
                hpobj[0].gameObject.SetActive(false);
                hpobj[1].gameObject.SetActive(false);
                hpobj[2].gameObject.SetActive(false);
                if (activeCount == 1)
                {
                    AudioManager.Instance.AudioList[1].Stop();
                    AudioManager.Instance.EffAudioList[5].Play();
                    Instantiate(board2, GameObject.Find("Canvas").transform);
                    Instantiate(board, GameObject.Find("Canvas").transform);
                    if (highScore < Score)
                    {
                        AudioManager.Instance.EffAudioList[6].Play();
                        highScore = Score;
                        PlayerPrefs.SetInt("Score", highScore);
                    }
                    activeCount--;
                }
                break;

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))//이거 두개인데
        {
            if (SlowZone.Instance.instanceobj == null) return;
            Next(SlowZone.Instance.instanceobj);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (SlowZone.Instance.instanceobj == null) return;
            Break(SlowZone.Instance.instanceobj);
        }
    }
    public IEnumerator LimitTime(float Limittime, GameObject gameObject)
    {
        float time = Time.time;
        while (Time.time - time < Limittime)
        {
            slider.value = Time.time - time;
        }
        gameObject.GetComponent<Obj>().dspd = 10;
        yield return null;
    }
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }

    public void SpawnObj()
    {
        Debug.Log("안이");
        int rnd = Random.Range(0, 10);
        if (rnd > spawnPersent)
        {
            int randomIndex = Random.Range(0, greenobj.Count);
            Instantiate(greenobj[randomIndex], pos);

        }
        else
        {
            int randomIndex = Random.Range(0, otherobj.Count);
            Instantiate(otherobj[randomIndex], pos);
        }
    }
    /// <summary>
    /// 떨어지는 오브젝트를 옆으로 넘기는 함수
    /// </summary>
    /// <param name="obj">떨어지는 오브젝트</param>
    public void Next(GameObject obj)
    {
        var getObj = obj.GetComponent<Obj>();
        Vector2 dir = new Vector2();
        int speed = 0;

        if (obj == null && getObj.isCan == false) return;

        if (getObj.eColor == EColor.Green)
        {
            dir = Vector2.down;
            speed = 0;
        }
        else if (getObj.eColor == EColor.Other)
        {
            dir = Vector2.left;
            speed = 50;
            Score += SCORE;
        }

        obj.GetComponent<Rigidbody2D>().velocity = dir * (getObj.dspd + speed);
    }
    public void Break(GameObject obj)
    {
        var getObj = obj.GetComponent<Obj>();
        Vector2 dir = new Vector2();

        playerMotion.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        Invoke(nameof(Change), 0.2f);

        if (obj == null && getObj.isCan == false) return;
        if (getObj.eColor == EColor.Green)
        {
            CameraManager.Instance.Shake();
            Destroy(obj);
            Score += SCORE;
        }
        else if (getObj.eColor == EColor.Other)
        {
            dir = Vector2.left;
            getObj.isCan = false;
        }
        

    }
    public void Change()
    {
        playerMotion.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
    }
}