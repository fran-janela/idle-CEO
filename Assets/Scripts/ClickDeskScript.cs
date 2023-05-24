using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDeskScript : MonoBehaviour
{
    private bool canClick = true;
    private float clickDelayTimer = 0f;

    public float clickDelay = 10.0f;

    Collider2D collider;
    SpriteRenderer spriteRenderer;

    SpriteRenderer propSpriteRenderer;

    public GameObject PVisualizer;

    private Color Dim = new Color(0.3f, 0.3f, 0.3f, 1f);
    private Color Bright = new Color(1f, 1f, 1f, 1f);

    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        propSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                if (collider.OverlapPoint(touchPosition))
                {
                    if (touch.phase == TouchPhase.Began && canClick){
                        canClick = false;
                        clickDelayTimer = 0.0f;
                        DimAssets();
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (collider.OverlapPoint(mousePosition))
            {
                if (canClick)
                {
                    canClick = false;
                    clickDelayTimer = 0.0f;
                    DimAssets();
                }
            }
        }

        if(!canClick){
            clickDelayTimer += Time.deltaTime;
            if (clickDelayTimer >= clickDelay)
            {
                Debug.Log("Click");
                Debug.Log(clickDelay);
                GameManager.IncrementMoney(200.0f);
                canClick = true;
                BrightenAssets();
                PVisualizer.GetComponent<ProgressVisualizer>().PlayMoneyAnimation();
            }
        }
    }

    private void DimAssets()
    {
        spriteRenderer.color = Dim;
        propSpriteRenderer.color = Dim;
    }

    private void BrightenAssets()
    {
        spriteRenderer.color = Bright;
        propSpriteRenderer.color = Bright;
    }
}
