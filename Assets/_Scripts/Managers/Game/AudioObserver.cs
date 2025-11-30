using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioObserver : MonoBehaviour, IObserver<object>
{
    #region --- Nestest class ---

    [System.Serializable]
    public class AudioSourceDictionary : SerializableDictionary<EAudioKey, AudioSource> { }

    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        #region --- Methods ---

        public TValue GetValueByKey(TKey k) => values[keys.IndexOf(k)];

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in this)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            int count = Mathf.Min(keys.Count, values.Count);

            for (int i = 0; i < count; i++)
                this[keys[i]] = values[i];
        }

        #endregion
    }

    [CustomPropertyDrawer(typeof(SerializableDictionary<,>), true)]
    public class SerializableDictionaryPropertyDrawer : PropertyDrawer
    {
        private const float lineHeight = 20f;
        private const float padding = 3f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty keys = property.FindPropertyRelative("keys");
            return (keys.arraySize + 2) * (lineHeight + padding);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty keys = property.FindPropertyRelative("keys");
            SerializedProperty values = property.FindPropertyRelative("values");

            position.height = lineHeight;

            // Label
            EditorGUI.LabelField(position, label);
            position.y += lineHeight + padding;

            // Rows
            for (int i = 0; i < keys.arraySize; i++)
            {
                float half = position.width / 2;

                Rect keyRect = new Rect(position.x, position.y, half - 10, lineHeight);
                Rect valueRect = new Rect(position.x + half, position.y, half - 10, lineHeight);

                EditorGUI.PropertyField(keyRect, keys.GetArrayElementAtIndex(i), GUIContent.none);
                EditorGUI.PropertyField(valueRect, values.GetArrayElementAtIndex(i), GUIContent.none);

                position.y += lineHeight + padding;
            }

            // Buttons
            Rect addRect = new Rect(position.x, position.y, 80, lineHeight);
            Rect removeRect = new Rect(position.x + 90, position.y, 80, lineHeight);

            if (GUI.Button(addRect, "Add"))
            {
                keys.InsertArrayElementAtIndex(keys.arraySize);
                values.InsertArrayElementAtIndex(values.arraySize);
            }

            if (keys.arraySize > 0 && GUI.Button(removeRect, "Remove"))
            {
                keys.DeleteArrayElementAtIndex(keys.arraySize - 1);
                values.DeleteArrayElementAtIndex(values.arraySize - 1);
            }

            EditorGUI.EndProperty();
        }
    }

    #endregion

    #region --- Overrides ---

    public void OnNotify(object data)
    {
        if(data is EAudioKey d)
        {
            source.GetValueByKey(d).Play();
        }
    }

    #endregion

    #region --- Unity methods ---

    private void Awake()
    {
        if(_isConnectingtoAudioSubject)
            GameManager.Instance.GameSubject.AddObserver(EEventKey.Audio, this);
    }

    #endregion

    #region --- Methods ---

    public void OnInit(Subject<EEventKey, object> subject)
    {
        subject.AddObserver(EEventKey.Audio, this);
    }

    #endregion

    #region --- Fields ---

    [Header("Auto connecting to AudioSubject")]
    [SerializeField] private bool _isConnectingtoAudioSubject = true;

    [Header("Audio sources")]
    [SerializeField] private AudioSourceDictionary source;

    #endregion
}