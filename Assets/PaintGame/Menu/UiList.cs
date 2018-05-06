using UnityEngine;

namespace PaintGame.Menu
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class UiList : MonoBehaviour
    {
        public UiListItem SelectedItem { get; private set; }

        public void SelectItem(UiListItem item)
        {
            if (SelectedItem != null)
                SelectedItem.MarkAsUnselected();

            SelectedItem = item;

            SelectedItem.MarkAsSelected();
        }

        protected void Update()
        {
            var transform = this.transform as RectTransform;

            if (!transform.hasChanged)
                return;

            transform.offsetMin = new Vector2(transform.offsetMin.x, transform.offsetMax.y);

            for (var i = 0; i < transform.childCount; ++i)
            {
                var element = transform.GetChild(i) as RectTransform;

                transform.offsetMin = transform.offsetMin += new Vector2(0, element.offsetMin.y - element.offsetMax.y);
            }
    
            var elementPosition = new Vector2(0, transform.offsetMax.y - transform.offsetMin.y);

            for (var i = 0; i < transform.childCount; ++i)
            {
                var element = transform.GetChild(i) as RectTransform;

                element.pivot = new Vector2(.5f, 1);

                element.anchorMin = new Vector2(0, 0);
                element.anchorMax = new Vector2(1, 0);
                element.anchoredPosition = elementPosition;
                element.offsetMin = new Vector2(0, element.offsetMin.y);
                element.offsetMax = new Vector2(0, element.offsetMax.y);

                elementPosition -= new Vector2(0, element.rect.height);
            }
        }
    }
}
