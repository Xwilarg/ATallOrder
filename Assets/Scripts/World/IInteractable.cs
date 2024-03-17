namespace NSFWMiniJam3.World
{
    public interface IInteractable
    {
        void Interact(PlayerController pc);

        public string InteractionKey { get; }
        public bool CanInteract {  get; }
    }
}
