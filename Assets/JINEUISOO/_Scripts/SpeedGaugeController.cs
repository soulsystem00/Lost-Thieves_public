using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedGaugeController : MonoBehaviour
{
    [SerializeField] float _addSpeedGaugePowerMulty;
    [SerializeField] float _minusGaugePowerMulty;
    [SerializeField] float _speedGagueValue;
    [Range (0.5f, 1.0f)]
    [SerializeField] float _speedGaugeSecondValue = 0.65f;
    internal float SpeedGagueValue => _speedGagueValue;

    [SerializeField] Image _gaugeMovingImage;

    [SerializeField] GameObject _spaceBarSound;

    bool _keyDown, _keyUp;

    // 구간별로 Speed Gague 감소량이 달라지면 좋겠다. 낮을 때는 빨리 차고, 높을때는 느리게 차고.
    // 이는 간단하게, 높을때는 덜차거나, 혹은 빨리 감소하도록 해두면 좋겠다.

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _keyUp = true;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
            _keyDown = true;

        // 키 한번에 한번만 충전되어야 한다.
        if (_keyDown == true && _keyUp == true)
        {
            SetSpeedGaugeValue();
            _keyDown = false;
            _keyUp = false;
            Instantiate(_spaceBarSound);
        }

        _speedGagueValue -= 1 * _minusGaugePowerMulty;
        UpdateSpeedGauge();
    }

    void UpdateSpeedGauge()
    {
        _speedGagueValue = Mathf.Clamp(_speedGagueValue, 0f, 1f);
        _gaugeMovingImage.fillAmount = _speedGagueValue;
    }

    void SetSpeedGaugeValue()
    {
        
        if(_speedGagueValue > 0.7f)
        _speedGagueValue += _speedGaugeSecondValue * _addSpeedGaugePowerMulty;
        if(_speedGagueValue < 0.7f)
        _speedGagueValue += 1f * _addSpeedGaugePowerMulty;

    }
}
