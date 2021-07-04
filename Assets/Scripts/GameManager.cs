
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { private set; get; }
    public event UnityAction onGameOver = delegate { };
    public event UnityAction onWin = delegate { };
    [SerializeField] public Vector3 originPos;
    private GameObject ball;
    private void Awake()
    {
        #region µ¥Àý
        if (instance == null)
        {
            instance = this;
        }
        #endregion
        randomNum = new List<int>();
        ball = FindObjectOfType<Ball>().gameObject;
    }

    [SerializeField] Material ballMaterial;
    [SerializeField] GameObject[] allWheels;
    [SerializeField] Material deathMaterial;
    List<int> randomNum;
    private void Start()
    {
        originPos = ball.transform.position;
        Initialize();
    }
    private void Initialize()
    {
        foreach (var wheel in allWheels)
        {
            RandomlySpawnHelix(wheel);
        }
    }
    private void RandomlySpawnHelix(GameObject wheel)
    {
        var openCount = Random.Range(3, 4);
        var deathCount = Random.Range(1, 2);
        while (openCount>0)
        {
            var index = Random.Range(1, wheel.transform.childCount-1);
            if (!randomNum.Contains(index))
            {
                wheel.transform.GetChild(index).gameObject.SetActive(false);
                randomNum.Add(index);
                openCount--;
            }
        }
        while (deathCount>0)
        {
            var index = Random.Range(1, wheel.transform.childCount - 1);
            if (!randomNum.Contains(index))
            {
                wheel.transform.GetChild(index).tag = "DeathHelix";
                wheel.transform.GetChild(index).GetComponent<Renderer>().material = deathMaterial;
                randomNum.Add(index);
                deathCount--;
            }
        }
        randomNum.Clear();
    }

    public void OnGameOver()
    {
        onGameOver.Invoke();
        ball.transform.position = originPos;
    }
    public void OnWin()
    {
        onWin.Invoke();
        Debug.Log("YOU WIN!!!");
    }
}
