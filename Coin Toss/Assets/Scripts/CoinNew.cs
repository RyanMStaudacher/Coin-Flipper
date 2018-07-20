using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinNew : MonoBehaviour
{
    [Tooltip("How fast the coin will flip")]
    [SerializeField] private float coinFlipSpeed = 50f;

    [Tooltip("How many times the coin will flip")]
    [SerializeField] private float desiredNumberOfFlips = 5f;

    [SerializeField] private Sprite heads;
    [SerializeField] private Sprite tails;

    SpriteRenderer sRenderer;
    Vector3 desiredRotation;
    private bool shouldRotate = false;
    private float flips = 0f;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update ()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            shouldRotate = true;
        }

        FlipCoin();
    }

    private void FlipCoin()
    {
        if(flips < desiredNumberOfFlips && shouldRotate)
        {
            Vector3 ninetyDegrees = new Vector3(90, 0, 0);
            Vector3 zeroDegrees = new Vector3(0, 0, 0);
            float step = coinFlipSpeed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(desiredRotation), step);

            if(desiredRotation == ninetyDegrees && transform.rotation == Quaternion.Euler(desiredRotation))
            {
                desiredRotation = zeroDegrees;

                if(sRenderer.sprite == heads)
                {
                    sRenderer.sprite = tails;
                }
                else if(sRenderer.sprite == tails)
                {
                    sRenderer.sprite = heads;
                }
            }
            else if(desiredRotation == zeroDegrees && transform.rotation == Quaternion.Euler(desiredRotation))
            {
                flips++;
                desiredRotation = ninetyDegrees;
            }
        }
        else if(flips >= desiredNumberOfFlips)
        {
            shouldRotate = false;
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
            flips = 0f;
        }
    }
}
