using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] GameObject _characterImageGO;
    [SerializeField] SpeedGaugeController _gaugeController;
    [SerializeField] TextMeshPro speedMeter;

    [SerializeField] float _movingSpeedMulti;
    [Range(0f,1f)]
    [SerializeField] float _separateGoBackAndGoFowardStance = 0.15f;
    float _innerCenterValue;

    [SerializeField] bool _jumpAble;
    [SerializeField] bool _jumpDisabled;
    [Range(0.15f, 1.0f)]
    [SerializeField] float _jumpDisableTime;
    [SerializeField] float _jumpPower;

    [SerializeField] GameObject _jumpSound;

    Rigidbody2D _characterRB;

    // Start is called before the first frame update
    void Start()
    {
        _characterRB = _characterImageGO.GetComponent<Rigidbody2D>();
        _characterImageGO.transform.position = new Vector2(0f, -3f);
        Initialization();
    }

    void Initialization()
    {
        SetDifficulty();
    }

    void SetDifficulty()
    {
        _separateGoBackAndGoFowardStance = GameSceneManager.GSM.GetLevelSetting(GameSceneManager.GSM.GetHowManyWeLooped()).SeparateBackAndGo;
        _movingSpeedMulti = GameSceneManager.GSM.GetLevelSetting(GameSceneManager.GSM.GetHowManyWeLooped()).MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _innerCenterValue = _separateGoBackAndGoFowardStance * _separateGoBackAndGoFowardStance;

        float tempFloatXPos;
        float tempFloatYPos;
        tempFloatXPos = _gaugeController.SpeedGagueValue * _gaugeController.SpeedGagueValue - _innerCenterValue;
        
        tempFloatXPos = _movingSpeedMulti * tempFloatXPos;

        //speedMeter.text = tempFloatXPos.ToString();

        Vector2 tempVector2CharacterPos = new Vector2(tempFloatXPos, _characterRB.velocity.y);

        _characterRB.velocity = tempVector2CharacterPos;

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(new Vector2(_characterImageGO.transform.position.x, _characterImageGO.transform.position.y + -1.2f), new Vector2(0.5f, 1), 0.0f);

        foreach(Collider2D col2D in collider2Ds)
        {
            if(col2D.gameObject.CompareTag("Platform"))
            {
                _jumpAble = true;
                break;
            }
        }

        if (Input.GetKey(KeyCode.Z) && _jumpAble == true && _jumpDisabled == false)
        {
            _characterRB.AddForce(new Vector2(0f, _jumpPower));
            _jumpAble = false;
            _jumpDisabled = true;
            StartCoroutine(SetJumpDisableToTrue(_jumpDisableTime));
            Instantiate(_jumpSound);
        }

    }

    IEnumerator SetJumpDisableToTrue(float disableTIme)
    {
        yield return new WaitForSeconds(disableTIme);
        _jumpDisabled = false;
    }
}
