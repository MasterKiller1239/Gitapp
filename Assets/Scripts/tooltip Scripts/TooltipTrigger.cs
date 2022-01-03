using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Multiline()]
    public string content;
    public string header;
    private static readonly LTDescr delay;
    public bool active = true;
   public void OnPointerEnter(PointerEventData eventData)
    {
        if(active)
        LeanTween.delayedCall(0.1f, () =>
        {
            TooltipSystem.Show(content, header);
        }
            );
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }
    void OnMouseOver()
    {
        if (active)
            TooltipSystem.Show(content, header);

    }
        void OnMouseExit()
    {
        TooltipSystem.Hide();
    }

}
