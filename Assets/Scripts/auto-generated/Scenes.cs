using UnityEngine.SceneManagement;
namespace k
{
	public static class Scenes
	{
		public const string BOOSTRAP_SCENE = "BoostrapScene";
		public const string SCENE1 = "Scene1";
		public const string SCENE2 = "Scene2";

		public const int TOTAL_SCENES = 3;


		public static int nextSceneIndex()
		{
			if( SceneManager.GetActiveScene().buildIndex + 1 == TOTAL_SCENES )
				return 0;
			return SceneManager.GetActiveScene().buildIndex + 1;
		}
	}
}