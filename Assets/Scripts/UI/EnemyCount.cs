using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyCount : MonoBehaviour
{
    public GameObject counter;
    public TMP_Text textMesh;
    public bool nextSceneFlag;

    private int enemiesDefeated;
    private int enemiesToDefeat;

    void Start() {
        textMesh.SetText("Enemies: " + enemiesToDefeat);
        nextSceneFlag = false;
        enemiesToDefeat = counter.GetComponent<EnemyCounter>().enemiesToDefeat;

        counter = GameObject.Find("EnemyCounter");
    }

    void Update() {
        enemiesDefeated = counter.GetComponent<EnemyCounter>().enemiesDefeated;
        enemiesToDefeat = counter.GetComponent<EnemyCounter>().enemiesToDefeat;
        textMesh.SetText("Enemies: " + (enemiesToDefeat - enemiesDefeated));
        if (enemiesToDefeat - enemiesDefeated <= 0) {
            nextSceneFlag = true;
        }
    }
}
