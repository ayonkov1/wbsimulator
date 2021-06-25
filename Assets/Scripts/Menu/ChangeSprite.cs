using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{

    public List<Sprite> _riskFactorImages =  new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        setInactive();
    }

    // Changes the sprite img based on the level of the risk factor
    public void ChangeSpriteImg(int riskFactor)
    {
        Sprite spriteToSet;
       if (riskFactor < 3 )
        {
           spriteToSet =  _riskFactorImages[0];
        } 
       else if (riskFactor < 5)
        {
            spriteToSet = _riskFactorImages[1];
        }
       else
        {
            spriteToSet = _riskFactorImages[2];
        }
        gameObject.GetComponent<Image>().sprite = spriteToSet;
        setActive();
    }

    public void setInactive()
    {
        gameObject.SetActive(false);
    }

    public void setActive()
    {
        gameObject.SetActive(true);
    }
}
