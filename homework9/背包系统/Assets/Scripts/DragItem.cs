using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform myTransform;
    private RectTransform myRectTransform;
    private CanvasGroup canvasGroup;
    public Vector3 originalPosition;
    private GameObject lastEnter = null;
    private Color lastEnterNormalColor;
    private Color highLightColor = Color.cyan;
    void Start()
    {
        myTransform = this.transform;
        myRectTransform = this.transform as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = myTransform.position;
    }
    void Update()
    {
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        lastEnter = eventData.pointerEnter;
        lastEnterNormalColor = lastEnter.GetComponent<Image>().color;
        originalPosition = myTransform.position;
        gameObject.transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            myRectTransform.position = globalMousePos;
        }
        GameObject curEnter = eventData.pointerEnter;
        bool inItemGrid = EnterItemGrid(curEnter);
        if (inItemGrid)
        {
            Image img = curEnter.GetComponent<Image>();
            lastEnter.GetComponent<Image>().color = lastEnterNormalColor;
            if (lastEnter != curEnter)
            {
                lastEnter.GetComponent<Image>().color = lastEnterNormalColor;
                lastEnter = curEnter;//记录当前物品格子以供下一帧调用
            }
            img.color = highLightColor;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject curEnter = eventData.pointerEnter;
        //拖拽到的空区域中（如包裹外），恢复原位
        if (curEnter == null)
        {
            myTransform.position = originalPosition;
        }
        else
        {
            if (curEnter.name == "UI_ItemGrid")
            {
                myTransform.position = curEnter.transform.position;
                originalPosition = myTransform.position;
                curEnter.GetComponent<Image>().color = lastEnterNormalColor;
            }
            else
            {
                if (curEnter.name == eventData.pointerDrag.name && curEnter != eventData.pointerDrag)
                {
                    Vector3 targetPostion = curEnter.transform.position;
                    curEnter.transform.position = originalPosition;
                    myTransform.position = targetPostion;
                    originalPosition = myTransform.position;
                }
                else
                {
                    myTransform.position = originalPosition;
                }
            }
        }
        lastEnter.GetComponent<Image>().color = lastEnterNormalColor;
        canvasGroup.blocksRaycasts = true;
    }

    bool EnterItemGrid(GameObject go)
    {
        if (go == null)
        {
            return false;
        }
        return go.name == "UI_ItemGrid";
    }

}