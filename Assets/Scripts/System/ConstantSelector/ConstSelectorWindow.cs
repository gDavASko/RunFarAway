using System.Collections.Generic;
using UnityEngine;

namespace KBP.EDITOR
{
    public class ConstSelectorWindow: StringSelectorWindow
    {
        public static void Open(System.Action<string> onClose, string initValue, List<string> values)
        {
            window = GetWindow<ConstSelectorWindow>();
            window.minSize = new Vector2(400, 800);
            window.titleContent.text = "Const selector";
            window.onCloseAction = onClose;
            window.curValue = initValue;
            window.Load(values);
        }
    }
}