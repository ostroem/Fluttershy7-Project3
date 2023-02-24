using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hulahoop_Falling : MonoBehaviour
{
    [SerializeField] GameObject[] HoopPrefab;
    [SerializeField] float nextSpawn = 1.5f;
    [SerializeField] float minRange;
    [SerializeField] float maxRange;
   //[SerializeField] int framesPerTime = 10; // number of frames per time unit
   [SerializeField] float fallSpeed = 1f; // adjustable speed for falling object
    [SerializeField] bool isSpawnPointInt;


    void Start()
    {
        if (isSpawnPointInt)
        {
            StartCoroutine(HoopSpawnInt());
            return;
        }
        StartCoroutine(HoopSpawn());

    }


    IEnumerator HoopSpawn()
    {
        while (true)
        {
            var wanted = Random.Range(minRange, maxRange);
            var position = new Vector3(wanted, transform.position.y);

            GameObject gameObject = Instantiate(HoopPrefab[Random.Range(0, HoopPrefab.Length)], position, Quaternion.identity);
            gameObject.transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0); // move object down by fallSpeed per frame

            yield return new WaitForSeconds(nextSpawn);
            Destroy(gameObject, 5f);
        }
    }
    IEnumerator HoopSpawnInt()
    {
        while (true)
        {
            var wanted = Random.Range((int)minRange, (int)maxRange + 1);
            var position = new Vector3(wanted, transform.position.y);

            GameObject gameObject = Instantiate(HoopPrefab[Random.Range(0, HoopPrefab.Length)], position, Quaternion.identity);
            gameObject.transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0); // move object down by fallSpeed per frame

            yield return new WaitForSeconds(nextSpawn);
            Destroy(gameObject, 5f);
        }
    }

}