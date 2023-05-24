using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressVisualizer : MonoBehaviour
{
    GameObject MoneySprite;

    // Start is called before the first frame update
    void Start()
    {
        MoneySprite = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }

    public void PlayMoneyAnimation()
    {
        MoneySprite.GetComponent<Animator>().Play("PopUpMoneyAnimation");
        MoneySprite.GetComponent<AudioSource>().Play();
    }
}
