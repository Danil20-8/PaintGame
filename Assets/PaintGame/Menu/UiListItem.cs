using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace PaintGame.Menu
{
    [RequireComponent(typeof(Image))]
    public class UiListItem : MonoBehaviour, IPointerClickHandler
    {
        public void MarkAsSelected()
        {
            image.color = selectedColor;
        }

        public void MarkAsUnselected()
        {
            image.color = unselectedColor;
        }

        [SerializeField]
        Color selectedColor;
        Color unselectedColor;
        protected Image image;

        protected void Awake()
        {
            image = GetComponent<Image>();
            unselectedColor = image.color;
        }

        protected void OnMouseDown()
        {
            var list = GetComponentInParent<UiList>();

            if (list == null)
                return;

            list.SelectItem(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnMouseDown();
        }
    }
}
