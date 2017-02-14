using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    public GameObject floorTilePrefab;
    public GameObject blockPrefab;
    public GameObject playerPrefab;
    public int floorTilesMaxRows = 5;   // Position 'z'
    public int floorTilesMaxCols = 5;   // Position 'x'
    public int[] startPosition = { 0, 0 };
    public int[] endPosition = { 5, 5 };

    private ArrayList floorTiles = new ArrayList();
    private ArrayList blocks = new ArrayList();

    // Use this for initialization
    void Start() {
        SpawnPlayer(startPosition);
        SpawnFloorTiles();
        SpawnBlocks();
    }

    // Update is called once per frame
    void Update() {

    }

    void SpawnFloorTiles() {
        GameObject floorTile;
        for (int row = 0; row < floorTilesMaxRows; row++) {
            for (int col = 0; col < floorTilesMaxCols; col++) {
                floorTile = (GameObject) Instantiate(floorTilePrefab, new Vector3(col, 0, row), Quaternion.identity);
                floorTile.name = (floorTilePrefab.name + row + "_" + col);
                floorTiles.Add(floorTile);
            }
        }
    }
    void SpawnPlayer(int[] position) {
        Vector3 playerPosition;
        if (position == null) {
            Debug.Log("No spawn position for Player, randomizing...");
            playerPosition = new Vector3(Random.Range(0, floorTilesMaxCols), 0.625f, Random.Range(0, floorTilesMaxRows));
        } else {
            playerPosition = new Vector3(position[1], 0.625f, position[0]);
        }
        Instantiate(playerPrefab, playerPosition, Quaternion.identity);
    }
    void SpawnBlocks() {
        GameObject block1, block2;
        block1 = (GameObject) Instantiate(blockPrefab, new Vector3(3, 0.625f, 1), Quaternion.identity);
        block1.name = (blockPrefab.name + 1 + "_" + 3);
        block2 = (GameObject) Instantiate(blockPrefab, new Vector3(2, 0.625f, 4), Quaternion.identity);
        block2.name = (blockPrefab.name + 4 + "_" + 2);
        blocks.Add(block1);
        blocks.Add(block2);
    }

}
