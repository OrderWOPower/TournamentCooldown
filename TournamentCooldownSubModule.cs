using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace TournamentCooldown
{
    // This mod adds a cooldown for spawning tournaments to a settlement after a tournament in that settlement is finished.
    public class TournamentCooldownSubModule : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignStarter = (CampaignGameStarter)gameStarter;
                campaignStarter.AddBehavior(new TournamentCooldownBehavior());
                campaignStarter.AddModel(new TournamentCooldownModel());
            }
        }
    }
}
