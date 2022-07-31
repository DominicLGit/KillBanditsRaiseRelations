using MCM.Abstractions.Settings.Base.Global;
using MCM.Abstractions.Attributes.v2;

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
		public override string DisplayName => "Kill Bandits Raise Relations";
		public override string FormatType => "xml";
		#endregion

		[SettingPropertyInteger("Groups Of Bandits", minValue: 1, maxValue: 10, HintText = "This is the number of bandit groups you must defeat before you gain relationship.", RequireRestart = false)]
		public int GroupsOfBandits { get; set; } = 1;
		[SettingPropertyInteger("Relationship Increase", minValue: 1, maxValue: 10, HintText = "This is the base value that your relationship will increase by when it increases. This value will then be affected by your charm skill.", RequireRestart = false)]
		public int RelationshipIncrease { get; set; } = 1;
		[SettingPropertyInteger("Radius", minValue: 1, maxValue: 10000, HintText = "This is the size of the radius inside which villages and towns will be affected by the relationship increase.", RequireRestart = false)]
		public int Radius { get; set; } = 1000;
		[SettingPropertyFloatingInteger("Size Bonus", minValue: 0.25f, maxValue: 100f, HintText = "This a multiplier for the Relationship Increase value." +
			" It will multiply Relationship Increase by the SizeBonus and the number of bandits you have killed since you last gained relationship.", RequireRestart = false)]
		public float SizeBonus { get; set; } = 0.25f;
		[SettingPropertyBool("Toggle Size Bonus", HintText = "This setting controls whether or not Size Bonus is used.", RequireRestart = false)]
		public bool ToggleSizeBonus { get; set; } = false;
		[SettingPropertyBool("Prisoners Only", HintText = "This settings controls whether or not only bandits with prisoners give a relationship increase.", RequireRestart = false)]
		public bool PrisonersOnly { get; set; } = false;

	}
}
