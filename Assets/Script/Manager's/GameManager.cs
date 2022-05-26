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

    [Header("�ð� ����")]
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

    [Header("�Ҹ��� ����")]
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
    public void RandomSpawnObj(int index)
    {
        AbsObjFactory factory = new ObjFactory();
        factory.CreateObj((EColor)Random.Range(0,2),index, pos.transform.position);
    }
    /// <summary>
    /// �������� ������Ʈ�� ������ �ѱ�� �Լ�
    /// </summary>
    /// <param name="obj">�������� ������Ʈ</param>
    public void Next(GameObject obj)
    {
        
        var getObj = obj.GetComponent<Obj>();
        Vector2 dir = new Vector2();
        int speed = 0;

        if (obj == null || getObj.isCan == false) return;

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
    /// <summary>
    /// �������� ������Ʈ �νô� �Լ�
    /// </summary>
    /// <param name="obj"></param>
    public void Break(GameObject obj)
    {
        var getObj = obj.GetComponent<Obj>();
        Vector2 dir = new Vector2();

        playerMotion.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        Invoke(nameof(Change), 0.2f);

        if (obj == null || getObj.isCan == false) return;
        if (getObj.eColor == EColor.Green && getObj.isCan)
        {
            CameraManager.Instance.Shake();
            Destroy(obj);
            Score += SCORE;
        }
        else if (getObj.eColor == EColor.Other && getObj.isCan)
        {
            dir = Vector2.left;
            getObj.isCan = false;
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