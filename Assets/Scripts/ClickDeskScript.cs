using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ClickDeskScript : MonoBehaviour
{
    private bool canClick = true;
    private float clickDelayTimer = 0f;

    public float clickDelay = 7.0f;

    public int desk_id; // ID do laptop atual

    Collider2D collider;

    public Collider2D RunButtonCollider;

    public SpriteRenderer ButtonUpSpriteRenderer, ButtonDownSpriteRenderer;

    public Canvas UpgradeCanvas;

    public GameObject PVisualizer;

    private Color Dim = new Color(0.3f, 0.3f, 0.3f, 1f);
    private Color Bright = new Color(1f, 1f, 1f, 1f);

    // Queue Script from Object
    public RoomQueue roomQueue;
    private Transform ManagerTrigger;


    void Start()
    {
        desk_id = ExtractNumberFromString(transform.parent.name);
        // Debug.Log("DeskSet ID: " + laptopTableSetID);
        collider = GetComponent<Collider2D>();
        ManagerTrigger = transform.Find("ManagerTrigger").transform;

    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.menuOpen == false)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D clickCollider = Physics2D.OverlapPoint(mousePosition);
            List<GameObject> unityGameObjects = new List<GameObject>();
            if (collider.OverlapPoint(mousePosition)){
                UpgradeCanvas.GetComponent<loadMainUpdate>().openUpdateAll();
            } else if (RunButtonCollider.OverlapPoint(mousePosition) && clickCollider.gameObject == RunButtonCollider.gameObject){
                // Debug.Log("Clique no botão de execução");
                StartClickDelay();
                if (roomQueue.CheckIfTriggerInQueue(ManagerTrigger))
                {
                    roomQueue.RemoveDeskTrigger(ManagerTrigger);
                }
            }
            
            
        }

        if(!canClick){
            clickDelayTimer += Time.deltaTime;
            if (clickDelayTimer >= clickDelay)
            {
                // Debug.Log("Click");
                // Debug.Log(clickDelay);

                // Debug.Log ("Olha o ID do laptop: " + laptopTableSetID);
                GameManager.LaptopInfo laptopInfo = GameManager.GetLaptopInfo(desk_id);
                GameManager.TableInfo tableInfo = GameManager.GetTableInfo(desk_id);
                // Debug.Log("Os earnings do laptop: " + laptopInfo.earnings + " e o delayTime: " + laptopInfo.delayTime);
                // Debug.Log("Os earnings da mesa: " + tableInfo.earnings + " e o delayTime: " + tableInfo.delayTime);

                clickDelay = tableInfo.delayTime;
                GameManager.IncrementMoney(laptopInfo.earnings);
                
                canClick = true;
                BrightenAssets();
                PVisualizer.GetComponent<ProgressVisualizer>().PlayMoneyAnimation();
            }
        }
        if (canClick && GameManager.GetLaptopParameters(desk_id).level > 0){
            if (!roomQueue.CheckIfTriggerInQueue(ManagerTrigger))
            {
                roomQueue.AddDeskTrigger(ManagerTrigger);
            }
        }
    }

    public void StartClickDelay()
    {
        Debug.Log("Ask to StartClickDelay");
        if (canClick)
        {
            Debug.Log("Started ClickDelay!!!");
            canClick = false;
            clickDelayTimer = 0f;
            DimAssets();
        }
    }

    private void DimAssets()
    {
        ButtonUpSpriteRenderer.enabled = false;
        ButtonDownSpriteRenderer.enabled = true;
        ButtonDownSpriteRenderer.color = Dim;
    }

    private void BrightenAssets()
    {
        ButtonUpSpriteRenderer.enabled = true;
        ButtonDownSpriteRenderer.enabled = false;
    }

    static int ExtractNumberFromString(string input)
    {
        string pattern = @"\d+"; // Matches one or more digits
        Match match = Regex.Match(input, pattern);

        if (match.Success)
        {
            int number;
            bool success = int.TryParse(match.Value, out number);

            if (success)
                return number;
        }

        // Return a default value or throw an exception if no number found
        return -1;
    }
}
