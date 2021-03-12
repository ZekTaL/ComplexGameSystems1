using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private float forceAmount = 500;
    private Rigidbody dragObject;
    private Vector3 orginalPosition;
    private float selectionDistance;

    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject hatchDoor1;
    [SerializeField]
    private GameObject hatchDoor2;
    [SerializeField]
    private GameObject strikeScreen;
    [SerializeField]
    private Scoring scoring;
    [SerializeField]
    private Player player;

    public bool isGameOver = false;
    public float maxSpeed = 25;

    // Start is called before the first frame update
    void Start()
    {
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
            ClickDrag();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameOver)
            {
                isGameOver = true;
                Time.timeScale = 0f;
                GameOver();
            }
            else
            {
                gameOverScreen.SetActive(false);
                isGameOver = false;
                Time.timeScale = 1f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (dragObject)
        {
            Vector3 mousePositionOffset = Camera.main.ScreenToWorldPoint(
                            new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - orginalPosition;

            dragObject.velocity = (orginalPosition + mousePositionOffset - dragObject.transform.position) * forceAmount * Time.deltaTime;
            if (dragObject.velocity.magnitude > maxSpeed)
            {
                dragObject.velocity = dragObject.velocity.normalized * maxSpeed;
            }
        }
    }

    public void StartNewGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        gameOverScreen.SetActive(false);
        strikeScreen.SetActive(false);

        // I set the hatch doors to kinematic so they dont move at start
        hatchDoor1.GetComponent<Rigidbody>().isKinematic = true;
        hatchDoor2.GetComponent<Rigidbody>().isKinematic = true;

        scoring.ResetScore();
        player.ResetPlayer();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        isGameOver = true;
        gameOverScreen.SetActive(true);

    }

    public void PlayAgain()
    {
        // Inefficient but easier then reset the position of everything...
        SceneManager.LoadScene("GameScene");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ClickDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                selectionDistance = Vector3.Distance(ray.origin, hit.point);

                if (!hit.rigidbody)
                    return;

                // I can drag only the ball
                if (hit.rigidbody.CompareTag("Ball"))
                {
                    dragObject = hit.rigidbody;
                    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                            Input.mousePosition.y,
                                                            selectionDistance));
                    orginalPosition = hit.collider.transform.position;
                }    
            }

        }

        if (Input.GetMouseButtonUp(0) && dragObject)
        {
            dragObject.AddForce(Vector3.down * 50, ForceMode.VelocityChange);
            dragObject = null;
        }
    }

    public IEnumerator Strike()
    {
        // open the hatch!
        hatchDoor1.GetComponent<Rigidbody>().isKinematic = false;
        hatchDoor2.GetComponent<Rigidbody>().isKinematic = false;
        strikeScreen.SetActive(true);

        yield return new WaitForSeconds(3f);

        strikeScreen.SetActive(false);

        yield return null;
    }
}
