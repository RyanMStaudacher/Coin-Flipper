using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Tooltip("How fast the coin spins when flipped.")]
    [SerializeField] private float coinFlipSpeed = 5f;

    [SerializeField] private Sprite heads;
    [SerializeField] private Sprite tails;

    private SpriteRenderer sRenderer;
    private int rotations = 0;
    private int desiredRotations = 5;
    private bool shouldRotate = false;
    private bool hasChangedSprite = false;

	// Use this for initialization
	void Start ()
    {
        sRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CoinFlip();
	}

    private void CoinFlip()
    {
        if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            shouldRotate = true;
        }

        if (rotations < desiredRotations && shouldRotate)
        {
            Debug.Log(hasChangedSprite);
            
            if(this.transform.eulerAngles.x > 89f && this.transform.eulerAngles.x < 91f ||
                this.transform.eulerAngles.x < -89f && this.transform.eulerAngles.x > -91f)
            {
                if (sRenderer.sprite == heads && !hasChangedSprite)
                {
                    sRenderer.sprite = tails;
                    hasChangedSprite = true;
                }
                else if (sRenderer.sprite == tails && !hasChangedSprite)
                {
                    sRenderer.sprite = heads;
                    hasChangedSprite = true;
                }
            }

            if (this.transform.eulerAngles.x > -1f && this.transform.eulerAngles.x < 1f ||
                this.transform.eulerAngles.x > 179f && this.transform.eulerAngles.x < -179f)
            {
                Debug.Log("hey");
                rotations++;
                hasChangedSprite = false;
            }

            this.transform.Rotate(new Vector3(coinFlipSpeed * Time.deltaTime, 0, 0));
        }
        else if(rotations >= desiredRotations)
        {
            shouldRotate = false;
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
            rotations = 0;
        }
    }
}
