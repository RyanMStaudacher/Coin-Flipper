using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    public static event UnityAction<int> headsOrTails;

    [Tooltip("How fast the coin will flip")]
    [SerializeField] private float coinFlipSpeed = 50f;

    [Tooltip("How many times the coin will flip")]
    [SerializeField] private float desiredNumberOfFlips = 5f;

    [SerializeField] private Sprite heads;
    [SerializeField] private Sprite tails;

    [SerializeField] private List<AudioClip> flipSoundEffects;
    [SerializeField] private List<AudioClip> landSoundEffects;

    private List<Sprite> randomList;
    private SpriteRenderer sRenderer;
    private Vector3 desiredRotation;
    private AudioSource audioSource;
    private bool shouldRotate = false;
    private float flips = 0f;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        randomList = new List<Sprite>();
        randomList.Add(heads);
        randomList.Add(tails);
    }

    // Update is called once per frame
    private void Update ()
    {
        if (Input.touchCount > 0 && shouldRotate == false || Input.GetMouseButtonDown(0) && shouldRotate == false)
        {
            shouldRotate = true;

            int r = Random.Range(0, flipSoundEffects.Count);
            audioSource.clip = flipSoundEffects[r];
            audioSource.Play();
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

            if(desiredRotation == ninetyDegrees && transform.rotation == Quaternion.Euler(desiredRotation) && flips < desiredNumberOfFlips - 1)
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
            else if (desiredRotation == ninetyDegrees && transform.rotation == Quaternion.Euler(desiredRotation) && flips >= desiredNumberOfFlips - 1)
            {
                int r = Random.Range(0, 2);

                sRenderer.sprite = randomList[r];
                desiredRotation = zeroDegrees;
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

            if(headsOrTails != null)
            {
                if(sRenderer.sprite == heads)
                {
                    headsOrTails.Invoke(0);
                }
                else if(sRenderer.sprite == tails)
                {
                    headsOrTails.Invoke(1);
                }
            }

            int r = Random.Range(0, landSoundEffects.Count);
            audioSource.clip = landSoundEffects[r];
            audioSource.Play();
        }
    }
}
