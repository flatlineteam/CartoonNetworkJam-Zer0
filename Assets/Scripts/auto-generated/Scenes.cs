//This class is auto-generated do not modify
using UnityEngine.SceneManagement;
namespace k
{
	public static class Scenes
	{
		public const string TITLE = "Title";
		public const string DEFAULT = "default";
		public const string CREDITS = "Credits";

		public const int TOTAL_SCENES = 3;


		public static int nextSceneIndex()
		{
			if( SceneManager.GetActiveScene().buildIndex + 1 == TOTAL_SCENES )
				return 0;
			return SceneManager.GetActiveScene().buildIndex + 1;
		}
	}
}