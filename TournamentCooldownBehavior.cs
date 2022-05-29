using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TournamentCooldown
{
    public class TournamentCooldownBehavior : CampaignBehaviorBase
    {
        private Dictionary<Settlement, int> _tournamentCooldown = new Dictionary<Settlement, int>();

        public override void RegisterEvents()
        {
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnGameLoaded));
            CampaignEvents.TournamentFinished.AddNonSerializedListener(this, new Action<CharacterObject, MBReadOnlyList<CharacterObject>, Town, ItemObject>(OnTournamentFinished));
            CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(OnDailyTickSettlement));
        }

        public override void SyncData(IDataStore dataStore)
        {
            try
            {
                dataStore.SyncData("_tournamentCooldown", ref _tournamentCooldown);
            }
            catch (Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage(ex.Message + "\r\n" + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("\r\n"))));
            }
        }

        private void OnGameLoaded(CampaignGameStarter campaignGameStarter) => UpdateSettlements();

        // Add a tournament cooldown to the settlement and set it to 10 days.
        private void OnTournamentFinished(CharacterObject winner, MBReadOnlyList<CharacterObject> participants, Town town, ItemObject prize)
        {
            _tournamentCooldown.Add(town.Settlement, 10);
            UpdateSettlements();
        }

        // If a settlement has a tournament cooldown, decrease the tournament cooldown by 1 day.
        // If a settlement's tournament cooldown is 0 days, remove its tournament cooldown.
        private void OnDailyTickSettlement(Settlement settlement)
        {
            if (_tournamentCooldown.ContainsKey(settlement))
            {
                _tournamentCooldown[settlement]--;
                if (_tournamentCooldown[settlement] <= 0)
                {
                    _tournamentCooldown.Remove(settlement);
                }
            }
            UpdateSettlements();
        }

        private void UpdateSettlements() => TournamentCooldownManager.Current.SetSettlements(_tournamentCooldown.Keys.ToList());
    }
}
