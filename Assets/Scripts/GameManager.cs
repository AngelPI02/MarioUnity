using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public Text Lives;
    public Text Counter;
    public Text Puntos;
    public Text Coins;
    private float time = 400;
    private int points;
    private int livesMario;
    internal void AddPoints(int v)
    {
        points += v;
    }

    private void Awake()
    {

        if (Instance == null){
            DontDestroyOnLoad(gameObject); 
            Instance = this;
        }
        else if (Instance != this) 
        {
            Destroy(gameObject); 
        }
    }
    // Start is called before the first frame update

    void Start()
    {
        livesMario = GameObject.FindObjectOfType<MarioController>().lives;

        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        Counter.text = time.ToString("F0"); 
        Lives.text = livesMario.ToString();
        Puntos.text = points.ToString();
        Coins.text = "1";

        
    }
}
