using UnityEngine;
using UnityEngine.Advertisements;

public static class AdsManager
{
    public static void Init ()
    {
        #if (UNITY_IOS || UNITY_Editor)
        Advertisement.Initialize("1402074");
        #endif

        #if (UNITY_ANDROID || UNITY_Editor)
        Advertisement.Initialize("1402073");
        #endif
    }

    public static void ShowAd()
    {
        //if (Advertisement.IsReady())
        //{
        //    Advertisement.Show();
        //}
    }

    public static void ShowRewardedAd()
    {
        //if (Advertisement.IsReady("rewardedVideo"))
        //{
        //    var options = new ShowOptions { resultCallback = HandleShowResult };
        //    Advertisement.Show("rewardedVideo", options);
        //}
    }

    //private static void HandleShowResult(ShowResult result)
    //{
    //    switch (result)
    //    {
    //        case ShowResult.Finished:
    //            Debug.Log("The ad was successfully shown.");
    //            //
    //            // YOUR CODE TO REWARD THE GAMER
    //            ScoreManager.WispesCollected += GameGlobel.WATCH_AD_EARN_AMOUNT;
    //            break;
    //        case ShowResult.Skipped:
    //            Debug.Log("The ad was skipped before reaching the end.");
    //            break;
    //        case ShowResult.Failed:
    //            Debug.LogError("The ad failed to be shown.");
    //            break;
    //    }
    //}
}
