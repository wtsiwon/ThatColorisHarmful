using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Single<GameManager>
{
    public const int SCORE = 100;
    #region UI����
    [Header("UI")]
    public Slider slider;
    public TextMeshProUGUI scoreText;
    public GameObject board;
    public GameObject board2;
    public int hp;
    #endregion
    #region �ð� ����
    [Header("�ð� ����")]
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

    [Header("�Ҹ��� ����")]
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
        #region Hp UI ���� �ִ��ϱ�
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

        if (Input.GetKeyDown(KeyCode.LeftArrow))//�̰� �ΰ��ε�
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
    /// �������� ������Ʈ�� ������ �ѱ�� �Լ�
    /// </summary>
    /// <param name="obj">�������� ������Ʈ</param>
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
    /// �������� ������Ʈ �νô� �Լ�
    /// </summary>
    /// <param name="obj">Collider�� ���� ��</param>
    public void Break(Obj obj)
    { 
        Vector2 dir = new Vector2();
        #region �νǶ� �ִϸ��̼�
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
            Destroy(obj);
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
    /// �νô� �ִϸ��̼�
    /// </summary>
    public void Change()
    {
        playerMotion.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
    }
}