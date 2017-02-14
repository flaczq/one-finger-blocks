using UnityEngine;
using System.Collections;

public class FloorTile : MonoBehaviour {

    public Material usedMaterial;

    private new Renderer renderer;
    private int[] move;
    private Player playerScript;
    private bool isMouseOver = false;
    private bool isMouseDown = false;

    // Use this for initialization
    void Start() {
        renderer = GetComponent<Renderer>();
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        handleTouch();
        handleClick();
    }

    public void OnMouseEnter() {
        isMouseOver = true;
    }
    public void OnMouseExit() {
        isMouseOver = false;
    }
    public void OnMouseUp() {
        if (isMouseDown && playerScript.moveIndex > 0) {
            playerScript.pathIsComplete = true;
        }
    }
    public void OnMouseDown() {
        if (isMouseOver) {
            isMouseDown = true;
        }
    }
    private void OnTouchEnded() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended &&
            playerScript.moveIndex > 0) {
            playerScript.pathIsComplete = true;
        }
    }

    private void afterTouch() {
        move = new int[] { Mathf.FloorToInt(transform.position.z), Mathf.FloorToInt(transform.position.x) };
        if (!playerScript.pathIsComplete &&
            playerScript.IsNextMoveValid(move)) {
            playerScript.AddToPath(move);
            renderer.sharedMaterial = usedMaterial;
        }
    }

    private void handleClick() {
        if (isMouseOver && Input.GetMouseButton(0) &&
            !renderer.sharedMaterial.Equals(usedMaterial)) {
            afterTouch();
        }
    }
    private void handleTouch() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began &&
            !renderer.sharedMaterial.Equals(usedMaterial)) {
            if (isTouchingCurrent(Input.GetTouch(0).position)) {
                afterTouch();
            }
        }
    }
    private bool isTouchingCurrent(Vector2 position) {
        Vector3 stw = Camera.main.ScreenToWorldPoint(position);
        Vector2 touchPosition = new Vector2(stw.x, stw.y);
        return Physics2D.OverlapPoint(touchPosition);
    }

}
