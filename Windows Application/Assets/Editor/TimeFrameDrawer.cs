using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimeFrameDrawer : MonoBehaviour
{
    [CustomPropertyDrawer(typeof(Timeframe))]
    public class TimeframeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Split the position into two for hours and minutes
            Rect hoursLabelRect = new Rect(position.x, position.y, 50f, position.height);
            Rect hoursRect = new Rect(position.x + 45f, position.y, 50f, position.height);
            Rect minutesLabelRect = new Rect(position.x + 110f, position.y, 60f, position.height);
            Rect minutesRect = new Rect(position.x + 170f, position.y, 50f, position.height);

            // Get the serialized properties for hours and minutes
            SerializedProperty hoursProperty = property.FindPropertyRelative("hours");
            SerializedProperty minutesProperty = property.FindPropertyRelative("minutes");

            // Draw labels
            EditorGUI.LabelField(hoursLabelRect, "Hour");
            EditorGUI.LabelField(minutesLabelRect, "Minutes");

            // Draw hours popup
            string[] hourOptions = new string[24];
            for (int i = 0; i < 24; i++)
            {
                hourOptions[i] = i.ToString();
            }
            hoursProperty.intValue = EditorGUI.Popup(hoursRect, hoursProperty.intValue, hourOptions);

            // Draw minutes popup
            int selectedMinutes = minutesProperty.intValue;
            string[] minuteOptions = { "0", "15", "30", "45" };
            int[] minuteValues = { 0, 15, 30, 45 };
            for (int i = 0; i < minuteValues.Length; i++)
            {
                if (minuteValues[i] == selectedMinutes)
                {
                    selectedMinutes = EditorGUI.Popup(minutesRect, selectedMinutes / 15, minuteOptions) * 15;
                    break;
                }
            }
            minutesProperty.intValue = selectedMinutes;

            EditorGUI.EndProperty();
        }
    }
}
