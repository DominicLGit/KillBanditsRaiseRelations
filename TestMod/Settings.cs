﻿
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace KillBanditsRaiseRelations
{
	public class Settings
	{
		public int GroupsOfBandits { get; set; }
		public int RelationshipIncrease { get; set; }
		public int Radius { get; set; }
		public float SizeBonus { get; set; }
		public bool PrisonersOnly { get; set; }


		public Settings()
		{
			this.GroupsOfBandits = 1;
			this.RelationshipIncrease = 1;
			this.Radius = 1000;
			this.SizeBonus = 0;
			this.PrisonersOnly = false;
		}

		public void ClampValues()
		{
			GroupsOfBandits = GroupsOfBandits < 1 ? 1 : GroupsOfBandits;
			this.RelationshipIncrease = this.RelationshipIncrease < 1 ? 1 : this.RelationshipIncrease;
			this.Radius = this.Radius < 1 ? 0 : this.Radius;
			this.SizeBonus = this.SizeBonus < 0 ? 0 : this.SizeBonus;
		}									
	}

	public class SettingsMCM: AttributeGlobalSettings<SettingsMCM>
	{
        #region MCM attributes
        public override string Id => "KillBanditsRaiseRelations_1";
		public override string DisplayName => $"Kill Bandits Raise Relations v{typeof(SettingsMCM).Assembly.GetName().Version.ToString(3)}";
		public override string FormatType => "xml";
		#endregion

		[SettingPropertyInteger("{=KBRR.MCM001}Groups Of Bandits", minValue: 1, maxValue: 10, Order = 0,
            HintText = "{=KBRR.MCM001Hint}This is the number of bandit groups you must defeat before you gain relationship.", RequireRestart = false)]
		public int GroupsOfBandits { get; set; } = 1;
		[SettingPropertyInteger("{=KBRR.MCM002}Relationship Increase", minValue: 1, maxValue: 10, Order = 1,
			HintText = "{=KBRR.MCM002Hint}This is the base value that your relationship will increase by when it increases. This value will then be affected by your charm skill.", RequireRestart = false)]
		public int RelationshipIncrease { get; set; } = 1;
		[SettingPropertyInteger("{=KBRR.MCM003}Radius", minValue: 1, maxValue: 10000, Order = 2,
			HintText = "{=KBRR.MCM003Hint}This is the size of the radius inside which villages and towns will be affected by the relationship increase.", RequireRestart = false)]
		public int Radius { get; set; } = 1000;
		[SettingPropertyFloatingInteger("{=KBRR.MCM004}Size Bonus", minValue: 0.25f, maxValue: 100f, Order = 3,
			HintText = "{=KBRR.MCM004Hint}This a multiplier for the Relationship Increase value." +
			" It will multiply Relationship Increase by the SizeBonus and the number of bandits you have killed since you last gained relationship.", RequireRestart = false)]
		public float SizeBonus { get; set; } = 0.25f;
		[SettingPropertyBool("{=KBRR.MCM005}Toggle Size Bonus", Order = 4,
			HintText = "{=KBRR.MCM005Hint}This setting controls whether or not Size Bonus is used.", RequireRestart = false)]
		public bool ToggleSizeBonus { get; set; } = false;
		[SettingPropertyBool("{=KBRR.MCM006}Prisoners Only", Order = 5,
			HintText = "{=KBRR.MCM006Hint}This settings controls whether or not only bandits with prisoners give a relationship increase.", RequireRestart = false)]
		public bool PrisonersOnly { get; set; } = false;

	}
}
