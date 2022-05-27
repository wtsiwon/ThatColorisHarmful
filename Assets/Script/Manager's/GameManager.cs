using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Single<GameManager>
{
    public const int SCORE = 100;
    #region UI변수
    [Header("UI")]
    public Slider slider;
    public TextMeshProUGUI scoreText;
    public GameObject board;
    public GameObject board2;
    public int hp;
    #endregion
    #region 시간 변수
    [Header("시간 변수")]
    public float limittime;
    public float time = 100;
    public float minusTime;
    public int startTime = 3;
    public float spawnPersent;
    private int activeCount = 1;
    #endregion

    [Space(30f)]
    [Header("player")]
    public GameObject playerMotion;
    public GameObject player;
    public GameObject desParticle;
    private AbsObjFactory factory;

    public int highScore;
    private int score;

    [Header("불리언 변수")]
    public bool isEnd;

    [Space(20f)]
    [Header("Object")]
    public Transform pos;
    public List<GameObject> greenobj = new List<GameObject>();
    public List<GameObject> otherobj = new List<GameObject>();
    public List<GameObject> hpobj = new List<GameObject>(3);

    private void Start()
    {
        factory = FindObjectOfType<ObjFactory>();
        RandomSpawnObj();
    }
    private void OnEnable()
    {
        activeCount = 1;
        highScore = PlayerPrefs.GetInt("Score");
    }
    private void Update()
    {
        switch (hp)
        {
        #region Hp UI 껏다 켯다하기
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
                #endregion
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
    
    public void RandomSpawnObj()
    {
        EColor color = (EColor)Random.Range(0, 2);
        factory.CreateObj(color, pos.transform.position);
    }
    /// <summary>
    /// 떨어지는 오브젝트를 옆으로 넘기는 함수
    /// </summary>
    /// <param name="obj">떨어지는 오브젝트</param>
    public void Next(Obj obj)
    {
        Vector2 dir = new Vector2();
        int speed = 0;

        if (obj == null || obj.isCan == false) return;

        if (obj.eColor == EColor.Green)
        {
            dir = Vector2.down;
            RandomSpawnObj();
            speed = 0;
        }
        else if (obj.eColor == EColor.Other)
        {
            dir = Vector2.left;
            Score += SCORE;
            RandomSpawnObj();
            speed = 50;
        }

        obj.GetComponent<Rigidbody2D>().velocity = dir * (obj.dspd + speed);
    }
    /// <summary>
    /// 떨어지는 오브젝트 부시는 함수
    /// </summary>
    /// <param name="obj">Collider에 닿은 것</param>
    public void Break(Obj obj)
    { 
        Vector2 dir = new Vector2();
        #region 부실때 애니메이션
        playerMotion.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        Invoke(nameof(Change), 0.2f);
        #endregion
        if (obj == null || obj.isCan == false) return;
        if (obj.eColor == EColor.Green && obj.isCan)
        {
            CameraManager.Instance.Shake();
            Score += SCORE;
            obj.isCan = false;
            Destroy(obj);//이거 왜 실행이 안돼냐
            RandomSpawnObj();
        }
        else if (obj.eColor == EColor.Other && obj.isCan)
        {
            dir = Vector2.left;
            RandomSpawnObj();
            obj.isCan = false;
        }


    }
    /// <summary>
    /// 부시는 애니메이션
    /// </summary>
    public void Change()
    {
        playerMotion.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
    }
}