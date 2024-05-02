using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class HUD : MonoBehaviour
{
    public enum InfoType {Exp, Level, Kill, Time, Health}
    public InfoType type;
    private GameObject etq;

    Text myText;
    Slider mySlider;
    TMP_Text myTMP;
    private void Awake()
    {
        myTMP = GetComponent<TMP_Text>();
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;
            case InfoType.Kill:
                myTMP.text = "<sprite=0>" + GameManager.instance.kill;
                break;
            case InfoType.Time:
                float floorTime = GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(floorTime / 60);
                int sec = Mathf.FloorToInt(floorTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec );
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxhealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
