# Free-RPG-Tutorial-with-Skyrim-Like-Skill-System-

Make sure to get Synty Studios “POLYGON - Starter Pack”, https://assetstore.unity.com/packages/3d/props/polygon-starter-pack-156819  
and
Explosive LLC “RPG Character Mecanim Animation Pack FREE”, https://assetstore.unity.com/packages/3d/animations/rpg-character-mecanim-animation-pack-free-65284 

without those, this project won't work. 

Really important that you clone in or at least copy the InputManager.asset into your ProjectSettings folder once you've got Explosive LLC's RPG Animation pack. It'll take away a lot of the issues there.

--------------------More issues with Animation pack:-----------------------------

LAYERS!

Fix the Input Manager - copy or clone this: https://github.com/KampinKarl1/Free-RPG-Tutorial-with-Skyrim-Like-Skill-System-/blob/main/ProjectSettings/InputManager.asset

Make sure you add the TempCast layer (index 8) and Walkable layer (index 9) to your project.
I'm not sure that the Shadow and Collide tags are used.

If you want your character to walk on MeshColliders, you need to put the BSPTree component on them.

If you're annoyed by the popup telling you to setup Input and Tag managers, go to the Code folder and delete the SetupInputTagsFREE.cs file.
----------------------------------------------------------------------------------

As of October 28th, 2020, the skills are pretty useless unless you know how to code them up for use with your attacks and abilities.

You could get your player to level up by calling SkillUser.GainExperience (float xp),
however that information is forthcoming with my next tutorials.

Let me know if you have any problems (or suggestions).

Watch the videos here: https://www.youtube.com/playlist?list=PL8uuriBnyf5n48pmtnL0jKCl7ITz_W5To to help you get these systems put together.


#How to Fix Explosive RPG Animation Pack
#Explosive RPG Anim Pack Input Fix


Nov. 7 2020 (Biden wins Pres. Elec. in USA) 
Changes to include targeting. 
----------------------------------------------------------------------------------
Added some code to CombatTest.cs to include targeting nearest enemy.
Made changes to InputController.cs to reference CombatTest to call for enemy targeting.

Changes on Explosive RPG Scripts:

  //--------------ADD THIS TO RPGCharacterMovementControllerFREE.cs SCRIPT!-----------------
        //Targeting Properties
        
        private Vector3 StrafeTarget => rpgCharacterController.HasTarget ? rpgCharacterController.TargetPos : CurrentLookDir;
        private Vector3 CurrentLookDir =>  new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) + transform.position;
        
        Around lines 150 and 250 change Strafing (controller.target.transform.position) to 
          Strafing(StrafeTarget);
          
  //--------------ADD THIS TO RPGCharacterControllerFREE.cs SCRIPT!-----------------
  
        public void SetTarget(GameObject t) => target = t;
        public bool HasTarget => target != null;
        public GameObject GetTarget() => target;
        public Vector3 TargetPos => target.transform.position;
        
   //--------------------------------------------------------------------------------
