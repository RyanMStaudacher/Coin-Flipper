using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Text headsOrTailsText;
    [SerializeField] private List<GameObject> coinList;

    private int multiplier = 1;
    private int previousCoinFace = 2;

    private void OnEnable()
    {
        DetectShake.hasBeenShook += ChangeCoin;
        Coin.headsOrTails += DetermineHeadsOrTails;
    }

    private void OnDisable()
    {
        DetectShake.hasBeenShook -= ChangeCoin;
        Coin.headsOrTails -= DetermineHeadsOrTails;
    }

    private void ChangeCoin()
    {
        for (int i = 0; i < coinList.Count; i++)
        {
            GameObject g = GameObject.FindGameObjectWithTag("Coin");
            if(g != null)
            {
                Destroy(g);
            }
        }

        int r = Random.Range(0, coinList.Count);
        Instantiate(coinList[r], new Vector3(0, 0, 0), Quaternion.identity);

        headsOrTailsText.text = "";
        multiplier = 1;
    }

    private void DetermineHeadsOrTails(int coinFace)
    {
        if(coinFace == 0)
        {
            if(coinFace != previousCoinFace)
            {
                multiplier = 1;
                headsOrTailsText.text = "Heads";
            }
            else if(coinFace == previousCoinFace)
            {
                multiplier++;
                headsOrTailsText.text = "Heads x" + multiplier;
            }
        }
        else if(coinFace == 1)
        {
            if(coinFace != previousCoinFace)
            {
                multiplier = 1;
                headsOrTailsText.text = "Tails";
            }
            else if(coinFace == previousCoinFace)
            {
                multiplier++;
                headsOrTailsText.text = "Tails x" + multiplier;
            }
        }

        previousCoinFace = coinFace;
    }
}
