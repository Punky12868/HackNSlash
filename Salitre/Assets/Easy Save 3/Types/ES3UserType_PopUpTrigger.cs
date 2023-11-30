using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("popUpType", "spawnIzq", "alreadyTriggered")]
	public class ES3UserType_PopUpTrigger : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PopUpTrigger() : base(typeof(PopUpTrigger)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (PopUpTrigger)obj;
			
			writer.WriteProperty("popUpType", instance.popUpType, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(PopUpTrigger.popUps)));
			writer.WritePrivateField("spawnIzq", instance);
			writer.WritePrivateField("alreadyTriggered", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (PopUpTrigger)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "popUpType":
						instance.popUpType = reader.Read<PopUpTrigger.popUps>();
						break;
					case "spawnIzq":
					instance = (PopUpTrigger)reader.SetPrivateField("spawnIzq", reader.Read<System.Boolean>(), instance);
					break;
					case "alreadyTriggered":
					instance = (PopUpTrigger)reader.SetPrivateField("alreadyTriggered", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PopUpTriggerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PopUpTriggerArray() : base(typeof(PopUpTrigger[]), ES3UserType_PopUpTrigger.Instance)
		{
			Instance = this;
		}
	}
}