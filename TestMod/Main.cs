using System;
using System.IO;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using System.Xml;
using System.Xml.Serialization;
using TaleWorlds.CampaignSystem.MapEvents;

namespace KillBanditsRaiseRelations
{
	public class Main : MBSubModuleBase
	{

		private Settings CurrentSettings { get; set; }

		public override void BeginGameStart(Game game)
		{
			try
			{
				PlayerBattleEndEventListener playerBattleEndEventListener = new();
				CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(playerBattleEndEventListener, new Action<MapEvent>(playerBattleEndEventListener.IncreaseLocalRelationsAfterBanditFight));
			}
			catch (Exception ex)
			{
				InformationManager.DisplayMessage(new InformationMessage("An error has occurred during OnGameLoaded for KillBanditsRaiseRelations: " + ex.Message.ToString(), Color.FromUint(4282569842U)));
			}
		}

		private void DeserializeObject(string filename)
		{
			XmlSerializer serializer = new(typeof(Settings));

			// Declare an object variable of the type to be deserialized.
			Settings s;

			using (Stream reader = new FileStream(filename, FileMode.Open))
			{
				// Call the Deserialize method to restore the object's state.
				s = (Settings)serializer.Deserialize(reader);
			}

			// Write out the properties of the object.
			CurrentSettings = s;
		}

		private void SerializeObject(string filename)
		{
			Console.WriteLine("Writing With XmlTextWriter");

			XmlSerializer serializer = new(typeof(Settings));
			Settings s = new();
			// Create an XmlTextWriter using a FileStream.
			Stream fs = new FileStream(filename, FileMode.Create);
            XmlWriterSettings XmlSettings = new()
            {
                Indent = true,
                IndentChars = ("\t"),
                OmitXmlDeclaration = true
            };
            XmlWriter writer = XmlWriter.Create(fs, XmlSettings);
			// Serialize using the XmlTextWriter.
			serializer.Serialize(writer, s);
			writer.Close();
		}
	}
}
