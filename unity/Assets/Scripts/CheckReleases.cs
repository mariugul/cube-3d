using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class CheckReleases : MonoBehaviour
{
    // GitHub repository RSS feed info
    const string CUBE3D_RSS = "https://github.com/mariugul/cube-3d/releases.atom";

    // The content of the RSS feed from cube-3d repository
    string rss_content = "";

    // Internet Connection
    bool new_content = false;

    // BUMP
    //-------------------------------------------------------------------------
    // Version of the current install. NB! Bump this on new releases!!
    //-------------------------------------------------------------------------
    Version currentVersion = new Version("1.1.1"); 
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

        // Find all version number tags
        MatchCollection versionMatches = Regex.Matches(rss_content, @"(?<=tag\/)v?(.+?)(?=""/)");

        // Semantic version number
        var gitHubVersion = new Version("0.0.0");

        // Find the newest version tag
        foreach (Match match in versionMatches)
        {
            // Extract individual semantic versioning numbers
            MatchCollection semnums = Regex.Matches(match.Value, @"\d+");

            // Version to compare to the latest version
            string semver = semnums[0].Value + '.' + semnums[1].Value + '.' +semnums[2].Value;
            var version = new Version(semver);

            // Compare to find the latest version on github
            var _result = gitHubVersion.CompareTo(version);
          
            if (_result < 0)
                gitHubVersion = version;
        }

        // Compare the version from GitHub to the installed version
        var result = gitHubVersion.CompareTo(currentVersion);

        // Ask to update if newer release exists
        if (result > 0)
        {
            Debug.Log("There is a new release on GitHub!");
            string message = "There is a new release available on GitHub!" +
                             "\n\nInstalled version: " + currentVersion      +
                             "\nAvailable version: " + gitHubVersion;
            MessageBoxes.MBOXES.Show(message, "YesNo", "Update Available", "Do you want to update?");

        }
        else if (result < 0)
            // Hopefully shouldn't reach this point (it doesn't make sense)
            Debug.Log("For some reason, your release is newer than the latest on GitHub.");
        
        else
            Debug.Log("You have the newest release installed!");
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
