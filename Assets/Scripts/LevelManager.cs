using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject rocketPrefab, explosionPrefab, rocketAlertPrefab, explosionAlertPrefab,
     player, extraHeartPrefab;
    private float elapsed = 0f, playerXPos;
    private float[] obstacleXPositions = { -7.35f, -5.5f, -3.7f, -1.85f, -0.03f, 1.79f, 3.65f, 5.58f, 7.47f };
    
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("in the hall");
        StartCoroutine(RocketAlertBurst());
        Time.timeScale = 1;
        InvokeRepeating("spawnHeart", 60, 19);
        Invoke("spawnHeart", 140);
    }
    public void RandomizeRocketOrExplosion()
    {
        int randomForChoosingRocketOrExplosion = Random.Range(1, 5);
        if (randomForChoosingRocketOrExplosion == 2)
            ExplosionAlert();
        else
            RocketAlert();
    }

    private int FindPlayerPosIndex()
    {
        int i = 0;
        while (i < obstacleXPositions.Length)
        {
            if (Mathf.Abs(obstacleXPositions[i] - playerXPos) <= 1.1f)
            {
                return i;
            }
            i++;
        }
        return 0;
    }
    public void ExplosionAlert()
    {
        int randForExplosion;
        if (elapsed >= 1f)
        {
            randForExplosion = FindPlayerPosIndex();
            elapsed = 0f;
        }
        else
            randForExplosion = Random.Range(0, 9);
        float explosionXPosition = obstacleXPositions[randForExplosion];
        Vector3 V3Explo = new Vector3(explosionXPosition, -0.8f, 0);
        GameObject tempExp = Instantiate(explosionAlertPrefab, V3Explo, Quaternion.identity);
        StartCoroutine(SpawnExplo(V3Explo));
        Destroy(tempExp, 1.03f);
    }
    IEnumerator SpawnExplo(Vector3 v)
    {
        FindObjectOfType<AudioManager>().Play("explosion");
        yield return new WaitForSeconds(1f);
        GameObject exp = Instantiate(explosionPrefab, v, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        exp.GetComponent<CircleCollider2D>().enabled = false;
        Destroy(exp, 0.5f);
    }
    IEnumerator RocketAlertBurst()
    {
        float rocketXPosition;
        Debug.Log("in rocketAlertBurst");
        for (int i = 0; i < obstacleXPositions.Length - 2; i++)
        {
            rocketXPosition = obstacleXPositions[i];
            Vector3 V3rocketAlert = new Vector3(rocketXPosition, -4f, 0);
            GameObject temp = Instantiate(rocketAlertPrefab, V3rocketAlert, Quaternion.identity);
            SpawnRocket(rocketXPosition);
            Destroy(temp, 1f);
            yield return new WaitForSeconds(0.15f);
        }
    }
    private void RocketAlert()
    {
        int rocketXIndex;
        float rocketXPosition;
        if (elapsed >= 1f)
        {
            rocketXIndex = FindPlayerPosIndex();
            elapsed = 0f;
        }
        else
            rocketXIndex = Random.Range(0, 9);
        rocketXPosition = obstacleXPositions[rocketXIndex];
        Vector3 V3rocketAlert = new Vector3(rocketXPosition, -4f, 0);
        GameObject temp = Instantiate(rocketAlertPrefab, V3rocketAlert, Quaternion.identity);
        SpawnRocket(rocketXPosition);
        Destroy(temp, 1f);
    }
    void SpawnRocket(float r)
    {
        FindObjectOfType<AudioManager>().Play("rocket");
        Vector3 V3RocketSpawn = new Vector3(r, -15, 0);
        GameObject go = Instantiate(rocketPrefab, V3RocketSpawn, Quaternion.identity);
    }
    private void Update()
    {
        //elapsed time to control random/explosion rocket x pos 
        elapsed += Time.deltaTime;

        //get player x pos
        if (player.gameObject)
            playerXPos = player.transform.position.x;

        if (Input.GetKeyDown(KeyCode.H) && !PauseMenu.isPaused)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 2;
            }
            else if (Time.timeScale == 2)
            {
                Time.timeScale = 1;
            }
        }
    }
    void SpawnHeart()
    {
        Vector3 V3 = new Vector3(9.3f, 0, 0);
        GameObject heartInstance = Instantiate(extraHeartPrefab, V3, Quaternion.identity);
    }
}
