using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive; // Bổ sung biến isLive để quản lý trạng thái bật/tắt game
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Infor")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 200, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
        Resume(); // Đảm bảo game chạy bình thường khi bấm Play

        //
        uiLevelUp.Select(0);
    }


    void Update()
    {
        // Khi mở bảng Level Up (isLive = false), tạm ngưng đếm thời gian
        if (!isLive) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp() { 
        if (!isLive) return;

        exp++;

        // Sử dụng Mathf.Min để tránh lỗi out of range khi người chơi đạt level tối đa
        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0; // Tạm dừng thời gian trong game
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1; // Tiếp tục thời gian trong game
    }
}