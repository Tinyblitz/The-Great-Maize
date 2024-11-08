using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpSlider : MonoBehaviour
{
    public Slider slider;
    public GameObject boss;

    // Start is called before the first frame update
    void Awake()
    {
        slider.maxValue = boss.GetComponent<BossDeath>().health;
        slider.value = boss.GetComponent<BossDeath>().health;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null)
        {
            slider.value = boss.GetComponent<BossDeath>().health;
        }
    }
}
