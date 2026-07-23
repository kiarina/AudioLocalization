// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class AudioLocalization : ModuleRules
{
	public AudioLocalization(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[] {
			"Core",
			"CoreUObject",
			"Engine",
			"InputCore",
			"EnhancedInput",
			"AIModule",
			"StateTreeModule",
			"GameplayStateTreeModule",
			"UMG",
			"Slate",
			"Json"
		});

		PrivateDependencyModuleNames.AddRange(new string[] { });

		PublicIncludePaths.AddRange(new string[] {
			"AudioLocalization",
			"AudioLocalization/Variant_Platforming",
			"AudioLocalization/Variant_Platforming/Animation",
			"AudioLocalization/Variant_Combat",
			"AudioLocalization/Variant_Combat/AI",
			"AudioLocalization/Variant_Combat/Animation",
			"AudioLocalization/Variant_Combat/Gameplay",
			"AudioLocalization/Variant_Combat/Interfaces",
			"AudioLocalization/Variant_Combat/UI",
			"AudioLocalization/Variant_SideScrolling",
			"AudioLocalization/Variant_SideScrolling/AI",
			"AudioLocalization/Variant_SideScrolling/Gameplay",
			"AudioLocalization/Variant_SideScrolling/Interfaces",
			"AudioLocalization/Variant_SideScrolling/UI"
		});

		// Uncomment if you are using Slate UI
		// PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });

		// Uncomment if you are using online features
		// PrivateDependencyModuleNames.Add("OnlineSubsystem");

		// To include OnlineSubsystemSteam, add it to the plugins section in your uproject file with the Enabled attribute set to true
	}
}
