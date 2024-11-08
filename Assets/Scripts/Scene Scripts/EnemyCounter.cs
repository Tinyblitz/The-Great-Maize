using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour
{
    public int enemiesDefeated;
    public int enemiesToDefeat;
    public GameObject congrats;

    public static EnemyCounter instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemiesDefeated = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesToDefeat - enemiesDefeated <= 0) {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }
    }
}
