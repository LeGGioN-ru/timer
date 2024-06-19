using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class ClockView : MonoBehaviour
{
    [SerializeField] private RectTransform _hourHand;
    [SerializeField] private RectTransform _minuteHand;
    [SerializeField] private RectTransform _secondHand;
    [SerializeField] private TMP_Text _timeText;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<TimeChanged>(UpdateClock);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<TimeChanged>(UpdateClock);
    }

    private float CalculateShortestAngle(float from, float to)
    {
        float difference = to - from;
        while (difference < -180) difference += 360;
        while (difference > 180) difference -= 360;
        return difference;
    }

    public void UpdateClock(TimeChanged timeChangedEvent)
    {
        DateTime time = timeChangedEvent.Time;
        float hour = time.Hour % AppConstants.HalfHoursInDay + time.Minute / AppConstants.MinutesInHour;
        float minute = time.Minute + time.Second / AppConstants.SecondsInMinute;
        float second = time.Second + time.Millisecond / AppConstants.MillisecondsInSecond;

        float fullCircleDegree = 360f;
        float degreeHour = fullCircleDegree / AppConstants.HalfHoursInDay;
        float degreeMinute = fullCircleDegree / AppConstants.MinutesInHour;
        float degreeSecond = degreeMinute;

        float startAngle = 90;
        float hourAngle = startAngle - hour * degreeHour; 
        float minuteAngle = startAngle - minute * degreeMinute;
        float secondAngle = startAngle - second * degreeSecond;

        float currentHourAngle = _hourHand.localRotation.eulerAngles.z;
        float currentMinuteAngle = _minuteHand.localRotation.eulerAngles.z;
        float currentSecondAngle = _secondHand.localRotation.eulerAngles.z;

        _hourHand.DORotate(new Vector3(0, 0, currentHourAngle + CalculateShortestAngle(currentHourAngle, hourAngle)), 1f).SetEase(Ease.OutCubic);
        _minuteHand.DORotate(new Vector3(0, 0, currentMinuteAngle + CalculateShortestAngle(currentMinuteAngle, minuteAngle)), 1f).SetEase(Ease.OutCubic);
        _secondHand.DORotate(new Vector3(0, 0, currentSecondAngle + CalculateShortestAngle(currentSecondAngle, secondAngle)), 1f).SetEase(Ease.OutCubic);

        _timeText.text = time.ToString("HH:mm:ss");
    }
}
