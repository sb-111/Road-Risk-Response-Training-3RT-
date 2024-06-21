using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShortPanel : MonoBehaviour
{
    RectTransform rectTransform;

    // �ӵ��� ������ ����
    public float speed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransform.anchoredPosition.y >= 150)
        {
            Destroy(gameObject);
        }
        rectTransform.anchoredPosition = new Vector2(
            rectTransform.anchoredPosition.x,
            rectTransform.anchoredPosition.y + speed * Time.deltaTime
        );
    }
}

