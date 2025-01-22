using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusArea : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerArea, enemyArea, castArea;
    public TMP_Text playerHealthText, enemyHealthText;
    public RectTransform playerHealthPanel, playerChargePanel, enemyHealthPanel, chargeCooldownPanel, attackCooldownPanel;

    public bool visible = true;
    private bool castVisible = false;
    public CanvasGroup castPanel;
    public TMP_Text castText;

    public CharacterInfo playerInfo, enemyInfo;

    CanvasGroup statusPanel;

    void Awake()
    {
        statusPanel = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        SetPlayerHealth(playerInfo.health, (float)(playerInfo.health/playerInfo.healthMax));
        SetEnemyHealth(enemyInfo.health, (float)(enemyInfo.health / enemyInfo.healthMax));

        if (castVisible)
        {
            if (castPanel.alpha < 1) castPanel.alpha += 5 * Time.deltaTime;
        }
        else if (castPanel.alpha > 0) castPanel.alpha -= 5 * Time.deltaTime;

        if (visible)
        {
            if (statusPanel.alpha < 1) statusPanel.alpha += 5 * Time.deltaTime;
        }
        else if (statusPanel.alpha > 0) statusPanel.alpha -= 5 * Time.deltaTime;
    }

    public void SetOpacity(float a)
    {
        statusPanel.alpha = a;
    }

    public void SetPlayerHealth(float amt, float p)
    {
        playerHealthText.text = amt.ToString();

        if (playerHealthPanel.sizeDelta.x > 282 * p)
        {
            playerHealthPanel.sizeDelta = new Vector2(playerHealthPanel.sizeDelta.x - 100 * Time.deltaTime, 16);
        }
        else playerHealthPanel.sizeDelta = new Vector2(282 * p, 16);
    }

    public void SetChageAmount(float p)
    {
        playerChargePanel.sizeDelta = new Vector2(282 * p, 10);
    }

    public void SetEnemyHealth(float amt, float p)
    {
        enemyHealthText.text = amt.ToString();

        if (enemyHealthPanel.sizeDelta.x > 282 * p)
        {
            enemyHealthPanel.sizeDelta = new Vector2(enemyHealthPanel.sizeDelta.x - 100 * Time.deltaTime, 16);
        }
        else enemyHealthPanel.sizeDelta = new Vector2(282 * p, 16);
    }

    public void FadeInCast(string text)
    {
        castText.text = text;
        castVisible = true;
    }

    public void FadeOutCast()
    {
        castVisible = false;
    }

    public void SetChargeCooldown(float p)
    {
        if (p >= 1) p = 1;
        chargeCooldownPanel.sizeDelta = new Vector2(131 - (131 * p), 0);
    }

    public void SetAttackCooldown(float p)
    {
        if (p >= 1) p = 1;
        attackCooldownPanel.sizeDelta = new Vector2(131 - (131* p), 0);
    }
}
