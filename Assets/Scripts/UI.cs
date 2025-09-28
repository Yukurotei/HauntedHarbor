using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("Screens")]
    public GameObject TitleScreen;
    public GameObject PauseScreen;
    public GameObject GameScreen;

    [Header("Player Info")]
    public TMPro.TMP_Text PlayerHPText;
    public TMPro.TMP_Text CoinAmount;
    public Slider PlayerHPBar;

    [Header("Boss Info")]
    public GameObject BossUI;
    public TMPro.TMP_Text BossText, BossHPText;
    public Slider BossHPBar;
    public BY.Sprite Boss;

    [Header("UI Variables")]
    public int coins = 0;
    public static UI INSTANCE;

    // Start is called before the first frame update
    void Start()
    {
        INSTANCE = this;
        TitleScreen.SetActive(true);
        GameScreen.SetActive(false);
        BossUI.SetActive(false);
        PlayerHPBar.maxValue = BY.Sprite.PLAYER_INSTANCE.stats.maxHealth;
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (BY.Sprite.PLAYER_INSTANCE.controller.inputActions.PlayerActionMap.enabled)
        {
            TitleScreen.SetActive(false);
            PauseScreen.SetActive(false);
            GameScreen.SetActive(true);
            Time.timeScale = 1f;
        }
        if (TitleScreen.activeSelf == false && !BY.Sprite.PLAYER_INSTANCE.controller.inputActions.PlayerActionMap.enabled)
        {
            PauseScreen.SetActive(true);
            GameScreen.SetActive(false);
            Time.timeScale = 0f;
        }
        PlayerHPBar.value = BY.Sprite.PLAYER_INSTANCE.currentHealth;
        PlayerHPText.text = BY.Sprite.PLAYER_INSTANCE.currentHealth + "/" + BY.Sprite.PLAYER_INSTANCE.stats.maxHealth;
        CoinAmount.text = coins.ToString();
        if (Boss != null)
        {
            BossHPBar.maxValue = Boss.stats.maxHealth;
            BossHPBar.value = Boss.currentHealth;
            BossHPText.text = Boss.currentHealth + "/" + Boss.stats.maxHealth;
            BossText.text = Boss.name;
        }
    }

    public void setBoss(BY.Sprite boss)
    {
        this.Boss = boss;
    }
}
