using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    private float speed = 2f;
    private bool moveIsComplete = false;
    private Vector3 nextPosition;
    private Main mainScript;

    // Use this for initialization
    void Start () {
        mainScript = GameObject.Find("MainController").GetComponent<Main>();
    }
	
	// Update is called once per frame
	void Update () {
        if (moveIsComplete && transform.position != nextPosition) {
            MoveToPosition(nextPosition);
        }
    }

    public void MoveFromPosition(Vector3 sourcePosition) {
        bool isSourceNearby = IsSourceNearby(sourcePosition);
        if (!isSourceNearby) {
            Debug.LogWarning("Something pushed block: " + name + " from invalid position: " + sourcePosition);
        } else {
            nextPosition = CalculateTargetPosition(sourcePosition);
            moveIsComplete = true;
        }
    }

    private void MoveToPosition(Vector3 position) {
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }

    private bool IsSourceNearby(Vector3 sourcePosition) {
        if (Mathf.Abs(transform.position.x - sourcePosition.x) > 1) {
            return false;
        }
        if (Mathf.Abs(transform.position.y - sourcePosition.y) > 1) {
            return false;
        }
        return true;
    }

    private Vector3 CalculateTargetPosition(Vector3 sourcePosition) {
        Vector3 targetPosition = new Vector3();
        targetPosition.y = 0.625f;
        if (transform.position.x == sourcePosition.x) {
            targetPosition.x = transform.position.x;
        } else {
            targetPosition.x = (sourcePosition.x > transform.position.x ? 0 : mainScript.floorTilesMaxCols - 1);
        }
        if (transform.position.z == sourcePosition.z) {
            targetPosition.z = transform.position.z;
        } else {
            targetPosition.z = (sourcePosition.z > transform.position.z ? 0 : mainScript.floorTilesMaxRows - 1);
        }
        return targetPosition;
    }

}
