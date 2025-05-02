using UnityEngine;
using UnityEngine.EventSystems;
public class OnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private enum ButtonState
    {
        Idle,
        Grow,
        Shrink,
    }

    private ButtonState state;
    private float increment = 0;
    private Vector3 expandSize = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector3 shrinkSize = new Vector3(1f, 1f, 1f);
    [SerializeField] private float timer = 0.5f;

    void Start() => state = ButtonState.Idle;
    void FixedUpdate()
    {
        if (state == ButtonState.Idle)
        {
            if (increment != 0)
                increment = 0;
        }
        else if (state == ButtonState.Grow)
        {
            increment += Time.deltaTime;
            transform.localScale = Vector3.Lerp(shrinkSize, expandSize, increment / timer);
        }
        else if (state == ButtonState.Shrink)
        {
            increment += Time.deltaTime;
            transform.localScale = Vector3.Lerp(expandSize, shrinkSize, increment / timer);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        increment = 0;
        state = ButtonState.Grow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        increment = 0;
        state = ButtonState.Shrink;
    }
}