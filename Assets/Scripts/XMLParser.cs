﻿using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class XMLParser 
{
	public static StaticEntityProperties[] ParseBuildings (string content)
	{
		XmlReader reader = XmlReader.Create (new StringReader (content));
		List<StaticEntityProperties> entities = new List<StaticEntityProperties> ();
		StaticEntityProperties current = null;
		while (reader.Read()) 
		{
			if(reader.IsStartElement("building"))
			{
				if(current != null)
				{
					entities.Add(current);
				}
				current = new StaticEntityProperties();
			}
			if(current != null)
			{
				if(reader.IsStartElement("id"))
				{
					current.Id = reader.ReadElementContentAsInt();
				}
				if(reader.IsStartElement("name"))
				{
					current.Name = reader.ReadElementContentAsString();
				}
				if(reader.IsStartElement("resources"))
				{
					int food, wood, gold = 0;
					food = int.Parse(reader.GetAttribute("food"));
					gold = int.Parse(reader.GetAttribute("gold"));
					wood = int.Parse(reader.GetAttribute("wood"));
					current.NecessaryResources = new ResourceSet(wood, food, gold);
				}
				if(reader.IsStartElement("description"))
				{
					current.Description = reader.ReadElementContentAsString();
				}
				if(reader.IsStartElement("availableOn"))
				{
					current.AvailableOn = reader.ReadElementContentAsInt();
				}
				if(reader.IsStartElement("level"))
				{
					current.Level = reader.ReadElementContentAsInt();
				}
				if(reader.IsStartElement("script"))
				{
					Debug.Log (reader.AttributeCount);
					current.scriptInfo = new StaticUnitScriptInfo (string.Empty, new string [reader.AttributeCount]);
					for (int i = 0; i < reader.AttributeCount; i++)
					{
						Debug.Log (reader.GetAttribute ("arg" + i));
						current.scriptInfo.arguments[i] = reader.GetAttribute ("arg" + i);
					}
					current.scriptInfo.script = reader.ReadElementContentAsString();
				}
			}
		}

		if (current != null)
		{
			entities.Add(current);
		}

		return entities.ToArray();
	}

	public static MobileEntityProperties[] ParseCharacters (string content)
	{
		XmlReader reader = XmlReader.Create (new StringReader (content));
		List<MobileEntityProperties> entities = new List<MobileEntityProperties> ();
		MobileEntityProperties current = null;
		while (reader.Read()) 
		{
			if(reader.IsStartElement("character"))
			{
				if(current != null)
				{
					entities.Add(current);
				}
				current = new MobileEntityProperties();
			}
			if(current != null)
			{
				if(reader.IsStartElement("id"))
				{
					current.Id = reader.ReadElementContentAsInt();
				}
				if(reader.IsStartElement("name"))
				{
					current.Name = reader.ReadElementContentAsString();
				}
				if(reader.IsStartElement("resources"))
				{
					int food, wood, gold = 0;
					food = int.Parse(reader.GetAttribute("food"));
					gold = int.Parse(reader.GetAttribute("gold"));
					wood = int.Parse(reader.GetAttribute("wood"));
					current.NecessaryResources = new ResourceSet(wood, food, gold);
				}
				if(reader.IsStartElement("description"))
				{
					current.Description = reader.ReadElementContentAsString();
				}
				if(reader.IsStartElement("availableOn"))
				{
					current.AvailableOn = reader.ReadElementContentAsInt();
				}
				if(reader.IsStartElement("level"))
				{
					current.Level = reader.ReadElementContentAsInt();
				}
			}
		}
		
		if (current != null)
		{
			entities.Add(current);
		}
		
		return entities.ToArray();
	}
}