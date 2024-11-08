using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    public Slider slider;
    public GameObject chef;

    // Start is called before the first frame update
    void Awake()
    {
        slider.maxValue = chef.GetComponent<PlayerControlScript>().hp;
        slider.value = chef.GetComponent<PlayerControlScript>().hp;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = chef.GetComponent<PlayerControlScript>().hp;
    }
}
