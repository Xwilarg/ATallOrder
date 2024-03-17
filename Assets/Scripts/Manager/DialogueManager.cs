using Ink.Runtime;
using NSFWMiniJam3.Dialogue;
using NSFWMiniJam3.World;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NSFWMiniJam3.Manager
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { private set; get; }

        [SerializeField]
        private GameObject _container;

        [SerializeField]
        private GameObject _hSceneContainer;

        [SerializeField]
        private TextDisplay _display, _vnDisplay;

        [SerializeField]
        private Transform _choiceContainer;

        [SerializeField]
        private GameObject _choicePrefab;

        private Story _story;

        private Action _onDone;

        public bool IsPlayingStory => _container.activeInHierarchy;

        public TextDisplay Display => _hSceneContainer.activeInHierarchy ? _vnDisplay : _display;

        private void Awake()
        {
            Instance = this;

            _display.OnDisplayDone += (_sender, _e) =>
            {
                if (_story.currentChoices.Any())
                {
                    ResetVN();
                    foreach (var choice in _story.currentChoices)
                    {
                        var button = Instantiate(_choicePrefab, _choiceContainer);
                        button.GetComponentInChildren<TMP_Text>().text = choice.text;

                        var elem = choice;
                        button.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            _story.ChoosePath(elem.targetPath);
                            for (int i = 0; i < _choiceContainer.childCount; i++)
                                Destroy(_choiceContainer.GetChild(i).gameObject);
                            DisplayStory(_story.Continue());
                        });
                    }
                }
            };
        }

        private void ResetVN()
        {
            _container.SetActive(true);
            _choiceContainer.gameObject.SetActive(true);
        }

        public void ShowStory(Vector2 refPos, TextAsset asset, Action onDone = null)
        {
            Debug.Log($"[STORY] Playing {asset.name}");

            _hSceneContainer.SetActive(false);
            _container.transform.position = new(refPos.x - 2f, refPos.y + 4f);
            _onDone = onDone;
            _story = new(asset.text);
            ResetVN();
            DisplayStory(_story.Continue());
        }

        private void DisplayStory(string text)
        {
            _container.SetActive(true);

            foreach (var tag in _story.currentTags)
            {
                var s = tag.Split(' ');
                var content = string.Join(' ', s.Skip(1)).ToUpperInvariant();
                switch (s[0])
                {
                    case "speaker": break;

                    case "spe":
                        NpcIntro.Instance.Spe();
                        break;

                    case "cg":
                        _hSceneContainer.SetActive(content == "ENABLE");
                        break;

                    default:
                        Debug.LogError($"Unknown story key: {s[0]}");
                        break;
                }
            }
            Display.ToDisplay = text;
        }

        public void DisplayNextDialogue()
        {
            if (!_container.activeInHierarchy)
            {
                return;
            }
            if (!Display.IsDisplayDone)
            {
                // We are slowly displaying a text, force the whole display
                Display.ForceDisplay();
            }
            else if (_story.canContinue && // There is text left to write
                !_story.currentChoices.Any()) // We are not currently in a choice
            {
                DisplayStory(_story.Continue());
            }
            else if (!_story.canContinue && !_story.currentChoices.Any())
            {
                _container.SetActive(false);
                _onDone?.Invoke();
            }
        }
    }
}