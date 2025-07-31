using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Stage " + FindObjectOfType<ObstacleGenerator>().phase;
        StartCoroutine(waitDestroy());
    }

    IEnumerator waitDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(transform.parent.gameObject);
    }
}
