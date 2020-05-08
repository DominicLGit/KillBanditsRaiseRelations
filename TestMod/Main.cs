using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using ModLib;


namespace KillBanditsRaiseRelations
{
    public class Main : MBSubModuleBase
	{

		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			try
			{
				FileDatabase.Initialise("KillBanditsRaiseRelations");
				KBRRModLibSettings settings = FileDatabase.Get<KBRRModLibSettings>(KBRRModLibSettings.InstanceID);
				if (settings == null) settings = new KBRRModLibSettings();
			}
			catch(Exception ex)
			{
				InformationManager.DisplayMessage(new InformationMessage("Could not Initialise ModLib for KillBanditsRaiseRelations: " + ex.Message.ToString(), Color.FromUint(4282569842U)));
			}
			
		}

		public override void OnGameLoaded(Game game, object initializerObject)
		{
			try
			{
				PlayerBattleEndEventListener playerBattleEndEventListener = new PlayerBattleEndEventListener();
				CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(playerBattleEndEventListener, new Action<MapEvent>(playerBattleEndEventListener.IncreaseLocalRelationsAfterBanditFight));
			}
			catch (Exception ex)
			{
				InformationManager.DisplayMessage(new InformationMessage("An error has occurred during OnGameLoaded for KillBanditsRaiseRelations: " + ex.Message.ToString(), Color.FromUint(4282569842U)));
			}
		}
	}
}
