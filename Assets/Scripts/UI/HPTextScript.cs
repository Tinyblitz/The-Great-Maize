using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPTextScript : MonoBehaviour
{
    public TMP_Text textMesh;
    public GameObject chef;
    private int maxHealth;

    void Awake()
    {
        maxHealth= chef.GetComponent<PlayerControlScript>().hp;
        textMesh.SetText(chef.GetComponent<PlayerControlScript>().hp + "/" + maxHealth);
    }

    void LateUpdate()
    {
        textMesh.SetText(chef.GetComponent<PlayerControlScript>().hp + "/" + maxHealth);
    }
}
