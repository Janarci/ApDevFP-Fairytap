using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class CoinHandler : MonoBehaviour
{
    private int coinTotal = 0;
    private int scoreTotal = 0;
    [SerializeField] private Text coinText;
    [SerializeField] private Text scoreText;

	private void Start()
	{
        AdsManager._AdsManager.onAdDone += OnAdDone;
	}

	void Update()
    {
        coinText.text = $"Gold: {coinTotal}";
        scoreText.text = $"Score: {scoreTotal}";
        FindObjectOfType<WebHandlerScript>().currentPlayerScore = scoreTotal;
    }

    public void getCoin()
    {
        coinTotal += Random.Range(2,6) * 10;
    }
    public void getScoreFairy()
    {
        scoreTotal += 10;
    }
    public void getScoreBoss()
    {
        scoreTotal += 50;
    }

    public void getPathCompleteCoin(int value)
    {
        coinTotal += value;
    }

    public int getGoldTotal()
    {
        return coinTotal;
    }

    public void setGoldTotal(int value)
    {
        coinTotal = value;
    }


    public void OnAdDone(object sender, AdFinishEventArgs args)
    {
        
        if (args.PlacementID == AdsManager.SampleRewarded)
        {
            if (args.AdResult == ShowResult.Finished)
            {
                Debug.Log("ad finish");
            }
        }

        
    }
}
