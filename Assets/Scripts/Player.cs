using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int moveIndex = 0;
    public bool pathIsComplete = false;

    private float speed = 2f;
    private int[][] path = new int[25][]; //TODO: dynamic size
    private int pathIndex = 0;
    private Vector3 nextPosition;
    private bool isWon = false;

    // Use this for initialization
    void Start() {
        name = name.Replace("(Clone)", "");
    }
	
	// Update is called once per frame
	void Update () {
        MoveToPath(path);

        // WIN
        if (!isWon && pathIsComplete && path[pathIndex] == null) {
            Debug.Log("*WIN");
            isWon = true;
        }
    }

    public void AddToPath(int[] move) {
        path[moveIndex] = move;
        moveIndex++;
    }
    private void MoveToPath(int[][] path) {
        if (pathIsComplete && pathIndex < path.Length && path[pathIndex] != null) {
            nextPosition = new Vector3(path[pathIndex][1], 0.625f, path[pathIndex][0]);
            if (transform.position.Equals(nextPosition)) {
                pathIndex++;
            } else {
                MoveToPosition(nextPosition);
            }
        }
    }
    private void MoveToPosition(Vector3 position) {
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }
    public bool IsNextMoveValid(int[] nextMove) {
        if (moveIndex == 0) {
            // First touched floor tile has to be the one with the player
            return (nextMove[0] == 0 && nextMove[1] == 0);
        }
        int rowDiff = Mathf.Abs(path[moveIndex - 1][0] - nextMove[0]);
        int colDiff = Mathf.Abs(path[moveIndex - 1][1] - nextMove[1]);
        return ((rowDiff == 0 && colDiff == 1) ||
            (rowDiff == 1 && colDiff == 0));
    }

}
