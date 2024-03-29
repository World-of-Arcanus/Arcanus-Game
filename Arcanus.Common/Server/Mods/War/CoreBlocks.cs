﻿namespace Arcanus.Mods.War
{
	/// <summary>
	/// This class contains all block definitions
	/// </summary>
	public class CoreBlocks : IMod
	{
		ModManager m;
		SoundSet solidSounds;
		SoundSet snowSounds;
		SoundSet noSound;

		public void PreStart(ModManager m)
		{
			m.RequireMod("CoreWar");
		}
		public void Start(ModManager manager)
		{
			m = manager;

			//Timer for season changes
			m.RegisterTimer(UpdateSeasons, 1);

			//Initialize sounds
			noSound = new SoundSet();
			solidSounds = new SoundSet()
			{
				Walk = new string[] { "walk1", "walk2", "walk3", "walk4" },
				Break = new string[] { "destruct" },
				Build = new string[] { "build" },
				Clone = new string[] { "clone" },
			};
			snowSounds = new SoundSet()
			{
				Walk = new string[] { "walksnow1", "walksnow2", "walksnow3", "walksnow4" },
				Break = new string[] { "destruct" },
				Build = new string[] { "build" },
				Clone = new string[] { "clone" },
			};
			m.SetDefaultSounds(solidSounds);

			#region Block types
			m.SetBlockType(0, "Empty", new BlockType()
			{
				DrawType = DrawType.Empty,
				WalkableType = WalkableType.Empty,
				Sounds = noSound,
			});
			m.SetBlockType(1, "Stone", new BlockType()
			{
				AllTextures = "Stone",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(2, "Grass", new BlockType()
			{
				TextureIdTop = "Grass",
				SideTextures = "GrassSide",
				TextureIdForInventory = "GrassSide",
				TextureIdBottom = "Dirt",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				WhenPlayerPlacesGetsConvertedTo = 3,
			});
			m.SetBlockType(3, "Dirt", new BlockType()
			{
				AllTextures = "Dirt",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(4, "Cobblestone", new BlockType()
			{
				AllTextures = "Cobblestone",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(5, "OakWood", new BlockType()
			{
				AllTextures = "OakWood",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(6, "OakSapling", new BlockType()
			{
				AllTextures = "OakSapling",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(7, "Adminium", new BlockType()
			{
				AllTextures = "Adminium",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(8, "Water", new BlockType()
			{
				AllTextures = "Water",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = noSound,
			});
			m.SetBlockType(9, "StationaryWater", new BlockType()
			{
				AllTextures = "Water",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = noSound,
			});
			m.SetBlockType(10, "Lava", new BlockType()
			{
				AllTextures = "Lava",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = noSound,
				LightRadius = 15,
				DamageToPlayer = 2,
			});
			m.SetBlockType(11, "StationaryLava", new BlockType()
			{
				AllTextures = "Lava",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = noSound,
				LightRadius = 15,
				DamageToPlayer = 2,
			});
			m.SetBlockType(12, "Sand", new BlockType()
			{
				AllTextures = "Sand",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(13, "Gravel", new BlockType()
			{
				AllTextures = "Gravel",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(138, "DiamondOre", new BlockType()
			{
				AllTextures = "DiamondOre",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(14, "GoldOre", new BlockType()
			{
				AllTextures = "GoldOre",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(139, "EmeraldOre", new BlockType()
			{
				AllTextures = "EmeraldOre",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(154, "LapisOre", new BlockType()
			{
				AllTextures = "LapisOre",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(15, "IronOre", new BlockType()
			{
				AllTextures = "IronOre",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(16, "CoalOre", new BlockType()
			{
				AllTextures = "CoalOre",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(17, "OakTreeTrunk", new BlockType()
			{
				TopBottomTextures = "OakTreeTrunkTopBottom",
				SideTextures = "OakTreeTrunk",
				TextureIdForInventory = "OakTreeTrunk",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(18, "OakLeaves", new BlockType()
			{
				AllTextures = "OakLeaves",
				DrawType = DrawType.Transparent,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(19, "Sponge", new BlockType()
			{
				AllTextures = "Sponge",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(20, "Glass", new BlockType()
			{
				AllTextures = "Glass",
				DrawType = DrawType.Transparent,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(21, "RedCloth", new BlockType()
			{
				AllTextures = "RedCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(22, "OrangeCloth", new BlockType()
			{
				AllTextures = "OrangeCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(23, "YellowCloth", new BlockType()
			{
				AllTextures = "YellowCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(24, "LightGreenCloth", new BlockType()
			{
				AllTextures = "LightGreenCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(25, "GreenCloth", new BlockType()
			{
				AllTextures = "GreenCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(27, "CyanCloth", new BlockType()
			{
				AllTextures = "CyanCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(26, "LightBlueCloth", new BlockType()
			{
				AllTextures = "LightBlueCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(28, "BlueCloth", new BlockType()
			{
				AllTextures = "BlueCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(29, "PurpleCloth", new BlockType()
			{
				AllTextures = "PurpleCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(30, "BrownCloth", new BlockType()
			{
				AllTextures = "BrownCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(31, "SilverCloth", new BlockType()
			{
				AllTextures = "SilverCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(32, "MagentaCloth", new BlockType()
			{
				AllTextures = "MagentaCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(33, "PinkCloth", new BlockType()
			{
				AllTextures = "PinkCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(34, "BlackCloth", new BlockType()
			{
				AllTextures = "BlackCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(35, "GrayCloth", new BlockType()
			{
				AllTextures = "GrayCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(36, "WhiteCloth", new BlockType()
			{
				AllTextures = "WhiteCloth",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(37, "Dandelion", new BlockType()
			{
				AllTextures = "FlowerDandelion",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(38, "Rose", new BlockType()
			{
				AllTextures = "FlowerRose",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(39, "RedMushroom", new BlockType()
			{
				AllTextures = "RedMushroom",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(40, "BrownMushroom", new BlockType()
			{
				AllTextures = "BrownMushroom",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(135, "DiamondBlock", new BlockType()
			{
				AllTextures = "DiamondBlock",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(41, "GoldBlock", new BlockType()
			{
				AllTextures = "GoldBlock",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(136, "EmeraldBlock", new BlockType()
			{
				AllTextures = "EmeraldBlock",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(137, "LapisBlock", new BlockType()
			{
				AllTextures = "LapisBlock",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(42, "IronBlock", new BlockType()
			{
				AllTextures = "IronBlock",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(43, "DoubleStair", new BlockType()
			{
				TopBottomTextures = "Stair",
				SideTextures = "DoubleStairSide",
				TextureIdForInventory = "DoubleStairSide",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(44, "Stair", new BlockType()
			{
				TopBottomTextures = "Stair",
				SideTextures = "StairSide",
				TextureIdForInventory = "StairInventory",
				DrawType = DrawType.HalfHeight,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(45, "Brick", new BlockType()
			{
				AllTextures = "Brick",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(47, "Bookcase", new BlockType()
			{
				TopBottomTextures = "OakWood",
				SideTextures = "Bookcase",
				TextureIdForInventory = "Bookcase",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(48, "MossyCobblestone", new BlockType()
			{
				AllTextures = "MossyCobblestone",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(49, "Obsidian", new BlockType()
			{
				AllTextures = "Obsidian",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(50, "Torch", new BlockType()
			{
				TextureIdTop = "TorchTop",
				TextureIdBottom = "Torch",
				SideTextures = "Torch",
				TextureIdForInventory = "Torch",
				LightRadius = 15,
				DrawType = DrawType.Torch,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(51, "Clay", new BlockType()
			{
				AllTextures = "Clay",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(52, "Marble", new BlockType()
			{
				AllTextures = "Marble",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(53, "Granite", new BlockType()
			{
				AllTextures = "Granite",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(54, "RedSand", new BlockType()
			{
				AllTextures = "RedSand",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(55, "Sandstone", new BlockType()
			{
				AllTextures = "Sandstone",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(56, "RedSandstone", new BlockType()
			{
				AllTextures = "RedSandstone",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(57, "Cactus", new BlockType()
			{
				TextureIdTop = "CactusTop",
				TextureIdBottom = "CactusBottom",
				SideTextures = "CactusSide",
				TextureIdForInventory = "CactusSide",
				DrawType = DrawType.Cactus,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(58, "DeadPlant", new BlockType()
			{
				AllTextures = "DeadPlant",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(59, "GrassPlant", new BlockType()
			{
				AllTextures = "GrassPlant",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(60, "BirchTreeTrunk", new BlockType()
			{
				TopBottomTextures = "BirchTreeTrunkTopBottom",
				SideTextures = "BirchTreeTrunk",
				TextureIdForInventory = "BirchTreeTrunk",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(61, "BirchLeaves", new BlockType()
			{
				AllTextures = "BirchLeaves",
				DrawType = DrawType.Transparent,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(62, "BirchSapling", new BlockType()
			{
				AllTextures = "BirchSapling",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(63, "BirchWood", new BlockType()
			{
				AllTextures = "BirchWood",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(64, "SpruceTreeTrunk", new BlockType()
			{
				TopBottomTextures = "SpruceTreeTrunkTopBottom",
				SideTextures = "SpruceTreeTrunk",
				TextureIdForInventory = "SpruceTreeTrunk",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(65, "SpruceLeaves", new BlockType()
			{
				AllTextures = "SpruceLeaves",
				DrawType = DrawType.Transparent,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(66, "SpruceSapling", new BlockType()
			{
				AllTextures = "SpruceSapling",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(67, "SpruceWood", new BlockType()
			{
				AllTextures = "SpruceWood",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(68, "HalfCobblestone", new BlockType()
			{
				TopBottomTextures = "Cobblestone",
				SideTextures = "CobblestoneHalf",
				TextureIdForInventory = "CobblestoneHalfInventory",
				DrawType = DrawType.HalfHeight,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(69, "HalfMossyCobblestone", new BlockType()
			{
				TopBottomTextures = "MossyCobblestone",
				SideTextures = "MossyCobblestoneHalf",
				TextureIdForInventory = "MossyCobblestoneHalfInventory",
				DrawType = DrawType.HalfHeight,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(70, "HalfOakWood", new BlockType()
			{
				TopBottomTextures = "OakWood",
				SideTextures = "OakWoodHalf",
				TextureIdForInventory = "OakWoodHalfInventory",
				DrawType = DrawType.HalfHeight,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(71, "HalfBirchWood", new BlockType()
			{
				TopBottomTextures = "BirchWood",
				SideTextures = "BirchWoodHalf",
				TextureIdForInventory = "BirchWoodHalfInventory",
				DrawType = DrawType.HalfHeight,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(72, "HalfSpruceWood", new BlockType()
			{
				TopBottomTextures = "SpruceWood",
				SideTextures = "SpruceWoodHalf",
				TextureIdForInventory = "SpruceWoodHalfInventory",
				DrawType = DrawType.HalfHeight,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(73, "HalfBrick", new BlockType()
			{
				TopBottomTextures = "Brick",
				SideTextures = "BrickHalf",
				TextureIdForInventory = "BrickHalfInventory",
				DrawType = DrawType.HalfHeight,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(74, "HalfSandBrick", new BlockType()
			{
				TopBottomTextures = "SandBrick",
				SideTextures = "SandBrickHalf",
				TextureIdForInventory = "SandBrickHalfInventory",
				DrawType = DrawType.HalfHeight,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(100, "BrushedMetal", new BlockType()
			{
				AllTextures = "BrushedMetal",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(101, "ChemicalGreen", new BlockType()
			{
				AllTextures = "ChemicalGreen",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(102, "Salt", new BlockType()
			{
				AllTextures = "Salt",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(103, "StoneRoof", new BlockType()
			{
				AllTextures = "StoneRoof",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(104, "Camouflage", new BlockType()
			{
				AllTextures = "Camouflage",
				DrawType = DrawType.Fence,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(105, "DirtForFarming", new BlockType()
			{
				AllTextures = "DirtForFarming",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(106, "Apples", new BlockType()
			{
				AllTextures = "Apples",
				DrawType = DrawType.Transparent,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				IsUsable = true,
			});
			m.SetBlockType(107, "Hay", new BlockType()
			{
				AllTextures = "Hay",
				DrawType = DrawType.Transparent,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(108, "Wheat", new BlockType()
			{
				AllTextures = "Crops1",
				TextureIdForInventory = "Crops4",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(109, "Crops2", new BlockType()
			{
				AllTextures = "Crops2",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(110, "Crops3", new BlockType()
			{
				AllTextures = "Crops3",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(111, "Crops4", new BlockType()
			{
				AllTextures = "Crops4",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(112, "CraftingTable", new BlockType()
			{
				TextureIdTop = "CraftingTableTopBottom",
				TextureIdBack = "CraftingTableSide",
				TextureIdFront = "CraftingTableFront",
				TextureIdLeft = "CraftingTableSide",
				TextureIdRight = "CraftingTableSide",
				TextureIdForInventory = "CraftingTableFront",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				IsUsable = true,
			});
			m.SetBlockType(113, "Minecart", new BlockType()
			{
				TextureIdTop = "MinecartTop",
				TextureIdBack = "Minecart",
				TextureIdFront = "Minecart",
				TextureIdLeft = "Minecart",
				TextureIdRight = "Minecart",
				DrawType = DrawType.HalfHeight,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(114, "Trampoline", new BlockType()
			{
				AllTextures = "Trampoline",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(115, "FillStart", new BlockType()
			{
				AllTextures = "FillStart",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(116, "Cuboid", new BlockType()
			{
				AllTextures = "FillEnd",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(117, "FillArea", new BlockType()
			{
				AllTextures = "FillArea",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = solidSounds,
			});
			m.SetBlockType(118, "Water0", new BlockType()
			{
				AllTextures = "Water",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = solidSounds,
			});
			m.SetBlockType(119, "Water1", new BlockType()
			{
				AllTextures = "Water",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = solidSounds,
			});
			m.SetBlockType(120, "Water2", new BlockType()
			{
				AllTextures = "Water",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = solidSounds,
			});
			m.SetBlockType(121, "Water3", new BlockType()
			{
				AllTextures = "Water",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = solidSounds,
			});
			m.SetBlockType(122, "Water4", new BlockType()
			{
				AllTextures = "Water",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = solidSounds,
			});
			m.SetBlockType(123, "Water5", new BlockType()
			{
				AllTextures = "Water",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = solidSounds,
			});
			m.SetBlockType(124, "Water6", new BlockType()
			{
				AllTextures = "Water",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = solidSounds,
			});
			m.SetBlockType(125, "Water7", new BlockType()
			{
				AllTextures = "Water",
				DrawType = DrawType.Fluid,
				WalkableType = WalkableType.Fluid,
				Sounds = solidSounds,
			});
			/*
			m.SetBlockType(130, "GrassTrap", new BlockType()
			{
				TextureIdTop = "Camouflage",
				SideTextures = "GrassSide",
				TextureIdForInventory = "GrassSide",
				TextureIdBottom = "Dirt",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			*/
			m.SetBlockType(131, "WoodWindow", new BlockType()
			{
				TextureIdTop = "WoodBlock",
				TextureIdBack = "WoodWindow",
				TextureIdFront = "WoodWindow",
				TextureIdLeft = "WoodBlock",
				TextureIdRight = "WoodBlock",
				TextureIdBottom = "WoodBlock",
				TextureIdForInventory = "WoodWindow",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(132, "IronWindow", new BlockType()
			{
				TextureIdTop = "IronBlock",
				TextureIdBack = "IronWindow",
				TextureIdFront = "IronWindow",
				TextureIdLeft = "IronBlock",
				TextureIdRight = "IronBlock",
				TextureIdBottom = "IronBlock",
				TextureIdForInventory = "IronWindow",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(133, "IronBars", new BlockType()
			{
				AllTextures = "IronBars",
				DrawType = DrawType.Fence,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
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
				Sounds = solidSounds,
				IsUsable = true,
			});
			m.SetBlockType(140, "DirtBrick", new BlockType()
			{
				AllTextures = "DirtBrick",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(141, "WoodRoof", new BlockType()
			{
				AllTextures = "WoodRoof",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(142, "SandBrick", new BlockType()
			{
				AllTextures = "SandBrick",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(143, "FakeBookcase", new BlockType()
			{
				TopBottomTextures = "OakWood",
				SideTextures = "Bookcase",
				TextureIdForInventory = "Bookcase",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
			});
			m.SetBlockType(144, "WoodDesk", new BlockType()
			{
				TextureIdTop = "WoodBlock",
				TextureIdBottom = "Empty",
				SideTextures = "GlassDeskSide",
				TextureIdForInventory = "WoodBlock",
				DrawType = DrawType.Transparent,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(145, "GlassDesk", new BlockType()
			{
				TextureIdTop = "Glass",
				TextureIdBottom = "Empty",
				SideTextures = "GlassDeskSide",
				TextureIdForInventory = "GlassDeskSide",
				DrawType = DrawType.Transparent,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(146, "Mosaic", new BlockType()
			{
				AllTextures = "Mosaic",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(147, "Asphalt", new BlockType()
			{
				AllTextures = "Asphalt",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(148, "Cake", new BlockType()
			{
				TextureIdTop = "CakeTop",
				TextureIdBottom = "CakeBottom",
				SideTextures = "CakeSide",
				TextureIdForInventory = "CakeTop",
				DrawType = DrawType.HalfHeight,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(149, "Fire", new BlockType()
			{
				AllTextures = "Fire",
				LightRadius = 15,
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds,
				DamageToPlayer = 2,
			});
			m.SetBlockType(150, "Fence", new BlockType()
			{
				AllTextures = "Fence",
				DrawType = DrawType.Fence,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(151, "Compass", new BlockType()
			{
				AllTextures = "CompassInventory",
				TextureIdForInventory = "CompassInventory",
				DrawType = DrawType.Plant,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
			});
			m.SetBlockType(152, "Ladder", new BlockType()
			{
				AllTextures = "Ladder",
				DrawType = DrawType.Ladder,
				WalkableType = WalkableType.Fluid,
				Sounds = solidSounds,
			});
			m.SetBlockType(153, "EmptyHand", new BlockType()
			{
				// a walking stick
				AllTextures = "EmptyHand",
				DrawType = DrawType.Torch,
				WalkableType = WalkableType.Empty,
				Sounds = noSound,
			});
			m.SetBlockType(176, "Rail0", new BlockType()
			{
				TextureIdTop = "Rail0",
				TextureIdBottom = "RailBottom",
				SideTextures = "RailSide",
				TextureIdForInventory = "Rail0",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				Rail = 0,
			});
			m.SetBlockType(177, "Rail1", new BlockType()
			{
				TextureIdTop = "Rail1",
				TextureIdBottom = "RailBottom",
				SideTextures = "RailSide",
				TextureIdForInventory = "Rail1",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				Rail = 1,
			});
			m.SetBlockType(178, "Rail2", new BlockType()
			{
				TextureIdTop = "Rail2",
				TextureIdBottom = "RailBottom",
				SideTextures = "RailSide",
				TextureIdForInventory = "Rail2",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				Rail = 2,
			});
			m.SetBlockType(179, "Rail3", new BlockType()
			{
				TextureIdTop = "Rail3",
				TextureIdBottom = "RailBottom",
				SideTextures = "RailSide",
				TextureIdForInventory = "Rail3",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				Rail = 3,
			});
			m.SetBlockType(180, "Rail4", new BlockType()
			{
				TextureIdTop = "Rail4",
				TextureIdBottom = "RailBottom",
				SideTextures = "RailSide",
				TextureIdForInventory = "Rail4",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				Rail = 4,
			});
			m.SetBlockType(181, "Rail5", new BlockType()
			{
				TextureIdTop = "Rail5",
				TextureIdBottom = "RailBottom",
				SideTextures = "RailSide",
				TextureIdForInventory = "Rail5",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				Rail = 8,
			});
			m.SetBlockType(182, "Rail6", new BlockType()
			{
				TextureIdTop = "Rail6",
				TextureIdBottom = "RailBottom",
				SideTextures = "RailSide",
				TextureIdForInventory = "Rail6",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				Rail = 16,
			});
			m.SetBlockType(183, "Rail7", new BlockType()
			{
				TextureIdTop = "Rail7",
				TextureIdBottom = "RailBottom",
				SideTextures = "RailSide",
				TextureIdForInventory = "Rail7",
				DrawType = DrawType.Solid,
				WalkableType = WalkableType.Solid,
				Sounds = solidSounds,
				Rail = 32,
			});
			m.SetBlockType(241, "RedCarpet", new BlockType()
			{
				AllTextures = "RedCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(242, "OrangeCarpet", new BlockType()
			{
				AllTextures = "OrangeCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(243, "YellowCarpet", new BlockType()
			{
				AllTextures = "YellowCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(244, "LightGreenCarpet", new BlockType()
			{
				AllTextures = "LightGreenCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(245, "GreenCarpet", new BlockType()
			{
				AllTextures = "GreenCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(247, "CyanCarpet", new BlockType()
			{
				AllTextures = "CyanCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(246, "LightBlueCarpet", new BlockType()
			{
				AllTextures = "LightBlueCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(248, "BlueCarpet", new BlockType()
			{
				AllTextures = "BlueCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(249, "PurpleCarpet", new BlockType()
			{
				AllTextures = "PurpleCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(250, "BrownCarpet", new BlockType()
			{
				AllTextures = "BrownCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(251, "SilverCarpet", new BlockType()
			{
				AllTextures = "SilverCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(252, "MagentaCarpet", new BlockType()
			{
				AllTextures = "MagentaCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(253, "PinkCarpet", new BlockType()
			{
				AllTextures = "PinkCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(254, "BlackCarpet", new BlockType()
			{
				AllTextures = "BlackCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(255, "GrayCarpet", new BlockType()
			{
				AllTextures = "GrayCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			m.SetBlockType(256, "WhiteCarpet", new BlockType()
			{
				AllTextures = "WhiteCloth",
				DrawType = DrawType.Flat,
				WalkableType = WalkableType.Empty,
				Sounds = solidSounds
			});
			#endregion

			#region Creative inventory
			m.AddToCreativeInventory("Stone");
			m.AddToCreativeInventory("Dirt");
			m.AddToCreativeInventory("Cobblestone");
			m.AddToCreativeInventory("OakWood");
			m.AddToCreativeInventory("OakSapling");
			// m.AddToCreativeInventory("Adminium");
			m.AddToCreativeInventory("Water");
			m.AddToCreativeInventory("Lava");
			m.AddToCreativeInventory("Sand");
			m.AddToCreativeInventory("Gravel");
			m.AddToCreativeInventory("DiamondOre");
			m.AddToCreativeInventory("GoldOre");
			m.AddToCreativeInventory("EmeraldOre");
			m.AddToCreativeInventory("LapisOre");
			m.AddToCreativeInventory("IronOre");
			m.AddToCreativeInventory("CoalOre");
			m.AddToCreativeInventory("OakTreeTrunk");
			m.AddToCreativeInventory("OakLeaves");
			m.AddToCreativeInventory("Sponge");
			m.AddToCreativeInventory("Glass");
			m.AddToCreativeInventory("RedCloth");
			m.AddToCreativeInventory("OrangeCloth");
			m.AddToCreativeInventory("YellowCloth");
			m.AddToCreativeInventory("LightGreenCloth");
			m.AddToCreativeInventory("GreenCloth");
			m.AddToCreativeInventory("CyanCloth");
			m.AddToCreativeInventory("LightBlueCloth");
			m.AddToCreativeInventory("BlueCloth");
			m.AddToCreativeInventory("PurpleCloth");
			m.AddToCreativeInventory("BrownCloth");
			m.AddToCreativeInventory("SilverCloth");
			m.AddToCreativeInventory("MagentaCloth");
			m.AddToCreativeInventory("PinkCloth");
			m.AddToCreativeInventory("BlackCloth");
			m.AddToCreativeInventory("GrayCloth");
			m.AddToCreativeInventory("WhiteCloth");
			m.AddToCreativeInventory("RedCarpet");
			m.AddToCreativeInventory("OrangeCarpet");
			m.AddToCreativeInventory("YellowCarpet");
			m.AddToCreativeInventory("LightGreenCarpet");
			m.AddToCreativeInventory("GreenCarpet");
			m.AddToCreativeInventory("CyanCarpet");
			m.AddToCreativeInventory("LightBlueCarpet");
			m.AddToCreativeInventory("BlueCarpet");
			m.AddToCreativeInventory("PurpleCarpet");
			m.AddToCreativeInventory("BrownCarpet");
			m.AddToCreativeInventory("SilverCarpet");
			m.AddToCreativeInventory("MagentaCarpet");
			m.AddToCreativeInventory("PinkCarpet");
			m.AddToCreativeInventory("BlackCarpet");
			m.AddToCreativeInventory("GrayCarpet");
			m.AddToCreativeInventory("WhiteCarpet");
			m.AddToCreativeInventory("Dandelion");
			m.AddToCreativeInventory("Rose");
			m.AddToCreativeInventory("RedMushroom");
			m.AddToCreativeInventory("BrownMushroom");
			m.AddToCreativeInventory("DiamondBlock");
			m.AddToCreativeInventory("GoldBlock");
			m.AddToCreativeInventory("EmeraldBlock");
			m.AddToCreativeInventory("LapisBlock");
			m.AddToCreativeInventory("IronBlock");
			m.AddToCreativeInventory("DoubleStair");
			m.AddToCreativeInventory("Stair");
			m.AddToCreativeInventory("Brick");
			m.AddToCreativeInventory("Bookcase");
			m.AddToCreativeInventory("MossyCobblestone");
			m.AddToCreativeInventory("Obsidian");
			m.AddToCreativeInventory("Torch");
			m.AddToCreativeInventory("BrushedMetal");
			m.AddToCreativeInventory("ChemicalGreen");
			m.AddToCreativeInventory("Salt");
			m.AddToCreativeInventory("StoneRoof");
			m.AddToCreativeInventory("Camouflage");
			m.AddToCreativeInventory("DirtForFarming");
			m.AddToCreativeInventory("Apples");
			m.AddToCreativeInventory("Hay");
			m.AddToCreativeInventory("Wheat");
			m.AddToCreativeInventory("CraftingTable");
			m.AddToCreativeInventory("Trampoline");
			// m.AddToCreativeInventory("FillStart");
			// m.AddToCreativeInventory("Cuboid");
			// m.AddToCreativeInventory("FillArea");
			// m.AddToCreativeInventory("GrassTrap");
			m.AddToCreativeInventory("WoodWindow");
			m.AddToCreativeInventory("IronWindow");
			m.AddToCreativeInventory("IronBars");
			m.AddToCreativeInventory("DirtBrick");
			m.AddToCreativeInventory("WoodRoof");
			m.AddToCreativeInventory("SandBrick");
			m.AddToCreativeInventory("FakeBookcase");
			m.AddToCreativeInventory("WoodDesk");
			m.AddToCreativeInventory("GlassDesk");
			m.AddToCreativeInventory("Mosaic");
			m.AddToCreativeInventory("Asphalt");
			m.AddToCreativeInventory("Cake");
			m.AddToCreativeInventory("Fire");
			m.AddToCreativeInventory("Fence");
			m.AddToCreativeInventory("Compass");
			m.AddToCreativeInventory("Ladder");
			m.AddToCreativeInventory("Rail1");
			m.AddToCreativeInventory("Rail3");
			m.AddToCreativeInventory("Rail6");
			m.AddToCreativeInventory("Clay");
			m.AddToCreativeInventory("Marble");
			m.AddToCreativeInventory("Granite");
			m.AddToCreativeInventory("Sandstone");
			m.AddToCreativeInventory("RedSand");
			m.AddToCreativeInventory("RedSandstone");
			m.AddToCreativeInventory("Cactus");
			m.AddToCreativeInventory("DeadPlant");
			m.AddToCreativeInventory("GrassPlant");
			m.AddToCreativeInventory("BirchTreeTrunk");
			m.AddToCreativeInventory("BirchLeaves");
			m.AddToCreativeInventory("BirchSapling");
			m.AddToCreativeInventory("BirchWood");
			m.AddToCreativeInventory("SpruceTreeTrunk");
			m.AddToCreativeInventory("SpruceLeaves");
			m.AddToCreativeInventory("SpruceSapling");
			m.AddToCreativeInventory("SpruceWood");
			m.AddToCreativeInventory("HalfCobblestone");
			m.AddToCreativeInventory("HalfMossyCobblestone");
			m.AddToCreativeInventory("HalfOakWood");
			m.AddToCreativeInventory("HalfBirchWood");
			m.AddToCreativeInventory("HalfSpruceWood");
			m.AddToCreativeInventory("HalfBrick");
			m.AddToCreativeInventory("HalfSandBrick");
			#endregion

			#region Start inventory
			m.AddToStartInventory("Torch", 6);
			m.AddToStartInventory("Wheat", 1);
			m.AddToStartInventory("CraftingTable", 6);
			m.AddToStartInventory("Compass", 1);
			#endregion
		}

		int lastseason;
		void UpdateSeasons()
		{
			int currentSeason = m.GetSeason();
			if (currentSeason != lastseason)
			{
				// spring
				if (currentSeason == 0)
				{
					m.SetBlockType(2, "Grass", new BlockType()
					{
						TextureIdTop = "SpringGrass",
						TextureIdBack = "SpringGrassSide",
						TextureIdFront = "SpringGrassSide",
						TextureIdLeft = "SpringGrassSide",
						TextureIdRight = "SpringGrassSide",
						TextureIdForInventory = "SpringGrassSide",
						TextureIdBottom = "Dirt",
						DrawType = DrawType.Solid,
						WalkableType = WalkableType.Solid,
						Sounds = snowSounds,
						WhenPlayerPlacesGetsConvertedTo = 3,
					});
					m.SetBlockType(18, "OakLeaves", new BlockType()
					{
						AllTextures = "OakLeaves",
						DrawType = DrawType.Transparent,
						WalkableType = WalkableType.Solid,
						Sounds = solidSounds,
					});
					m.SetBlockType(106, "Apples", new BlockType()
					{
						AllTextures = "Apples",
						DrawType = DrawType.Transparent,
						WalkableType = WalkableType.Solid,
						Sounds = solidSounds,
						IsUsable = true,
					});
				}
				// summer
				if (currentSeason == 1)
				{
					m.SetBlockType(2, "Grass", new BlockType()
					{
						TextureIdTop = "Grass",
						TextureIdBack = "GrassSide",
						TextureIdFront = "GrassSide",
						TextureIdLeft = "GrassSide",
						TextureIdRight = "GrassSide",
						TextureIdForInventory = "GrassSide",
						TextureIdBottom = "Dirt",
						DrawType = DrawType.Solid,
						WalkableType = WalkableType.Solid,
						Sounds = solidSounds,
						WhenPlayerPlacesGetsConvertedTo = 3,
					});
					m.SetBlockType(18, "OakLeaves", new BlockType()
					{
						AllTextures = "OakLeaves",
						DrawType = DrawType.Transparent,
						WalkableType = WalkableType.Solid,
						Sounds = solidSounds,
					});
					m.SetBlockType(106, "Apples", new BlockType()
					{
						AllTextures = "Apples",
						DrawType = DrawType.Transparent,
						WalkableType = WalkableType.Solid,
						Sounds = solidSounds,
						IsUsable = true,
					});
					m.SetBlockType(8, "Water", new BlockType()
					{
						AllTextures = "Water",
						DrawType = DrawType.Fluid,
						WalkableType = WalkableType.Fluid,
						Sounds = noSound,
					});
				}
				// autumn
				if (currentSeason == 2)
				{
					m.SetBlockType(2, "Grass", new BlockType()
					{
						TextureIdTop = "AutumnGrass",
						TextureIdBack = "AutumnGrassSide",
						TextureIdFront = "AutumnGrassSide",
						TextureIdLeft = "AutumnGrassSide",
						TextureIdRight = "AutumnGrassSide",
						TextureIdForInventory = "AutumnGrassSide",
						TextureIdBottom = "Dirt",
						DrawType = DrawType.Solid,
						WalkableType = WalkableType.Solid,
						Sounds = snowSounds,
						WhenPlayerPlacesGetsConvertedTo = 3,
					});
					m.SetBlockType(18, "OakLeaves", new BlockType()
					{
						AllTextures = "AutumnLeaves",
						DrawType = DrawType.Transparent,
						WalkableType = WalkableType.Solid,
						Sounds = solidSounds,
					});
					m.SetBlockType(106, "Apples", new BlockType()
					{
						AllTextures = "AutumnApples",
						DrawType = DrawType.Transparent,
						WalkableType = WalkableType.Solid,
						Sounds = solidSounds,
						IsUsable = true,
					});
				}
				// winter
				if (currentSeason == 3)
				{
					m.SetBlockType(2, "Grass", new BlockType()
					{
						TextureIdTop = "WinterGrass",
						TextureIdBack = "WinterGrassSide",
						TextureIdFront = "WinterGrassSide",
						TextureIdLeft = "WinterGrassSide",
						TextureIdRight = "WinterGrassSide",
						TextureIdForInventory = "WinterGrassSide",
						TextureIdBottom = "Dirt",
						DrawType = DrawType.Solid,
						WalkableType = WalkableType.Solid,
						Sounds = snowSounds,
						WhenPlayerPlacesGetsConvertedTo = 3,
					});
					m.SetBlockType(18, "OakLeaves", new BlockType()
					{
						AllTextures = "WinterLeaves",
						DrawType = DrawType.Transparent,
						WalkableType = WalkableType.Solid,
						Sounds = solidSounds,
					});
					m.SetBlockType(106, "Apples", new BlockType()
					{
						AllTextures = "WinterApples",
						DrawType = DrawType.Transparent,
						WalkableType = WalkableType.Solid,
						Sounds = solidSounds,
						IsUsable = true,
					});
					m.SetBlockType(8, "Water", new BlockType()
					{
						AllTextures = "Ice",
						DrawType = DrawType.Solid,
						WalkableType = WalkableType.Solid,
						Sounds = snowSounds,
						IsSlipperyWalk = true,
					});
				}

				//Send updated BlockTypes to players
				m.UpdateBlockTypes();
				lastseason = currentSeason;

				//Readd "lost blocks" to inventory
				m.AddToCreativeInventory("OakLeaves");
				m.AddToCreativeInventory("Apples");
				m.AddToCreativeInventory("Water");
			}
		}
	}
}
