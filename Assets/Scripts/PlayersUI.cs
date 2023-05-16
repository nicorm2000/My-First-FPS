using UnityEngine;
using TMPro;

public class PlayersUI : MonoBehaviour
{
    [SerializeField] TMP_Text ammoText;

    public void UpdateAmmoText(int currentAmmo)
    {
        if (currentAmmo == -1)
        {
            ammoText.text = "";
        }
        else
        {
            ammoText.text = currentAmmo.ToString();
        }
    }
}
