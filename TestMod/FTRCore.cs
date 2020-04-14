using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace FightingTogetherRelationship
{
	// Token: 0x02000002 RID: 2
	public class FTRCore : MBSubModuleBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();
			if (!this.loaded)
			{
				InformationManager.DisplayMessage(new InformationMessage("Successfully loaded 'Fighting Together Relationship'.", Color.FromUint(4282569842U)));
				this.loaded = true;
				
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002080 File Offset: 0x00000280
		public override void OnGameLoaded(Game game, object initializerObject)
		{
			base.OnGameLoaded(game, initializerObject);
			try
			{
				CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(this, new Action<MapEvent>(this.CalculateRelation));
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error has occurred whilst initializing 'Fighting Together Relationship Mod':\n\n" + ex.Message + "\n\n" + ex.StackTrace);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E4 File Offset: 0x000002E4
		private void CalculateRelation(MapEvent m)
		{
			List<PartyBase> p = new List<PartyBase>();
			if (Hero.MainHero != null && (m.IsFieldBattle || m.IsSiege || m.IsSiegeOutside) && (m.PlayerSide.ToString().Equals("Defender") || m.PlayerSide.ToString().Equals("Attacker")))
			{
				MapEventSide playerEventSide = this.GetPlayerEventSide(m);
				MapEventSide enemyEventSide = this.GetEnemyEventSide(m);
				int totalMen = this.GetTotalMen(playerEventSide);
				int totalMen2 = this.GetTotalMen(enemyEventSide);
				float contributionRate = this.GetContributionRate(playerEventSide);
				p = this.GetHeroes(playerEventSide);
				int casualties = this.GetCasualties(playerEventSide);
				int casualties2 = this.GetCasualties(enemyEventSide);
				if (totalMen + totalMen2 >= 80 && totalMen2 >= 50)
				{
					if (playerEventSide.Equals(m.Winner))
					{
						if (totalMen >= totalMen2)
						{
							if ((double)(totalMen / totalMen2) <= 1.2 && totalMen - totalMen2 <= 80)
							{
								if (casualties2 > casualties)
								{
									int r = (int)Math.Round(2.0 + (double)contributionRate * 1.3, 0, MidpointRounding.AwayFromZero);
									this.AddRelationship(m, playerEventSide, p, r);
									return;
								}
								int r2 = (int)Math.Round(1.25 + (double)contributionRate * 1.3, 0, MidpointRounding.AwayFromZero);
								this.AddRelationship(m, playerEventSide, p, r2);
								return;
							}
							else
							{
								if (casualties2 > casualties)
								{
									int r3 = (int)Math.Round(1.1 + (double)contributionRate * 1.3, 0, MidpointRounding.AwayFromZero);
									this.AddRelationship(m, playerEventSide, p, r3);
									return;
								}
								this.AddRelationship(m, playerEventSide, p, 1);
								return;
							}
						}
						else
						{
							if ((double)(totalMen2 / totalMen) >= 1.2 && totalMen2 - totalMen > 60)
							{
								double num = (double)(totalMen2 / totalMen);
								int r4 = (int)Math.Round(2.4 + num + (double)contributionRate * 1.3, 0, MidpointRounding.AwayFromZero);
								this.AddRelationship(m, playerEventSide, p, r4);
								return;
							}
							int r5 = (int)Math.Round(2.25 + (double)contributionRate * 1.3, 0, MidpointRounding.AwayFromZero);
							this.AddRelationship(m, playerEventSide, p, r5);
							return;
						}
					}
					else if (playerEventSide.LeaderParty.Equals(PartyBase.MainParty))
					{
						if (totalMen >= totalMen2)
						{
							if (casualties <= casualties2)
							{
								double num2 = Math.Round((double)contributionRate * 1.3, 0, MidpointRounding.AwayFromZero);
								int r6 = -2 + (int)num2;
								this.AddRelationship(m, playerEventSide, p, r6);
								return;
							}
							double num3 = (double)((casualties - casualties2) / 50);
							num3 = Math.Ceiling(num3);
							double num4 = (double)contributionRate * 1.3;
							num4 = Math.Round(num4, 0, MidpointRounding.AwayFromZero);
							int r7 = -2 - (int)num3 + (int)num4;
							this.AddRelationship(m, playerEventSide, p, r7);
							return;
						}
						else
						{
							if (casualties > casualties2)
							{
								double num5 = (double)((totalMen2 - totalMen) / 50);
								double num6 = (double)((casualties - casualties2) / 50);
								num5 = Math.Ceiling(num5);
								num6 = Math.Ceiling(num6);
								double num7 = (double)contributionRate * 1.3;
								num7 = Math.Round(num7, 0, MidpointRounding.AwayFromZero);
								int r8 = -4 - (int)num5 - (int)num6 + (int)num7;
								this.AddRelationship(m, playerEventSide, p, r8);
								return;
							}
							double num8 = (double)((totalMen2 - totalMen) / 25);
							num8 = Math.Ceiling(num8);
							double num9 = (double)contributionRate * 1.3;
							num9 = Math.Round(num9, 0, MidpointRounding.AwayFromZero);
							int r9 = -4 - (int)num8 + (int)num9;
							this.AddRelationship(m, playerEventSide, p, r9);
						}
					}
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002444 File Offset: 0x00000644
		private void AddRelationship(MapEvent m, MapEventSide side, List<PartyBase> p, int r)
		{
			List<string> list = new List<string>();
			List<Hero> list2 = Hero.MainHero.CompanionsInParty.ToList<Hero>();
			string value = "Error";
			for (int i = 0; i <= p.Count - 1; i++)
			{
				if (!list.Contains(p[i].Owner.Clan.ToString()))
				{
					list.Add(p[i].Owner.Clan.ToString());
				}
			}
			for (int j = 0; j <= p.Count - 1; j++)
			{
				if (p[j].Owner != Hero.MainHero && list.Contains(p[j].Owner.Clan.ToString()))
				{
					int num = CharacterRelationManager.GetHeroRelation(Hero.MainHero, p[j].Owner.Clan.Leader) + r;
					for (int k = 0; k <= p.Count - 1; k++)
					{
						if (p[j].Owner.Clan.Equals(p[k].Owner.Clan))
						{
							CharacterRelationManager.SetHeroRelation(Hero.MainHero, p[k].Owner.Clan.Leader, num);
							CharacterRelationManager.SetHeroRelation(Hero.MainHero, p[k].Owner, num);
							if (r < 0)
							{
								value = string.Concat(new string[]
								{
									"Your relation is decreased by ",
									r.ToString(),
									" to ",
									num.ToString(),
									" with ",
									p[k].Owner.ToString()
								});
							}
							if (r >= 0)
							{
								value = string.Concat(new string[]
								{
									"Your relation is increased by ",
									r.ToString(),
									" to ",
									num.ToString(),
									" with ",
									p[k].Owner.ToString()
								});
							}
							InformationManager.AddQuickInformation(new TextObject(value, null), 0, null, "");
						}
					}
					list.Remove(p[j].Owner.Clan.ToString());
				}
			}
			for (int l = 0; l <= list2.Count - 1; l++)
			{
				int num2 = CharacterRelationManager.GetHeroRelation(Hero.MainHero, list2[l]) + r;
				CharacterRelationManager.SetHeroRelation(Hero.MainHero, list2[l], num2);
				if (r < 0)
				{
					value = string.Concat(new string[]
					{
						"Your relation is decreased by ",
						r.ToString(),
						" to ",
						num2.ToString(),
						" with ",
						list2[l].ToString()
					});
				}
				if (r >= 0)
				{
					value = string.Concat(new string[]
					{
						"Your relation is increased by ",
						r.ToString(),
						" to ",
						num2.ToString(),
						" with ",
						list2[l].ToString()
					});
				}
				InformationManager.AddQuickInformation(new TextObject(value, null), 0, null, "");
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002788 File Offset: 0x00000988
		private MapEventSide GetPlayerEventSide(MapEvent m)
		{
			if (m.PlayerSide.ToString().Equals("Defender"))
			{
				return m.DefenderSide;
			}
			if (m.PlayerSide.ToString().Equals("Attacker"))
			{
				return m.AttackerSide;
			}
			return null;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000027E4 File Offset: 0x000009E4
		private MapEventSide GetEnemyEventSide(MapEvent m)
		{
			if (m.PlayerSide.ToString().Equals("Defender"))
			{
				return m.AttackerSide;
			}
			if (m.PlayerSide.ToString().Equals("Attacker"))
			{
				return m.DefenderSide;
			}
			return null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002840 File Offset: 0x00000A40
		private int GetTotalMen(MapEventSide m)
		{
			return m.TroopCount + m.Casualties;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000284F File Offset: 0x00000A4F
		private List<PartyBase> GetHeroes(MapEventSide m)
		{
			return m.PartiesOnThisSide.ToList<PartyBase>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000285C File Offset: 0x00000A5C
		private float GetContributionRate(MapEventSide m)
		{
			return m.GetPlayerPartyContributionRate();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002864 File Offset: 0x00000A64
		private int GetCasualties(MapEventSide m)
		{
			return m.Casualties;
		}

		// Token: 0x04000001 RID: 1
		private bool loaded;
	}
}