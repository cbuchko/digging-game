using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public GameObject template;
    public GameObject[] prefabProbabilities;

    private int spawnAtTime = 10;
    private int initialSpawn = 10;
    private int farthestSpawn = -10;

    private int gridXMinimum = -7;
    private int gridXMaximum = 6;

    private int gridY = -2;
    
    private Tilemap tilemap;
    private Transform playerTransform;
    
    private List<Tile> tiles;
    private List<Vector3Int> positions;
    
    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<Tile>();
        positions = new List<Vector3Int>();
    
        tilemap = template.GetComponent<Tilemap>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        

        //spawn the initial batch of prefabs
        for(int i = 0; i < initialSpawn - 2; i++, gridY--){
            spawnXLayer(gridY);
        }

        Vector3Int[] positionsArray = positions.ToArray();
        Tile[] tilesArray = tiles.ToArray();
        tilemap.SetTiles(positionsArray, tilesArray);

        StartCoroutine(regenerateCollider());
        
    }

    // spawn more tiles as necessary, called in character dig;
    public void spawnMoreTiles()
    {
        int playerDepth = ((int)System.Math.Floor((playerTransform.position.y/1.25)));

        //if the world ends within the spawn range, extend the world
        if((playerDepth - (spawnAtTime)) < farthestSpawn){
            Debug.Log("spawning more");
            positions.Clear();
            tiles.Clear();

            for(int y = farthestSpawn; y > farthestSpawn - spawnAtTime; y--){
                spawnXLayer(y);
            }

            farthestSpawn = farthestSpawn - spawnAtTime;

            Vector3Int[] positionsArray = positions.ToArray();
            Tile[] tilesArray = tiles.ToArray();
            tilemap.SetTiles(positionsArray, tilesArray);

            //StartCoroutine(regenerateCollider());
        }
        
    }

    private void spawnXLayer(int yLayer){
        for(int i = gridXMinimum ; i <= gridXMaximum ; i++){
            Vector3Int position = new Vector3Int(i, yLayer, 0);

            Tile tile = ScriptableObject.CreateInstance<Tile>();
            GameObject objectToSpawn = pickRandomObject();
            tile.gameObject = objectToSpawn;

            positions.Add(position);
            tiles.Add(tile);
        } 
    }

    private GameObject pickRandomObject(){
        int rand = Random.Range(0, prefabProbabilities.Length);
        GameObject obj = prefabProbabilities[rand];
        return obj;
    }

    //wait for tiles to spawn before updating composite collider
    private IEnumerator regenerateCollider(){
        yield return new WaitForEndOfFrame();

        tilemap.GetComponent<CompositeCollider2D>().GenerateGeometry();

        yield return new WaitForEndOfFrame();
    }
}
