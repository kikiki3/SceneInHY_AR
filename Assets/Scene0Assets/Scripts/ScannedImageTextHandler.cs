using UnityEngine;
using TMPro;

public class ScannedImageTextHandler : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    void Start()
    {
        string imageName = MultipleImageTracker.scannedImageName;

        switch (imageName)
        {
            case "SecretlyGreatly":
                displayText.text = "Secretly Greatly";
                break;
            case "twentyMovPosters":
                displayText.text = "Twenty";
                break;
            case "HotelDelluna":
                displayText.text = "Hotel DelLuna";
                break;
            default:
                displayText.text = "None";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
