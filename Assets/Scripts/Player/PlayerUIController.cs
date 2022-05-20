using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private PlayerStatsData _playerStatsData;
    
    [SerializeField]
    private Slider _reloadSlider;

    [SerializeField]
    private GameObject _interactPanel;
    
    private void Start()
    {
        _reloadSlider.gameObject.SetActive(false);
        _interactPanel.SetActive(false);
    }

    public void UpdateReloadUI()
    {
        if (_playerStatsData.CurrentWeapon.GetCurrentReloadTime() <= 0 ||
            _playerStatsData.CurrentWeapon.GetCurrentReloadTime() >=
            _playerStatsData.CurrentWeapon.GetData().ReloadTime)
        {
            _reloadSlider.gameObject.SetActive(false);
        }
        else
        {
            _reloadSlider.gameObject.SetActive(true);
        }

        _reloadSlider.value = _playerStatsData.CurrentWeapon.GetCurrentReloadTime() /
                              _playerStatsData.CurrentWeapon.GetData().ReloadTime;
    }
    
    public void ShowInteractPanel(bool set)
    {
        _interactPanel.SetActive(set);
    }
}