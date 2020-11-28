using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public float MoneyEarned;    
    public float HealthPrice = 300f;
    public float FireRatePrice = 200f;
    public float maxFireRate = 10f;
    public Text moneyText;
    public Button HealthBuy;
    public Button FireRateBuy;
    public PlayerController PlayerCont;
    public Slider HealthBar;
    public Slider FireRateBar;
    
    float money = 0;
    float time = 0f;
    bool doonce0 = true;
    bool doonce1 = true;
    bool doonce2 = true;
    bool doonce3 = true;
    private void Update()
    {
        FireRateBar.value = Mathf.Lerp(FireRateBar.value, PlayerCont.FireRate, Time.deltaTime);
        if (PlayerCont.Health >= 1000 && doonce2)
        {
            HealthBuy.interactable = false;
            PlayerCont.Health = 1000;
            doonce2 = false;
        }
        else if (MoneyEarned >= HealthPrice && doonce0 && PlayerCont.Health != 1000)
        {
            HealthBuy.interactable = true;
            doonce0 = false;
            doonce2 = true;
        }
        else if (MoneyEarned < HealthPrice && !doonce0 && PlayerCont.Health != 1000)
        {
            HealthBuy.interactable = false;
            doonce0 = true;
            doonce2 = true;
        }
        if (PlayerCont.FireRate >= maxFireRate && doonce3)
        {
            FireRateBuy.interactable = false;
            PlayerCont.FireRate = maxFireRate;
            doonce3 = false;
        }
        else if (MoneyEarned >= FireRatePrice && doonce1)
        {
            FireRateBuy.interactable = true;
            doonce1 = false;
        }
        else if (MoneyEarned < FireRatePrice && !doonce1)
        {
            FireRateBuy.interactable = false;
            doonce1 = true;
        }

        time += Time.deltaTime;
        if (time > 2) time = 2;
        money = Mathf.Lerp(money, MoneyEarned, time / 2);
        if (time == 2) time = 0;
        moneyText.text = (int)money + "$";
    }

    public void BuyFireRate()
    {
        MoneyEarned -= FireRatePrice;
        PlayerCont.FireRate += 0.5f;
        doonce3 = true;
    }

    public void BuyHealth()
    {
        MoneyEarned -= HealthPrice;
        PlayerCont.Health += 100f;
        if (PlayerCont.Health > 1000f)
            PlayerCont.Health = 1000f;
        HealthBar.value = PlayerCont.Health;
        doonce2 = true;
    }
}
