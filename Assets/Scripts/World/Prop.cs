using Cinemachine;
using NSFWMiniJam3.Manager;
using NSFWMiniJam3.SO;
using UnityEngine;
using UnityEngine.Assertions;

namespace NSFWMiniJam3.World
{
    public class Prop : MonoBehaviour, IInteractable, IRoomOwned
    {
        public NpcInfo HiddenNpc { private set; get; }
        public Room ParentRoom { set; private get; }

        [SerializeField]
        private Sprite _free, _busy;

        private SpriteRenderer _sr;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        public void SetHide(NpcInfo info)
        {
            HiddenNpc = info;
            _sr.sprite = info == null ? _free : _busy;
        }

        public Npc SpawnNpc(PlayerController pc)
        {
            Assert.IsNotNull(HiddenNpc);

            var go = Instantiate(AssetManager.Instance.NpcPrefab, new(transform.position.x, pc.transform.position.y, 0f), Quaternion.identity);
            go.GetComponent<SpriteRenderer>().sprite = HiddenNpc.GameSprite;
            return go.GetComponent<Npc>();
        }

        public string InteractionKey => "search";

        public void Interact(PlayerController pc)
        {
            if (HiddenNpc != null)
            {
                var npc = SpawnNpc(pc);
                DialogueManager.Instance.ShowStory(transform.position, HiddenNpc.Intro, () =>
                {
                    MinigameManager.Instance.Play(HiddenNpc,
                        onLoose: () =>
                        {
                            DialogueManager.Instance.ShowStory(transform.position, HiddenNpc.OnPlayerLoose, () =>
                            {
                                Destroy(npc.gameObject);
                                ParentRoom.FailHideCheck(pc);
                            });
                        },
                        onWin: () =>
                        {
                            npc.NakedAnim();
                            DialogueManager.Instance.ShowStory(transform.position, HiddenNpc.OnPlayerWin, () =>
                            {
                                TransitionManager.Instance.StartTransition((CinemachineVirtualCamera _) =>
                                {
                                    SetHide(null);
                                    Destroy(npc.gameObject);
                                });
                            });
                        });
                });
            }
            else
            {
                ParentRoom.FailHideCheck(pc);
                DialogueManager.Instance.ShowStory(transform.position, AssetManager.Instance.PropEmpty, null);
            }
        }
    }
}
