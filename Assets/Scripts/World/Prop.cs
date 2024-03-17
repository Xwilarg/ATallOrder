using Cinemachine;
using NSFWMiniJam3.Manager;
using NSFWMiniJam3.SO;
using System.Collections;
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
            go.GetComponent<Npc>().SetInfo(HiddenNpc);   
            return go.GetComponent<Npc>();
        }

        public string InteractionKey => "search";

        public void Interact(PlayerController pc)
        {
            if (HiddenNpc != null)
            {
                var npc = SpawnNpc(pc);
                npc.transform.Translate(Vector3.up * .13f);
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
                            npc.DownAnim();
                            if (HiddenNpc.PosOverrides.IsMovedDownWhenFound)
                            {
                                npc.transform.position = new Vector3(transform.position.x, pc.transform.position.y, 0f) + Vector3.down;
                            }
                            DialogueManager.Instance.ShowStory(transform.position, HiddenNpc.OnPlayerWin, () =>
                            {
                                TransitionManager.Instance.StartTransition((CinemachineVirtualCamera _) =>
                                {
                                    GameManager.Instance.MinigameWon++;

                                    SetHide(null);
                                    npc.NakedAnim();

                                    var node = GameManager.Instance.NextSpot;
                                    npc.transform.parent = node.transform;
                                    npc.transform.position = node.transform.position;
                                    npc.GetComponent<SpriteRenderer>().sortingLayerName = "NPC";
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
