using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    Vector3 prevPosition;

    public float zoomOutMin = 6;
    public float zoomOutMax = 17;


    public float minX = -3f;
    public float maxX = 50f;
    public float minY = 4.7f;
    public float maxY = -26f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Somente verdadeiro no começo do toque
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            prevPosition = touchStart;
        }
        if (Input.touchCount == 2){
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = (currentMagnitude - prevMagnitude)/2.5f;

            Zoom(difference * 0.01f);
        }

        if (Input.GetMouseButton(0) && GameManager.menuOpen == false)
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Camera.main.transform.position.x <= maxX && Camera.main.transform.position.x >= minX && Camera.main.transform.position.y <= minY && Camera.main.transform.position.y >= maxY){
                Camera.main.transform.position += direction;
            } else {
                if (Camera.main.transform.position.x > maxX){
                    Camera.main.transform.position = new Vector3(maxX, Camera.main.transform.position.y, Camera.main.transform.position.z);
                } else if (Camera.main.transform.position.x < minX){
                    Camera.main.transform.position = new Vector3(minX, Camera.main.transform.position.y, Camera.main.transform.position.z);
                }
                if (Camera.main.transform.position.y > minY){
                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, minY, Camera.main.transform.position.z);
                } else if (Camera.main.transform.position.y < maxY){
                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, maxY, Camera.main.transform.position.z);
                }
            }
        }
        if (GameManager.menuOpen == false){
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
        
    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}