using KBP.CORE;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KBP.EDITOR
{
    public class EnumSelectorWindow<T>: SearchableEditorWindow where T : System.Enum
    {
        protected static EnumSelectorWindow<T> window = null;
        private Vector2 scrollPos = new Vector2();
        private static string filter = "";
        private int countOnPage = 25;
        public System.Action<T> onCloseAction = null;

        private Dictionary<T, string> numberMessages = new Dictionary<T, string>();

        public void Load()
        {
            foreach(T msgNumber in System.Enum.GetValues(typeof(T)))
            {
                numberMessages[msgNumber] = msgNumber.ToString().ToLower();
            }
        }

        private int currentPage = 0;
        public T curEnumValue;

        private void OnGUI()
        {
            int heightPlus = 24;

            int y = 4 + 24;
            int x = 4 + 36;
            int height = 16;
            int width = 400;
            int offset = 4;
            Rect rect = new Rect(4, 4, width, 16);

            rect.width /= 2;

            rect.y += 16;
            rect.x = 4;

            EditorGUI.LabelField(rect, "Search:");
            rect.x += 92;
            filter = EditorGUI.TextField(rect, filter).ToLower();
            y += 16;

            List<T> items = new List<T>();
            foreach(var item in numberMessages)
            {
                if(!item.Value.Contains(filter))
                {
                    continue;
                }

                items.Add(item.Key);
            }

            currentPage = DrawPages(4, y, 28, 20, currentPage, countOnPage, items);
            y += 12;
            scrollPos = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height), scrollPos,
                new Rect(0, 0, width, countOnPage * 1.1f * heightPlus));
            BeginWindows();

            height += heightPlus / 2;
            height += 8;
            Table(x, y, offset, width, height, heightPlus, currentPage, countOnPage, items);

            y += height + (Mathf.Min(countOnPage, items.Count) + 2) * (heightPlus);
            currentPage = DrawPages(4, y, 28, 20, currentPage, countOnPage, items);

            EndWindows();
            GUI.EndScrollView();
        }

        private int DrawPages(int x, int y, int width, int height, int currentPage, int countOnPage, List<T> items)
        {
            var pageRect = new Rect(x, y, width, height);
            for(int i = 0; i < (items.Count / countOnPage) + 1; i++)
            {
                GUI.skin.GetStyle("Button").richText = true;
                if(GUI.Button(pageRect, i.ToString().Color(i == currentPage ? Color.yellow : Color.white)))
                {
                    currentPage = i;
                }

                pageRect.x += width;
            }

            y += 24;
            return currentPage;
        }

        private void Table(float x, float y, float offset, float width, float height, float heightPlus, int page,
            int count, List<T> items)
        {
            Rect rect = new Rect(4, y + height, heightPlus - 2, heightPlus - 2);

            rect.x = x;
            rect.width = width;

            for(int i = (items.Count - 1) - (page * count);
                i >= Mathf.Max((items.Count - 1) - (page * count) - count, 0);
                i--)
            {
                rect.y = y + height;

                T item = items[i];

                if(!item.ToString().ToLower().Contains(filter))
                {
                    continue;
                }

                object val = System.Convert.ChangeType(item, item.GetTypeCode());
                int intVal = (int)val;
                if(GUI.Button(rect, item.ToString() + (" [" + intVal + "]").Color(Color.grey)))
                {
                    curEnumValue = item;
                    Close();
                }

                height += heightPlus;
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();

            onCloseAction?.Invoke(curEnumValue);
            onCloseAction = null;
        }
    }
}