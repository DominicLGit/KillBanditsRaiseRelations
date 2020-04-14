using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
}
