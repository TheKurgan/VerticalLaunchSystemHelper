# VerticalLaunchSystemHelper

Simply add

MODULE
	{
		name = ModuleVLSLauncher
	}
to any missile launcher, turret, or VLS system

The module will assign some default ZERO values to the missile for the launch, then restore it's configured values 0.6 sec later.
The reason for creating this was to ensure a clean launch of the missile, and prevent it from turning pre-maturely and causing issues.
