using Modding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace GoldenGodhome
{
	public class GoldenGodhome : Mod
	{
		AssetBundle bundle;
		public override void Initialize()
		{
			ModHooks.OnEnableEnemyHook += PatchGodhomeEnemySprites;

			Assembly a = Assembly.GetExecutingAssembly();
			Stream s = a.GetManifestResourceStream("GoldenGodhome.godhome");
			bundle = AssetBundle.LoadFromStream(s);

			s.Dispose();
		}

		private bool PatchGodhomeEnemySprites(GameObject enemy, bool isAlreadyDead)
		{
			string normalName = enemy.name;

			if (enemy.name.Contains(" (Clone)") || enemy.name.Contains("(Clone)"))
			{
				normalName = normalName.Replace(" (Clone)", "");
				normalName = normalName.Replace("(Clone)", "");
			}
			else if (enemy.name.Contains("("))
			{
				normalName = enemy.name.Substring(0, enemy.name.LastIndexOf("("));
			}
			if (normalName.EndsWith(" "))
			{
				normalName = normalName.Remove(normalName.Length - 1);
			}

			if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.StartsWith("GG_") && GodhomeTextures.ContainsKey(normalName))
			{
				enemy.transform.gameObject.GetComponent<tk2dSprite>().Collection.FirstValidDefinition.material.mainTexture = bundle.LoadAsset<Texture2D>(GodhomeTextures[normalName]);
			}
			return isAlreadyDead;
		}

		void OnSceneLoaded(Scene old, Scene newScene)
		{
			switch (newScene.name)
			{
				case "GG_Mantis_Lords":
					newScene.FindGameObject("Mantis Battle").FindGameObjectInChildren("Mantis Lord Throne 1").GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = bundle.LoadAsset<Texture2D>("GG_mantis_lords");
					break;
				case "GG_Mantis_Lords_V":
					newScene.FindGameObject("Mantis Battle").FindGameObjectInChildren("Mantis Lord Throne 1").GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = bundle.LoadAsset<Texture2D>("GG_mantis_lords");
					break;
				case "GG_Brooding_Mawlek_V":
					newScene.FindGameObject("Battle Scene").FindGameObjectInChildren("Tiso Boss").GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = bundle.LoadAsset<Texture2D>("GG_tiso");
					break;
			}
		}

		public static readonly Dictionary<string, string> GodhomeTextures = new Dictionary<string, string>()
		{
			{ "Infected Knight", "GG_broken_vessel" },
			{ "Mawlek Body", "GG_brooding_mawlek" },
			{ "Oro", "GG_brothers_oro_mato" },
			{ "Mato", "GG_brothers_oro_mato" },
			{ "Mega Zombie Beam Miner", "GG_crystal_guardian" },
			{ "Zombie Beam Miner Rematch", "GG_crystal_guardian" },
			{ "Dung Defender", "GG_dung_defender" },
			{ "White Defender", "GG_white_defender" },
			{ "Ghost Warrior Hu", "GG_elder_hu" },
			{ "False Knight New", "GG_false_knight" },
			{ "False Knight Dream", "GG_failed_champion" },
			{ "Fluke Mother", "GG_flukemarm" },
			{ "Ghost Warrior Galien", "GG_galien" },
			{ "Lobster", "GG_god_tamer" },
			{ "Lancer", "GG_god_tamer" },
			{ "Ghost Warrior Slug", "GG_gorb" },
			{ "Sly Boss", "GG_great_nailsage_sly" },
			{ "Grey Prince", "GG_grey_prince_zote" },
			{ "Giant Fly", "GG_gruz_mother" },
			{ "Hive Knight", "GG_hive_knight" },
			{ "Hornet Boss 1", "GG_hornet" },
			{ "Hornet Boss 2", "GG_hornet" },
			{ "Mantis Lord", "GG_mantis_lords" },
			{ "Mantis Lord S1", "GG_mantis_lords" },
			{ "Mantis Lord S2", "GG_mantis_lords" },
			{ "Ghost Warrior Markoth", "GG_markoth" },
			{ "Ghost Warrior Marmu", "GG_marmu" },
			{ "Nightmare Grimm Boss", "GG_nightmare_king_grimm" },
			{ "Ghost Warrior No Eyes", "GG_no_eyes" },
			{ "Mimic Spider", "GG_nosk" },
			{ "Mega Fat Bee ", "GG_oblobbles" },
			{ "Sheo Boss", "GG_paintmaster_sheo" },
			{ "HK Prime", "GG_pure_vessel" },
			{ "Mage Lord", "GG_soul_master" },
			{ "Mage Lord Phase2", "GG_soul_master" },
			{ "Dream Mage Lord", "GG_soul_tyrant" },
			{ "Dream Mage Lord Phase2", "GG_soul_tyrant" },
			{ "Mage Knight", "GG_soul_warrior" },
			{ "Mantis Traitor Lord", "GG_traitor_lord" },
			{ "Grimm Boss", "GG_troupe_master_grimm" },
			{ "Giant Buzzer Col", "GG_vengefly_king" },
			{ "Black Knight 1", "GG_watcher_knight" },
			{ "Black Knight 2", "GG_watcher_knight" },
			{ "Black Knight 3", "GG_watcher_knight" },
			{ "Black Knight 4", "GG_watcher_knight" },
			{ "Black Knight 5", "GG_watcher_knight" },
			{ "Black Knight 6", "GG_watcher_knight" },
			{ "Hornet Nosk", "GG_winged_nosk" },
			{ "Ghost Warrior Xero", "GG_xero" },
			{ "Mega Moss Charger", "GG_massive_moss_charger" },
			{ "Absolute Radiance", "GG_absolute_radiance" },
		};
	}
}
