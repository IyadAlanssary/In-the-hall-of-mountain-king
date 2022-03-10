using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject rocketPrefab, explosionPrefab, rocketAlertPrefab, explosionAlertPrefab, player, pauseMenuObject, gameOverObject;
    private int rocketRandom, lastRocketRandom = 0, randForExplosion1, lastExplosionRandom = 0;
    private float randForExplosion2, rocketXPosition, elapsed = 0f;
    private float playerXPos;
    Vector3 V3Explo;
    private float[] rocketXPositions = {-7.35f, -5.5f, -3.7f, -1.85f, -0.03f, 1.79f, 3.65f, 5.58f, 7.47f};
    //public GameObject StarPrefab;
    //public GameObject SpikePrefab;

    public void explosionAlert(){
        randForExplosion1 = Random.Range(0, 9);
        if(randForExplosion1 == lastExplosionRandom){
            randForExplosion1 = Random.Range(0, 9);
        }
        randForExplosion2 = randForExplosion1; //+0.5f
        V3Explo = new Vector3(randForExplosion2, 0, 0);
        GameObject tempExp = Instantiate(explosionAlertPrefab, V3Explo, Quaternion.identity);
        Invoke("spawnExplosion", 0.9f);
        Destroy(tempExp, 1f);
    }
    void spawnExplosion(){
        GameObject exp = Instantiate(explosionPrefab, V3Explo, Quaternion.identity);
        Destroy(exp, 0.3f);
        Invoke("explosionAlert", 1f);
    }
    public void rocketAlert(){
        rocketRandom = Random.Range(0, 9);
        if(elapsed >= 1f){
            rocketRandom = notBinarySearch();
            elapsed = 0f;
        }
        // if( (rocketRandom == lastRocketRandom || rocketRandom == -1 ) ){
        //     rocketRandom = Random.Range(0, 9);
        // }
        lastRocketRandom = rocketRandom;
        rocketXPosition = rocketXPositions[rocketRandom];
        Vector3 V3rocketAlert = new Vector3(rocketXPosition, -4f, 0);
        GameObject temp = Instantiate(rocketAlertPrefab, V3rocketAlert, Quaternion.identity);
        spawnRocket(rocketXPosition);
        Destroy(temp, 1f);
    }
    private int binarySearch(){
        int l=0, r = rocketXPositions.Length - 1, mid, i = 0;
        while(l <= r && i <= 8){
            mid = l + ( r - 1 ) / 2;
            if(Mathf.Abs( rocketXPositions[mid] - playerXPos ) <= 2f ) {
                Debug.Log("wanted pos: " + rocketXPositions[mid]);
                return mid;
            }
            else if ( rocketXPositions[mid] - playerXPos <= 0){
                l = mid + 1;
            }
            else{
                r = mid - 1;
            }
            i++;
        }
        Debug.Log("-1 + i=" + i );
        return -1;
        
    }
    private int notBinarySearch(){
        int i = 0;
        while(i < rocketXPositions.Length){
            if (Mathf.Abs( rocketXPositions[i] - playerXPos ) <= 1.2f){
                Debug.Log("Success "+rocketXPositions[i]);
                return i;
            }
            i++;
        }
        Debug.Log("-1");
        return -1;
    }
    void spawnRocket(float r){
        Vector3 V3RocketSpawn = new Vector3(r , -15, 0);
        GameObject go = Instantiate(rocketPrefab, V3RocketSpawn, Quaternion.identity);
    }
    private void Start(){
        //Invoke("explosionAlert", 1f);
        
        // InvokeRepeating("spawnSpike", 2f, 5f);
        // InvokeRepeating("spawnStar", 10f, 2f);
    }
    
    private void Awake(){
        instance = this;
    }
    private void Update() {
        //elapsed time to control random rocket x pos 
        elapsed += Time.deltaTime;
        //get player x pos
        if(player.gameObject)
            playerXPos = player.transform.position.x;

        //Pause/Unpause Game
        if(Input.GetKeyDown(KeyCode.P)){
            if(Time.timeScale == 0){
                pauseMenuObject.SetActive(false);
                AudioListener.pause = false;
                Time.timeScale = 1;
            }
            else{
                pauseMenuObject.SetActive(true);
                AudioListener.pause = true;
                Time.timeScale = 0;
            }
        }
        if ( Input.GetKeyDown(KeyCode.R) ){
            reloadGame();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            quitGame();
            //gameOverObject.SetActive(!gameOverObject.activeSelf);
            // if(Time.timeScale == 0){
            //     AudioListener.pause = false;
            //     Time.timeScale = 1;
            // }
            // else{
            //     AudioListener.pause = true;
            //     Time.timeScale = 0;
            // }
        }
    }
    public void quitGame(){
        Application.Quit();
    }
    public void reloadGame(){
        SceneManager.LoadScene("1");
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
    /*
    void spawnSpike(){
        rand1 = Random.Range(-3, 3);
        Vector3 V3 = new Vector3(9, rand1, 0);
        GameObject go = Instantiate(SpikePrefab, V3, Quaternion.Euler(0, 0, Random.Range(0, 180)));
    }
    void spawnStar(){
            Vector3 V3 = new Vector3(9, Random.Range(-5, 5), 0);
            GameObject go = Instantiate(StarPrefab, V3, Quaternion.identity);
    }
    */
}
