using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;

namespace TournamentCooldown
{
    public class TournamentCooldownManager
    {
        private static readonly TournamentCooldownManager _tournamentCooldownManager = new TournamentCooldownManager();

        public static TournamentCooldownManager Current => _tournamentCooldownManager;

        public List<Settlement> Settlements { get; set; } = new List<Settlement>();

        public void SetSettlements(List<Settlement> settlements) => Settlements = settlements;
    }
}
