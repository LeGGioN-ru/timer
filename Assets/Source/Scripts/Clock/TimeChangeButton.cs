using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimeChangeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_InputField _inputField;

    public bool IsPressed { get; private set; }

    private TimeModel _timeModel;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(TimeModel timeModel, SignalBus signalBus)
    {
        _timeModel = timeModel;
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        ChangeMode();
    }

    private void ChangeMode()
    {
        if (IsPressed == false)
        {
            _buttonText.text = "Сохранить";
            _inputField.text = _timeText.text;
        }
        else
        {
            _buttonText.text = "Изменить";
            TryChangeTime();
        }

        IsPressed = !IsPressed;

        _timeText.gameObject.SetActive(!IsPressed);
        _inputField.gameObject.SetActive(IsPressed);
    }

    private void TryChangeTime()
    {
        string[] timeParts = _inputField.text.Split(':');
        if (timeParts.Length == 3 &&
            int.TryParse(timeParts[0], out int hour) &&
            int.TryParse(timeParts[1], out int minute) &&
            int.TryParse(timeParts[2], out int second) &&
            hour >= 0 && hour < 24 &&
            minute >= 0 && minute < 60 &&
            second >= 0 && second < 60)
        {
            DateTime newTime = new DateTime(_timeModel.Time.Year, _timeModel.Time.Month, _timeModel.Time.Day, hour, minute, second);
            _timeModel.ChangeTime(newTime);
            _signalBus.Fire(new TimeChanged(newTime));
        }
        else
        {
            Debug.LogError("Invalid time input");
        }
    }
}
