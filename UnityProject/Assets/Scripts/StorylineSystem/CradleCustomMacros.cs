using System;
using UnityEngine;

namespace StorylineSystem
{
    /// <summary>
    /// These are example of custom macros that can be used in a story.
    /// Running custom macros is built-in the language.
    /// We can remove these examples once we are familiar enough
    /// with the added features we want to drive from a story.
    /// </summary>
    /// 
    /// <example>
    /// In this example, all macros are within () the first term is the macro name,
    /// followed by ":" then the parameter list.
    /// (LoadDialogOverlayScene:"BaseSmith")
    /// (Dialog: "Derek") Ryan! Did you hear the commotion at the docks this morning?
    /// (Dialog: "Ryan") I did not! What happened?
    /// (Dialog: "Derek") 3rd day in a row the fishermen's guild find their nets cut to pieces.
    /// (Dialog: "Ryan") Seriously, in this quiet town?
    /// (Dialog: "Derek") Well, no so quiet anymore! Do you think this is our chance to investigate our first team case! Wooo!
    /// (Dialog: "Ryan")Calm down Derek! There's got to be a simple explanation to this. Let's chat with Brug, he might be able to shine some light on this.
    /// (Dialog: "Ryan")Do you mind heading there now, while I wrap up some small smith jobs?
    /// </example>
    public class CradleCustomMacros : Cradle.RuntimeMacros
    {
        public static event Action<Cradle.Story> OnCurrentQuestPaused = delegate { };
        public static event Action<Cradle.Story, string> OnQuestMadeAvailable = delegate { };
        public static event Action<Cradle.Story, string> OnLoadDialogOverlayScene = delegate { };
        public static event Action<Cradle.Story, string> OnLoadScene = delegate { };
        public static event Action<Cradle.Story, string> OnSfxPlay = delegate { };
        public static event Action<Cradle.Story, string> OnSfxStop = delegate { };
        public static event Action<Cradle.Story, string> OnWaitForMore = delegate { };
        public static event Action<Cradle.Story, string, string> OnCharacterNameChanged = delegate { };
        public static event Action<Cradle.Story, string, string> OnDialogAboveChanged = delegate { };
        public static event Action<Cradle.Story, string, string, int> OnCharacterInventoryChanged = delegate { };

        [Cradle.RuntimeMacro]
        public void PauseQuest()
        {
            Debug.Log("CradleCustomMacros PauseQuest");
            OnCurrentQuestPaused(Story);
        }

        [Cradle.RuntimeMacro]
        public void MakeQuestAvailable(string questName)
        {
            Debug.Log("CradleCustomMacros MakeQuestAvailable");
            OnQuestMadeAvailable(Story, questName);
        }

        [Cradle.RuntimeMacro]
        public void LoadDialogOverlayScene(string dialogOverlaySceneName)
        {
            Debug.Log("CradleCustomMacros LoadDialogOverlayScene");
            OnLoadDialogOverlayScene(Story, dialogOverlaySceneName);
        }

        [Cradle.RuntimeMacro]
        public void LoadScene(string sceneName)
        {
            Debug.Log("CradleCustomMacros LoadScene");
            OnLoadScene(Story, sceneName);
        }

        /*
        * This could play a sound effect from the storyline
        */
        [Cradle.RuntimeMacro]
        public void SfxPlay(string soundFXName)
        {
            Debug.Log("CradleCustomMacros SfxPlay");
            OnSfxPlay(Story, soundFXName);
        }

        [Cradle.RuntimeMacro]
        /*
        * This stops a sound effect from the storyline
        */
        public void SfxStop(string soundFXName)
        {
            Debug.Log("CradleCustomMacros SfxStop");
            OnSfxStop(Story, soundFXName);
        }

        /*
        * Pauses the story and wait for user input to resume
        */
        [Cradle.RuntimeMacro]
        public void More(string moreIconObjectName = "default")
        {
            Debug.Log("CradleCustomMacros More");
            // this.Story.Pause(); // Todo re-insert this once we can wait for more...
            OnWaitForMore(Story, moreIconObjectName);
        }

        /*
        Update the main story dialog window to display the characterNameTag and align using nameTagAlignment.
        Ideally, the dlg will automatically pause the story so the player will need to press a key/A button to continue.
        */
        [Cradle.RuntimeMacro]
        public void Dialog(string characterName, string nameAlignment = "<", string moreIconObjectName = "default")
        {
            Debug.Log("CradleCustomMacros Dialog");
            OnCharacterNameChanged(Story, characterName, nameAlignment);
            More(moreIconObjectName);
        }

        [Cradle.RuntimeMacro]
        public void DialogAbove(string characterName, string nameAlignment = "<", string moreIconObjectName = "default")
        {
            Debug.Log("CradleCustomMacros DialogAbove");
            OnDialogAboveChanged(Story, characterName, nameAlignment);
        }

        [Cradle.RuntimeMacro]
        public void CharacterInventoryChange(string characterName, string itemName, int quantity)
        {
            Debug.Log("CradleCustomMacros AddArmor " + characterName + ":" + itemName + ":" + quantity);
            OnCharacterInventoryChanged(Story, characterName, itemName, quantity);
        }
    }
}