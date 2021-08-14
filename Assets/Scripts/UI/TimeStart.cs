using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeStart : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ChangeTime());
    }

    private IEnumerator ChangeTime()
    {
        for (int i = 3; i > 0; i--)
        {
            text.text = i.ToString();
            yield return new WaitForSeconds(1f);
            
        }
        gameObject.SetActive(false);
    }
}
