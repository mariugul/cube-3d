using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class CheckReleases : MonoBehaviour
{
    // GitHub repository RSS feed info
    const string CUBE3D_RSS = "https://github.com/mariugul/cube-3d/releases.atom";
    //const string TEST_RSS = "https://github.com/mariugul/Full-led-cube/releases.atom";

    // The content of the RSS feed from cube-3d repository
    string rss_content = "";

    // Internet Connection
    bool new_content = false;

    // BUMP
    //-------------------------------------------------------------------------
    // The date and time of the current installed release. NB! Bump this on new releases!!
    //-------------------------------------------------------------------------
    DateTime currentRelease = new DateTime(2020, 8, 30, 00, 40, 00);
    string   currentVersion = "v1.1.0"; 
    //--------------------------------------------------------------------------


    void Start()
    {
        // Start the coroutine that downloads update info, check for updates and repeats
        StartCoroutine(GetRequest(CUBE3D_RSS));
    }

    void CheckNewUpdate()
    {
        // Only run new update check if new content is available
        if (!new_content)
            return;
        else
            new_content = false; // Reset variable

        // Parse the date and time of all releases 
        MatchCollection matches = Regex.Matches(rss_content, @"(?<=<updated>)(.+?)(?=</updated>)");

        // Holds the latest release from the RSS feed, calculated below.
        DateTime latestRelease = DateTime.ParseExact(matches[0].Value, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);

        // Find the newest update time from RSS feed
        for (int i = 0; i < matches.Count - 1; i++)
        {
            DateTime cmp = DateTime.ParseExact(matches[i + 1].Value, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);

            if (DateTime.Compare(latestRelease, cmp) < 0)
                latestRelease = cmp;
        }                        

        // Check wether there is a new release on GitHub
        int result = DateTime.Compare(currentRelease, latestRelease);
        
        if (result < 0)
        {
            Debug.Log("There is a new release on GitHub!");
            string message = "There is a new release available on GitHub!\nInstalled version: " + currentVersion;
            MessageBoxes.MBOXES.Show(message, "YesNo", "Update Available", "Do you want to update?");
        }
        else if (result == 0)
        {
            Debug.Log("You have the newest release installed!");
            //string message = "Release: " + currentVersion + "\n\nYou have the newest release installed!";
            //MessageBoxes.MBOXES.Show(message, "Ok", "Updates");
        }
        else
        {
            // Hopefully shouldn't reach this point (it doesn't make sense)
            Debug.Log("For some reason, your release is newer than the one present on GitHub.");
        }
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
  
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("Error: Couldn't connect to the internet to get update information.");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log("Successfully connected and downloaded update info."); 
                    rss_content = webRequest.downloadHandler.text;
                    new_content = true;
                    break;
            }
        }
        // Check if there is a new update
        CheckNewUpdate();

        // Wait for 10 minutes to run again 
        yield return new WaitForSeconds(600);

        // Start the coroutine again
        StartCoroutine(GetRequest(CUBE3D_RSS));
    }
}
