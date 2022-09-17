using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour
{
    public Image firstimage, secondimage, thirdimage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstImage()
    {
        firstimage.gameObject.SetActive(true);
        secondimage.gameObject.SetActive(false);
        thirdimage.gameObject.SetActive(false);
    }
    public void SecondImage()
    {
        firstimage.gameObject.SetActive(false);
        secondimage.gameObject.SetActive(true);
        thirdimage.gameObject.SetActive(false);
    }
    public void ThirdImage()
    {
        firstimage.gameObject.SetActive(false);
        secondimage.gameObject.SetActive(false);
        thirdimage.gameObject.SetActive(true);
    }
}
