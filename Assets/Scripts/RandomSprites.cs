using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprites : MonoBehaviour
{
    public List<Sprite> HairSprites;
    public List<Sprite> bodySprites;
    public List<Sprite> clothesSprites;

    public GameObject Hair;
    public GameObject Body;
    public GameObject Clothes;
    // Start is called before the first frame update
    void Start()
    {   
        int hairIndex = Random.Range(0, HairSprites.Count);
        int bodyIndex = Random.Range(0, bodySprites.Count);
        int clothesIndex = Random.Range(0, clothesSprites.Count);

        Body.GetComponent<SpriteRenderer>().sprite = bodySprites[bodyIndex];
        Hair.GetComponent<SpriteRenderer>().sprite = HairSprites[hairIndex];
        Clothes.GetComponent<SpriteRenderer>().sprite = clothesSprites[clothesIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
