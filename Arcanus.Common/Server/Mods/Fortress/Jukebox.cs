namespace Arcanus.Mods
{
	public class Jukebox : IMod
	{
		public void PreStart(ModManager m)
		{
			m.RequireMod("CoreBlocks");
		}

		public void Start(ModManager manager)
		{
			m = manager;

			m.RegisterOnBlockUse(OnUse);

			SoundSet solidSounds = new SoundSet()
			{
				Walk = new string[] { "walk1", "walk2", "walk3", "walk4" },
				Break = new string[] { "destruct" },
				Build = new string[] { "build" },
				Clone = new string[] { "clone" },
			};

			m.SetBlockType(134, "Jukebox", new BlockType()
			{
				TextureIdTop = "JukeboxTop",
				TextureIdBack = "JukeboxSide",
				TextureIdFront = "JukeboxSide",
				TextureIdLeft = "JukeboxSide",
				TextureIdRight = "JukeboxSide",
				TextureIdBottom = "JukeboxBottom",
				TextureIdForInventory = "JukeboxTop",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				InventoryType = InventoryType.Item,
				SortAfter = "Trampoline",
				Sounds = solidSounds,
				IsUsable = true,
			});

			m.AddToCreativeInventory("Jukebox");
			m.AddCraftingRecipe("Jukebox", 1, "GoldBlock", 1);

			jukebox = m.GetBlockId("Jukebox");
		}

		ModManager m;
		int jukebox;
		bool playing = false;

		private void OnUse(int player, int x, int y, int z)
		{
			if (m.GetBlock(x, y, z) == jukebox)
			{
				// TODO: turn off the sound if it's playing
				// TODO: add some blinking lights :)
				m.PlaySoundAt(x, y, z, "jukebox.ogg");
			}
		}
	}
}
