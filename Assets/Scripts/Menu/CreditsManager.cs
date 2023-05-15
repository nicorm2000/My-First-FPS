using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class CreditsManager : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] private GameObject mainMenu;

        [Header("Buttons")]
        [SerializeField] private Button backButton;
        [SerializeField] private Button menuCreditsButton;

        void Start()
        {
            backButton.Select();
        }

        public void OnBackButtonClick()
        {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
            menuCreditsButton.Select();
        }
    }
}
