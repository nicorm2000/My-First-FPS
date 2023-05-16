using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayersUI : MonoBehaviour
{
    [SerializeField] TMP_Text ammoText;
    [SerializeField] Image reload;

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

    public void ReloadAnim(float currentTimeOfReload, float maxTimeOfReload)
    {
        reload.fillAmount = currentTimeOfReload/maxTimeOfReload;
    }
}
