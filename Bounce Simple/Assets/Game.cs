using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public Text scoreText;
    private int score;
    private int bonus = 1;
    private int workersCount,workersBonus = 1;

    [Header("Shop")]
    public Text upgradeText;
    public int[] shopCosts;
    public int[] shopBonuses;
    public Text[] shopBtnsText;
    public Button[] shopButns;
    public float[] bonusTime;
    public GameObject shopPan;

    //------------------------------------------------

    void Start()
    {
        StartCoroutine(BonusPerSec());
    }

    void Update()
    {
        scoreText.text = score + "$";
    }

    public void ShopPan_openOrClose() // Open or Close shop panel
    {
        shopPan.SetActive(!shopPan.activeSelf);
    }

    public void shopBtn_addBonus(int index) // Shop buttons
    {
        if (score >= shopCosts[index])
        {
            bonus += shopBonuses[index];
            score -= shopCosts[index];
            shopCosts[index] *= 2;
            upgradeText.text = "BUY UPGRADE\n" + shopCosts[index] + "$";
        } else
        {
            Debug.Log("Enough money!");
        }
    }

    public void HireWorker(int index) 
    {
        if (score >= shopCosts[index])
        {
            workersCount++;
            score -= shopCosts[index];
        }
    }

    public void startBonusTimer(int index) // Bous time for worker
    {
        int cost = 2 * workersCount;

        if (score >= cost)
        {
            shopBtnsText[2].text = "BUY BEER FOR WORKERS\n" + cost + "$";
            StartCoroutine(bonusTimer(bonusTime[index], index));
        }
    }

    IEnumerator bonusTimer(float time,int index)
    {
        shopButns[index].interactable = false;

        if (index == 0 && workersCount > 0)
        {
            workersBonus *= 2;
            yield return new WaitForSeconds(time);
            workersBonus /= 2;
        }
        shopButns[index].interactable = true;
    }

    IEnumerator BonusPerSec()
    {
        while(true)
        {
            score += (workersCount*workersBonus);
            yield return new WaitForSeconds(1);
        }
    }

	public void OnClick()
    {
        score += bonus;
    }
}
