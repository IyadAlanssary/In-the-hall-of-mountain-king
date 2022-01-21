using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    float time;
    public GameObject RocketPrefab, ExplosionPrefab, rocketAlertPrefab, explosionAlertPrefab;
    //public GameObject StarPrefab;
    //public GameObject SpikePrefab;
    private float rand, rand1, rand2, rocketSpawnTime;
    
    
    Vector3 V3explo;
    public void explosionAlert(){
        rand2 = Random.Range(-6, 6);
        V3explo = new Vector3(rand2, -2, 0);
        GameObject tempExp = Instantiate(explosionAlertPrefab, V3explo, Quaternion.identity);
        Invoke("spawnExplosion", 0.9f);
        Destroy(tempExp, 1f);
    }
    void spawnExplosion(){
        GameObject exp = Instantiate(ExplosionPrefab, V3explo, Quaternion.identity);
        Destroy(exp, 0.3f);
        Invoke("explosionAlert", 1f);
    }


    /*
    void spawnSpike()
    {
        rand1 = Random.Range(-3, 3);
        Vector3 V3 = new Vector3(9, rand1, 0);
        GameObject go = Instantiate(SpikePrefab, V3, Quaternion.Euler(0, 0, Random.Range(0, 180)));
    }
    
    
    void spawnStar(){
            Vector3 V3 = new Vector3(9, Random.Range(-5, 5), 0);
            GameObject go = Instantiate(StarPrefab, V3, Quaternion.identity);
    }
*/

    void rocketAlert()
    {
        rand = Random.Range(-6, 6);
        Vector3 V3rocketAlert = new Vector3(rand, -4.35f, 0);
        GameObject temp = Instantiate(rocketAlertPrefab, V3rocketAlert, Quaternion.identity);
        Invoke("spawnRocket", 0.9f);
        Destroy(temp, 1f);
        
    }
    void spawnRocket()
    {
        if(rocketSpawnTime > 0.3f)
            rocketSpawnTime -= 0.2f;
        Debug.Log("Rocket Spawn Time: " + rocketSpawnTime);
        Vector3 V3RocketSpawn = new Vector3(rand, -7, 0);
        GameObject go = Instantiate(RocketPrefab, V3RocketSpawn, Quaternion.identity);
        Invoke("rocketAlert", rocketSpawnTime);
    }
    private void Start()
    {
        time = Time.time;
        rocketSpawnTime = 3f;
        /*
        Invoke("rocketAlert", 1f);
        Invoke("rocketAlert", 3f);
        Invoke("rocketAlert", 5f);
        Invoke("rocketAlert", 5.01f);
        Invoke("rocketAlert", 6.01f);
        Invoke("rocketAlert", 6.02f);
        Invoke("rocketAlert", 6.03f);
        */
        //InvokeRepeating("rocketAlert", 0f, rocketSpawnTime);
        Invoke("rocketAlert", 2f);
        //Invoke("rocketAlert", 2.01f);

        Invoke("explosionAlert", 1f);
        //InvokeRepeating("explosionAlert", 0f, rocketSpawnTime);
        //StartCoroutine(explosionAlert());
        

        //InvokeRepeating("spawnSpike", 2f, 5f);
        //InvokeRepeating("spawnStar", 10f, 2f);
    }
    
    
    /*
    Vector3 V3explo;
    IEnumerator explosionAlert(){
        rand2 = Random.Range(-6, 6);
        V3explo = new Vector3(rand2, -2, 0);
        GameObject tempExp = Instantiate(explosionAlertPrefab, V3explo, Quaternion.identity);
        //Invoke("spawnExplosion", 0.9f);
        yield return new WaitForSeconds(0.9f);
        spawnExplosion(0.1f, V3explo);
        //Destroy(tempExp, 1f);
        Destroy(tempExp);
    }
    IEnumerator spawnExplosion(float t, Vector3 v3){
        yield return new WaitForSeconds(t);
        GameObject exp = Instantiate(ExplosionPrefab, v3, Quaternion.identity);
        Destroy(exp, 0.3f);
        StartCoroutine(explosionAlert());
    }
    */


    
    private void Awake()
    {
        instance = this;
    }
}
