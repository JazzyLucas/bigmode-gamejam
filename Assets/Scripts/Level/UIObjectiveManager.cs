using BigModeGameJam.Level.Manager;
using System.Collections.Generic;
using UnityEngine;
using BigModeGameJam.Core;
using TMPro;

namespace BigModeGameJam.Level
{
    public class UIObjectiveManager : MonoBehaviour
    {
        [SerializeField] GameObject background;
        [SerializeField] GameObject listenerText;
        [SerializeField] GameObject objectiveText;
        [SerializeField] Transform objectiveTextStartTransform;

        List<TextMeshProUGUI> objectiveMessagesRefs = new List<TextMeshProUGUI>();

        void Awake()
        {
            background.SetActive(false);
        }

        void Start()
        {
            LevelManager.UIObjectiveManager = this;
        }


        public void CreateNewObjectiveDisplay(string listenerMessage, string[] objectiveMessages)
        {
            background.SetActive(true);
            listenerText.GetComponent<TextMeshProUGUI>().text = listenerMessage;
            GameObject lastText = null;
            float lastTransformValue = 0;

            foreach (string message in objectiveMessages)
            {
                float lastTextYAdjust() => lastText != null ? objectiveTextStartTransform.position.y - lastTransformValue : objectiveTextStartTransform.position.y;
                Vector3 placeToInstantiate = new Vector3(objectiveTextStartTransform.position.x, lastTextYAdjust(), objectiveTextStartTransform.position.z);

                GameObject newText = Instantiate(objectiveText, placeToInstantiate, objectiveTextStartTransform.rotation, background.transform);
                newText.GetComponent<TextMeshProUGUI>().text = message;
                lastText = newText;
                objectiveMessagesRefs.Add(newText.GetComponent<TextMeshProUGUI>());
                lastTransformValue += 35;
            }
        }

        public void OnDisplayUpdate(string messageToUpdate)
        {
            foreach (TextMeshProUGUI textMsg in objectiveMessagesRefs)
            {
                if (textMsg.text != messageToUpdate)
                    continue;

                textMsg.text = "<s>" + textMsg.text + "</s>";
            }
        }

        public void OnAllObjectiveFinish()
        {
            int size = objectiveMessagesRefs.Count;

            for (int i = 0; i < size; i++)
            {
                Destroy(objectiveMessagesRefs[i].gameObject);
            }

            objectiveMessagesRefs.Clear();
            background.SetActive(false);
        }
    }
}
