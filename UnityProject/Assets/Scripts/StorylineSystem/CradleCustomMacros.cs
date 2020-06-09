using System;
using UnityEngine;

// These are example of custom macros that can be used in a story.
// Running custom macros is built-in the language.
// We can remove these examples once we are familiar enough
// with the added features we want to drive from a story point of view.
public class CradleCustomMacros : Cradle.RuntimeMacros
{
    static public event Action<Cradle.Story, string, string> OnCharacterNameChanged = delegate { };
    static public event Action<Cradle.Story, string, string> OnDlgAboveChanged = delegate { };
    static public event Action<Cradle.Story, string, string> OnCharacterArmorChanged = delegate { };

    /*
     * This could play a sound effect from the storyline
     */
    [Cradle.RuntimeMacro]
    public void sfxPlay(string soundName)
    {
        Debug.Log("CradleCustomMacros sfxPlay");
        // TODO
        // GameObject.Find(soundName).GetComponent<AudioSource>().Play();
    }

    [Cradle.RuntimeMacro]
    /*
     * This stops a sound effect from the storyline
     */
    public void sfxStop(string soundName)
    {
        // TODO
        Debug.Log("CradleCustomMacros sfxStop");
        // GameObject.Find(soundName).GetComponent<AudioSource>().Stop();
    }

    /*
     * Pauses the story and wait for user input to resume
     */
    [Cradle.RuntimeMacro]
    public void more(string moreIconObjectName = "default")
    {
        Debug.Log("CradleCustomMacros more");
        this.Story.Pause();
    }

    /*
        Update the main story dialog window to display the characterNameTag and align using nameTagAlignment.
        Ideally, the dlg will automatically pause the story so the player will need to press a key/A button to continue.
    */
    [Cradle.RuntimeMacro]
    public void dlg(string characterName, string nameAlignment = "<", string moreIconObjectName = "default")
    {
        Debug.Log("CradleCustomMacros dlg");
        OnCharacterNameChanged(this.Story, characterName, nameAlignment);
        more(moreIconObjectName);
    }

    /*
        Show a small dialog text or icon above the character on the map.
    */
    [Cradle.RuntimeMacro]
    public void dlgAbove(string characterName, string nameAlignment = "<", string moreIconObjectName = "default")
    {
        Debug.Log("CradleCustomMacros dlgAbove");
        OnDlgAboveChanged(this.Story, characterName, nameAlignment);
    }

    /*
        Equip a new piece of armor to the given character.
    */
    [Cradle.RuntimeMacro]
    public void addArmor(string characterName, string armorName)
    {
        Debug.Log("CradleCustomMacros addArmor " + characterName + ":" + armorName);
        OnCharacterArmorChanged(this.Story, characterName, armorName);
    }
}