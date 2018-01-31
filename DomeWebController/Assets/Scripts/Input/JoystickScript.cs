using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickScript : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    [Header("Stick Parameters")]
    [SerializeField]
    private float DeltaOffsetX = 3;
    [SerializeField]
    private float DeltaOffsetY = 3;

    private static Image BgImg;
    private static Image JoystickImg;
    public static Vector3 InputVector { private set; get; }

    public static UnityVector2Event OnChangeJoystick = new UnityVector2Event();


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(BgImg.rectTransform,
            eventData.position,
            eventData.pressEventCamera, out pos))
        {

            pos.x = pos.x / BgImg.rectTransform.sizeDelta.x;
            pos.y = pos.y / BgImg.rectTransform.sizeDelta.y;
            InputVector = new Vector3((pos.x), 0, (pos.y));

            InputVector = InputVector.magnitude > 1 ? InputVector.normalized : InputVector;

            JoystickImg.rectTransform.anchoredPosition =
                new Vector3(InputVector.x * (BgImg.rectTransform.sizeDelta.x / DeltaOffsetX),
                InputVector.z * (BgImg.rectTransform.sizeDelta.y / DeltaOffsetY));


            OnChangeJoystick.Invoke(new Vector2(InputVector.x, InputVector.z));



        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputVector = Vector3.zero;
        JoystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }



    // Use this for initialization
    void Start()
    {
        BgImg = transform.GetChild(0).GetComponent<Image>();
        JoystickImg = BgImg.transform.GetChild(0).GetComponent<Image>();
    }

}
