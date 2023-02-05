using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFlip : MonoBehaviour
{
    public enum Result
    {
        Home,
        Away,
    }

    public Result result = Result.Home;
    public Sprite homeSprite;
    public Sprite awaySprite;

    private SpriteRenderer resultSpriteRenderer;
    private GameObject resultObject;

    // Start is called before the first frame update
    void Start()
    {
        resultObject = this.transform.Find("CoinFlipResultSprite").gameObject;
        resultObject.SetActive(false);
        this.resultSpriteRenderer = resultObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.result == Result.Home)
        {
            this.resultSpriteRenderer.sprite = homeSprite;
        }
        else
        {
            this.resultSpriteRenderer.sprite = awaySprite;
        }
    }

    public void HandleAnimationEvent(string _eventName)
    {
        resultObject.SetActive(true);
        Destroy(this.gameObject, 2);
    }
}
