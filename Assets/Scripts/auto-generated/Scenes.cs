//This class is auto-generated do not modify
using UnityEngine.SceneManagement;
namespace k
{
	public static class Scenes
	{
		public const string DEFAULT = "default";

		public const int TOTAL_SCENES = 1;


		public static int nextSceneIndex()
		{
			if( SceneManager.GetActiveScene().buildIndex + 1 == TOTAL_SCENES )
				return 0;
			return SceneManager.GetActiveScene().buildIndex + 1;
		}
	}
}