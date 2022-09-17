using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadHunter : MonoBehaviour
{
    public GameObject canvas;
    public Text text;
    void Start()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canvas.GetComponent<BackgroundScript>().FirstImage();
            text.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.gameObject.SetActive(false);
        }
    }
}