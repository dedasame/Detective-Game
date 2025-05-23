using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

public class LeaderboardsSample : MonoBehaviour
{
    // Create a leaderboard with this ID in the Unity Cloud Dashboard
    const string LeaderboardId = "Time_Leader_Board";
    string VersionId { get; set; }
    int Offset { get; set; }
    int Limit { get; set; }
    int RangeLimit { get; set; }
    List<string> FriendIds { get; set; }


    async void Awake()
    {
        await UnityServices.InitializeAsync();

        await SignInAnonymously();
    }

    async Task SignInAnonymously()
    {
        AuthenticationService.Instance.ClearSessionToken();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
            ScoreBoardScript.Instance.AddScore();
        };
        AuthenticationService.Instance.SignInFailed += s =>
        {
            // Take some action here...
            Debug.Log(s);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }


    public async void AddScore()
    {
        //var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, 76.23);
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, (double)PlayerPrefs.GetFloat("time"));
        GetPaginatedScores();
        GetPlayerScore();
    }

    public async void GetScores()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

    }

    public async void GetPaginatedScores()
    {
        Offset = 0;
        Limit = 50;
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions{Offset = Offset, Limit = Limit});

        for(int i = 0; i < scoresResponse.Results.Count; i++)
        {
            //Debug.Log("Rank : "+ scoresResponse.Results[i].Rank + 1 + " username : " + scoresResponse.Results[i].PlayerName + 
                //" score : " + scoresResponse.Results[i].Score + "tier : " + scoresResponse.Results[i].Tier);

            GetComponentInChildren<ScoreBoardScript>().leaderBoardEntries[i].SetActive(true);

            EntryScript entry = GetComponentInChildren<ScoreBoardScript>().leaderBoardEntries[i].GetComponent<EntryScript>();

            entry.username.SetText(scoresResponse.Results[i].PlayerName);
            entry.rank.SetText((scoresResponse.Results[i].Rank + 1).ToString());
            entry.time = (float)scoresResponse.Results[i].Score;
            entry.scoreTime.SetText(scoresResponse.Results[i].Score.ToString("0.##"));
            entry.tier.SetText(scoresResponse.Results[i].Tier);
            entry.SetColor();
        }
    }

    public async void GetPlayerScore()
    {
        var scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
        EntryScript entry = GetComponentInChildren<ScoreBoardScript>().playerInfo.GetComponent<EntryScript>();
        entry.username.SetText(scoreResponse.PlayerName);
        entry.rank.SetText((scoreResponse.Rank + 1).ToString());
        entry.time = (float)scoreResponse.Score;
        entry.scoreTime.SetText(scoreResponse.Score.ToString("0.##"));
        entry.tier.SetText(scoreResponse.Tier);
        entry.SetColor();
    }

    public async void GetVersionScores()
    {
        var versionScoresResponse =
            await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId);
    Debug.Log(JsonConvert.SerializeObject(versionScoresResponse));
    }
}
