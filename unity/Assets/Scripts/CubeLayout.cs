using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CubeLayout : MonoBehaviour
{
    public void ToggleLayoutText()
    {
        //gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void TogglePlaneText()
    {
        gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshPro>().enabled = false;
        /*
        if (planeText.activeSelf) 
            //planeText.SetActive(false);
            MessageBox.Show("IS active");
        else
            //planeText.SetActive(true);
            MessageBox.Show("NOT active"); 
            */
    }   
    
}
