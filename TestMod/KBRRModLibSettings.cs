using System.Xml.Serialization;
using ModLib.Definitions;
using ModLib.Definitions.Attributes;

namespace KillBanditsRaiseRelations
{
    public class KBRRModLibSettings : SettingsBase
    {
        public override string ModName => "KillBanditsRaiseRelations";
        public override string ModuleFolderName => "KillBanditsRaiseRelations";
        public const string InstanceID = "KBRRModLibSettings";
        [XmlElement]
        public override string ID { get; set; } = InstanceID;

        public static KBRRModLibSettings Instance
        {
            get
            {
                return (KBRRModLibSettings)SettingsDatabase.GetSettings<KBRRModLibSettings>();
            }
        }

        [XmlElement]
        [SettingProperty("Groups Of Bandits", 1, 50, "Number of bandit groups you must destroy before you gain relation.")]
        public int GroupsOfBandits { get; set; } = 1;
        [XmlElement]
        [SettingProperty("Relationship Increase", 1, 50, "The base value that your relationship will increase by when it increases.")]
        public int RelationshipIncrease { get; set; } = 1;
        [XmlElement]
        [SettingPropertyAttribute("Radius", 500, 5000, "This is the size of the radius inside which villages and towns will be affected by the relationship increase.")]
        public int Radius { get; set; } = 1000;
        [XmlElement]
        [SettingProperty("Size Bonus toggle", "Activate size bonus")]
        [SettingPropertyGroup("Size Bonus", true)]
        public bool SizeBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("SizeBonus", 0.001f, 1f, "Multiply the SizeBonus by the number of bandits you have killed since you last gained relationship," +
            " this will then be multiplied by the base Relationship Increase to give your final increase value.")]
        [SettingPropertyGroup("Size Bonus", false)]
        public float SizeBonus { get; set; } = 0.05f;
        [XmlElement]
        [SettingProperty("PrisonersOnly", "This settings controls whether or not only bandits with prisoners give a relationship increase.")]
        public bool PrisonersOnly { get; set; } = false;

        [XmlElement]
        [SettingProperty("IncludeBandits", "This settings controls whether or not bandit factions should be included.")]
        public bool IncludeBandits { get; set; } = true;

        [XmlElement]
        [SettingProperty("IncludeOutlaws", "This settings controls whether or not outlaw factions should be included.")]
        public bool IncludeOutlaws { get; set; } = false;

        [XmlElement]
        [SettingProperty("Include Mafia", "This settings controls whether or not mafia clans should be included.")]
        public bool IncludeMafia { get; set; } = false;


    }
}
