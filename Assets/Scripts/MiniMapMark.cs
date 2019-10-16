using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapMark : MonoBehaviour
{
    private Transform snake;
    private RectTransform rectTransform;
    public bool showMark;
    public float posiRadius;

    public Vector3 targetPosi;
    private Image image;

    public float posiWeight;

    // Start is called before the first frame update
    void Start()
    {
        snake = GameObject.Find("Head").transform;
        rectTransform = gameObject.GetComponent<RectTransform>();
        image = gameObject.GetComponent<Image>();
    }

    public void ChangeTarget(Vector3 targetPosition)
    {
        targetPosi = targetPosition;
    }

    public void ChangeMarkPosi(Vector3 targetPosition)
    {
        targetPosi = targetPosition;
    }

    public void StartShowMark(Vector3 targetPosition)
    {
        image.enabled = true;
        showMark = true;
        targetPosi = targetPosition;
    }

    public void EndShowMark()
    {
        image.enabled = false;
        showMark = false;
    }

    private void CalculateImagePosi(Vector3 player, Vector3 target, float radius)
    {
        Vector2 temp = Vector2.ClampMagnitude((target - player) * posiWeight, radius);
        rectTransform.anchoredPosition = temp;
    }

    // Update is called once per frame
    void Update()
    {
        if (showMark)
        {
            CalculateImagePosi(snake.position, targetPosi, posiRadius);
        }

    }


}
