using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Settlements;

namespace TournamentCooldown
{
    public class TournamentCooldownModel : DefaultTournamentModel
    {
        // If a settlement has a tournament cooldown, stop the settlement from spawning a tournament.
        public override float GetTournamentStartChance(Town town)
        {
            if (TournamentCooldownManager.Current.Settlements.Contains(town.Settlement))
            {
                return 0f;
            }
            return base.GetTournamentStartChance(town);
        }
    }
}
