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
    public GameObject board;

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

        SpawnObj();
    }
    private void Update()//zz
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
                    Instantiate(board, GameObject.Find("Canvas").transform);
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
        while (Time.time - time < Limittime && OK)
        {
            slider.value = Time.time - time;
        }
        gameObject.GetComponent<Object>().dspd = 10;
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
    public int HighScore
    {
        get => highScore;
        set
        {
            highScore = value;
            if (highScore < Score)
            {
                highScore = Score;
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
            StartCoroutine(LimitTime(limittime, a));
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
        if (obj == null) return;
        if (obj.GetComponent<Object>().eColor == EColor.Green)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector3.down * obj.GetComponent<Object>().dspd;
            //Instantiate(desParticle, obj.transform);
            //StartCoroutine(Destroy(desParticle));
        }
        else if (obj.GetComponent<Object>().eColor == EColor.Other)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector3.left * (obj.GetComponent<Object>().dspd + 50);
            Score += SCORE;
        }

    }
    public void Break(GameObject obj)
    {
        playerMotion.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        Invoke("Change", 0.2f);
        if (obj == null) return;
        if (obj.GetComponent<Object>().eColor == EColor.Green)
        {
            Destroy(obj);
            Score += SCORE;
            //Instantiate(desParticle,obj.transform);
            //StartCoroutine(Destroy(desParticle));
        }
        else if (obj.GetComponent<Object>().eColor == EColor.Other)
        {
            //Instantiate(desParticle, obj.transform);
            //StartCoroutine(Destroy(desParticle));
            obj.GetComponent<Rigidbody2D>().velocity = Vector3.down * obj.GetComponent<Object>().dspd;
        }
    }
    public void Change()
    {
        playerMotion.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
    }
    private IEnumerator Destroy1(GameObject obj)
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(obj);
    }
    
}
