using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject rocketPrefab, explosionPrefab, rocketAlertPrefab, explosionAlertPrefab, player, extraHeartPrefab, pauseMenuObjectActual;
    public static GameObject pauseMenuObject;
    private int rocketRandom, randForExplosion, randomForChoosingRocketOrExplosion;
    private float rocketXPosition, elapsed = 0f, elapsedExplosion = 0f, explosionXPosition, playerXPos;
    Vector3 V3Explo;
    
    private float[] obstacleXPositions = {-7.35f, -5.5f, -3.7f, -1.85f, -0.03f, 1.79f, 3.65f, 5.58f, 7.47f};
    //public GameObject StarPrefab, SpikePrefab;
    public void chooseRocketorExplosion(){
        randomForChoosingRocketOrExplosion = Random.Range(1,5);
        if(randomForChoosingRocketOrExplosion == 2)
            explosionAlert();
        else
            rocketAlert();
    }
    private void Start(){
        //Invoke("rocketAlertBurst", 2);
        StartCoroutine( rocketAlertBurst() );
        pauseMenuObject = pauseMenuObjectActual;
        Time.timeScale = 1;
        InvokeRepeating("spawnHeart", 60, 19);
        Invoke("spawnHeart", 140);
        //Invoke("explosionAlert", 1f);
        
        // InvokeRepeating("spawnSpike", 2f, 5f);
        // InvokeRepeating("spawnStar", 10f, 2f);
    }
    
    private void Awake(){
        instance = this;
    }
    public void explosionAlert(){
        if(elapsedExplosion >= 1f){
            randForExplosion = notBinarySearch();
            elapsedExplosion = 0f;
        }
        else
            randForExplosion = Random.Range(0, 9);
        explosionXPosition = obstacleXPositions[randForExplosion];
        V3Explo = new Vector3(explosionXPosition, -0.8f, 0);
        GameObject tempExp = Instantiate(explosionAlertPrefab, V3Explo, Quaternion.identity);
        StartCoroutine( spawnExplo(V3Explo) );
        Destroy(tempExp, 1.03f);
    }
    IEnumerator spawnExplo(Vector3 v){
        yield return new WaitForSeconds(1f);
        GameObject exp = Instantiate(explosionPrefab, v, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        exp.GetComponent<CircleCollider2D>().enabled = false;
        Destroy(exp, 0.5f);
    }
    IEnumerator rocketAlertBurst(){
        Debug.Log("in rocketAlertBurst");
        for (int i = 0 ; i < obstacleXPositions.Length - 2 ; i++){
            
            rocketXPosition = obstacleXPositions[i];
            Vector3 V3rocketAlert = new Vector3(rocketXPosition, -4f, 0);
            GameObject temp = Instantiate(rocketAlertPrefab, V3rocketAlert, Quaternion.identity);
            spawnRocket(rocketXPosition);
            Destroy(temp, 1f);
            yield return new WaitForSeconds(0.15f);
        }
    }
    private void rocketAlert(){
        if(elapsed >= 1f){
            rocketRandom = notBinarySearch();
            elapsed = 0f;
        }
        else
            rocketRandom = Random.Range(0, 9);
        rocketXPosition = obstacleXPositions[rocketRandom];
        Vector3 V3rocketAlert = new Vector3(rocketXPosition, -4f, 0);
        GameObject temp = Instantiate(rocketAlertPrefab, V3rocketAlert, Quaternion.identity);
        spawnRocket(rocketXPosition);
        Destroy(temp, 1f);
    }
    private int binarySearch(){
        int l=0, r = obstacleXPositions.Length - 1;
        while(l < r){// && i <= 8
            int midLeft = (l + r) / 2;
            int midRight = (l + r + 1) / 2;
            float mid = (obstacleXPositions[midLeft] + obstacleXPositions[midRight]) / 2f;
            // if(obstacleXPositions[mid] < playerXPos) {
            //     Debug.Log("wanted pos: " + obstacleXPositions[mid]);
            //     return mid;
            // }
            if ( mid < playerXPos ){
                l = midRight;//+1
            }
            else{
                r = midLeft;//-1
            }
        }
        return l;//same as r
    }
    private int notBinarySearch(){
        int i = 0;
        while(i < obstacleXPositions.Length){
            if (Mathf.Abs( obstacleXPositions[i] - playerXPos ) <= 1.1f){
                return i;
            }
            i++;
        }
        return 0;
    }
    void spawnRocket(float r){
        Vector3 V3RocketSpawn = new Vector3(r , -15, 0);
        GameObject go = Instantiate(rocketPrefab, V3RocketSpawn, Quaternion.identity);
    }
    float timeScale;
    private void Update() {
        //elapsed time to control random rocket x pos 
        elapsed += Time.deltaTime;
        //elapsed time to control random explosion x pos 
        elapsedExplosion += Time.deltaTime;
        //get player x pos
        if(player.gameObject)
            playerXPos = player.transform.position.x;

        //Pause/unPause Game
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Time.timeScale == 0){
                AudioListener.pause = false;
                Time.timeScale = timeScale;
            }
            else{
                timeScale = Time.timeScale;
                AudioListener.pause = true;
                Time.timeScale = 0;
            }
            pauseMenuObject.SetActive(!pauseMenuObject.activeSelf);
            
        }
        if ( Input.GetKeyDown(KeyCode.H) && pauseMenuObject.activeSelf == false ){
            if(Time.timeScale == 1){
                Time.timeScale = 2;
            }
            else if(Time.timeScale == 2){
                Time.timeScale = 1;
            }
        }
    }
    public void quitGame(){
        Application.Quit();
    }
    public void reloadGame(){
        SceneManager.LoadScene("1");
    }

    /*
    void spawnSpike(){
        rand1 = Random.Range(-3, 3);
        Vector3 V3 = new Vector3(9, rand1, 0);
        GameObject go = Instantiate(SpikePrefab, V3, Quaternion.Euler(0, 0, Random.Range(0, 180)));
    }
    */
    void spawnHeart(){
            Vector3 V3 = new Vector3(9.3f, 0, 0);
            GameObject heartInstance = Instantiate(extraHeartPrefab, V3, Quaternion.identity);
    }
    
}
