using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class CheckReleases : MonoBehaviour
{
    // GitHub repository RSS feed info
    const string RELEASES_RSS = "https://github.com/mariugul/cube-3d/releases.atom";

    // The content of the RSS feed from cube-3d repository
    string rss_content = "";

    // Internet Connection
    bool new_content = false;

    // Is an update available online?
    // Null value represents internet connection error (Don't know)
    bool? update_available = false;

    // Send dialog button click to event handler
    public delegate void NewUpdateAvailable(Version version);
    public static event NewUpdateAvailable UpdateAvailable;

    // Current installed version
    Version current_version;

    // Initialize
    //--------------------------------------------------------------------------
    void Start()
    {
        // Set current version number
        current_version = new Version(Application.version);

        // Start the coroutine that downloads update info, check for updates and repeats
        StartCoroutine(GetRequest(RELEASES_RSS, 600));
    }

    // Private Functions
    // --------------------------------------------------------------------------

    // Checks if a new update is available based on the data from GitHub Release RSS
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
        var result = gitHubVersion.CompareTo(current_version);

        // Ask to update if newer release exists
        if (result > 0)
        {
            Debug.Log("There is a new release on GitHub!");

            // Trigger 'Update available' event to event handler
            UpdateAvailable?.Invoke(gitHubVersion);

            // Set global variable to true for update available
            update_available = true;
        }
        else if (result < 0)
        {
            // Hopefully shouldn't reach this point (it doesn't make sense)
            Debug.Log("For some reason, your release is newer than the latest on GitHub.");

            update_available = false;
        }
        else
        {
            Debug.Log("You have the newest release installed!");

            update_available = false;
        }
    }
  
    // Web request for getting GitHub Release RSS data
    IEnumerator GetRequest(string uri, uint repeatInSeconds = 0)
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
                    
                    // Checking for an update was not possible
                    update_available = null;
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);

                    // Checking for an update was not possible
                    update_available = null;
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);

                    // Checking for an update was not possible
                    update_available = null;
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

        // Loop the coroutine if a repeat time is given
        if (repeatInSeconds > 0)
        {
            // Wait for 10 minutes to run again 
            yield return new WaitForSeconds(repeatInSeconds);

            // Start the coroutine again
            StartCoroutine(GetRequest(RELEASES_RSS, repeatInSeconds));
        }
    }

    // Public Functions
    // --------------------------------------------------------------------------

    // Returns the current version number
    public Version GetCurrentVersion()
    {
        return current_version;
    }

    // Returns true if there is an update available
    public bool? IsUpdateAvailable()
    {
        // Start the coroutine that downloads update info and checks for updates
        StartCoroutine(GetRequest(RELEASES_RSS));
        
        // Return true if update is available
        // Null if not determinable (internet connection error)
        return update_available;
    }
}
