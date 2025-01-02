using UnityEngine;

namespace GameFrameX.Editor
{
    public partial class PackageManagerWindow
    {
        private GUIStyle _labelGreenStyle;

        private GUIStyle LabelGreenStyle
        {
            get
            {
                if (_labelGreenStyle == null)
                {
                    _labelGreenStyle = new GUIStyle(GUI.skin.label)
                    {
                        normal =
                        {
                            textColor = Color.green, // 设置文本颜色为绿色
                        },
                        alignment = TextAnchor.MiddleCenter,
                    };
                }

                return _labelGreenStyle;
            }
        }
    }
}