using HK;
using R3;
using TMPro;
using UnityEngine.Assertions;
using WOS.ActorControllers;
using WOS.ActorControllers.Abilities;

namespace WOS.UIViews
{
    public sealed class UIViewHUD
    {
        private readonly HKUIDocument document;

        public UIViewHUD(HKUIDocument document)
        {
            this.document = document;
        }

        public void Activate(Actor player)
        {
            if (!player.TryGetAbility<ActorInventory>(out var inventory))
            {
                Assert.IsNotNull(inventory, $"{nameof(ActorInventory)} not found for player {player.name}.");
            }

            inventory.MoneyAsObservable()
                .Subscribe(this, static (x, @this) =>
                {
                    @this.document.Q<TMP_Text>("Text.Money").text = x.ToString();
                })
                .RegisterTo(document.destroyCancellationToken);
        }
    }
}
