using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace KillBanditsRaiseRelations
{
	class PlayerBattleEndEventListener
	{
		private int GroupsOfBandits { get; set; }
		private int RelationshipIncrease { get; set; }
		private int Radius { get; set; }
		private float SizeBonus { get; set; }
		private bool PrisonersOnly { get; set; }
		private int BanditGroupCounter { get; set; }
		private int BanditDeathCounter { get; set; }

		public PlayerBattleEndEventListener(Settings CurrentSettings)
		{
			this.GroupsOfBandits = CurrentSettings.GroupsOfBandits;
			this.RelationshipIncrease = CurrentSettings.RelationshipIncrease;
			this.Radius = CurrentSettings.Radius;
			this.SizeBonus = CurrentSettings.SizeBonus;
			this.PrisonersOnly = CurrentSettings.PrisonersOnly;
			this.BanditGroupCounter = CurrentSettings.GroupsOfBandits;
			this.BanditDeathCounter = 0;

		}

		public void IncreaseLocalRelationsAfterBanditFight(MapEvent m)
		{
			//GetPartyReceivingLootShare method made internal in 1.4.3 beta.  
			TroopRoster rosterReceivingLootShare;
			int mainPartSideInt = (int)PartyBase.MainParty.Side;

			rosterReceivingLootShare = PlayerEncounter.Current.RosterToReceiveLootMembers;


			//PartyBase partyReceivingLootShare = m.GetPartyReceivingLootShare(PartyBase.MainParty);
			MapEventSide banditSide;
			if (m.DefeatedSide == BattleSideEnum.Attacker)
			{
				banditSide = m.AttackerSide;
			}
			else
			{
				banditSide = m.DefenderSide;
			}
			if (!((int)m.DefeatedSide == -1 || (int)m.DefeatedSide == 2))
			{
				if ((m.GetLeaderParty(m.DefeatedSide).MapFaction.IsBanditFaction) && (rosterReceivingLootShare.TotalHealthyCount > 0 || !this.PrisonersOnly))
				{
					BanditDeathCounter += banditSide.Casualties;
					if (this.BanditGroupCounter == 1)
					{
						IncreaseLocalRelations(m);
						this.ResetBanditDeathCounter();
					}
					this.BanditGroupCounterUpdate();
				}
			}
		}

		private void IncreaseLocalRelations(MapEvent m)
		{
			float FinalRelationshipIncrease = this.RelationshipIncrease;
			if (this.SizeBonus != 0)
			{
				FinalRelationshipIncrease = this.RelationshipIncrease * this.BanditDeathCounter * this.SizeBonus;
			}
			int FinalRelationshipIncreaseInt = (int)Math.Floor(FinalRelationshipIncrease);
			FinalRelationshipIncreaseInt = FinalRelationshipIncreaseInt < 1 ? 1 : FinalRelationshipIncreaseInt;

			List<Settlement> list = new List<Settlement>();
			foreach (Settlement settlement in Settlement.All)
			{
				if ((settlement.IsVillage || settlement.IsTown) && settlement.Position2D.DistanceSquared(m.Position) <= this.Radius)
				{
					list.Add(settlement);
				}
			}
			foreach (Settlement settlement2 in list)
			{
				if (settlement2.Notables.Any<Hero>())
				{
					Hero h = settlement2.Notables.GetRandomElement<Hero>();
					ChangeRelationAction.ApplyPlayerRelation(h, relation: FinalRelationshipIncreaseInt, affectRelatives: true, showQuickNotification: false);
				}
			}
			InformationManager.DisplayMessage(new InformationMessage("FinalRelationshipIncrease: " + FinalRelationshipIncreaseInt.ToString(), Color.FromUint(4282569842U)));
			InformationManager.DisplayMessage(new InformationMessage("Your relation increased with nearby notables.", Color.FromUint(4282569842U)));
		}

		private void BanditGroupCounterUpdate()
		{
			this.BanditGroupCounter--;
			if (this.BanditGroupCounter == 0)
			{
				this.BanditGroupCounter = this.GroupsOfBandits;
			}
		}

		private void ResetBanditDeathCounter()
		{
			this.BanditDeathCounter = 0;
		}


	}
}
