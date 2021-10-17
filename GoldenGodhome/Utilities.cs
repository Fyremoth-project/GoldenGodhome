using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace GoldenGodhome
{
	public static class Utilities
	{
		public static GameObject FindGameObject(this Scene scene, string name)
		{
			if (!scene.IsValid())
				return null;

			GameObject[] rootGameObjects = scene.GetRootGameObjects();

			foreach (GameObject go in rootGameObjects)
			{
				if (go == null)
				{
					break;
				}

				GameObject found = go.FindGameObjectInChildren(name);
				if (found != null)
					return found;
			}

			return null;
		}

		public static GameObject FindGameObjectInChildren(this GameObject gameObject, string name)
		{
			if (gameObject == null)
				return null;

			foreach (var t in gameObject.GetComponentsInChildren<Transform>(true))
			{
				if (t.name == name)
					return t.gameObject;
			}
			return null;
		}
	}
}
