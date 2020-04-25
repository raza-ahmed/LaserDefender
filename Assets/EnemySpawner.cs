using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed =  5f;
   
    private bool movingRight = true;
    private float xmin, xmax;

    // Start is called before the first frame update
    void Start()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;

        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

        xmin = leftBoundary.x;
        xmax = rightBoundary.x;

        foreach (Transform child in transform) { 
        GameObject enemy =  Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
        enemy.transform.parent = child;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(movingRight){
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else{
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightBoundaryOfFormation = transform.position.x + (0.5f * width);
        float leftBoundaryOfFormation = transform.position.x - (0.5f * width);

        if(leftBoundaryOfFormation<xmin || rightBoundaryOfFormation > xmax)
        {
            movingRight = !movingRight;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

}
