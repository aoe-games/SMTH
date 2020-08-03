using Cradle;
using UnityEngine;
using StorylineSystem;

// This is an example of story controller that can be attached to an object.
// That same object should have a StoryPLayer and a Story attached.
public class PrologueStoryController : MonoBehaviour
{
    private StoryPlayer _storyPlayer;

    private void Start()
    {
        _storyPlayer = GetComponentInParent<StoryPlayer>();
        _storyPlayer.story = GetComponentInParent<Story>();
        
        _storyPlayer.OnPassageEnter += StoryPlayerOnOnPassageEnter;
        _storyPlayer.OnPassageExit += StoryPlayerOnOnPassageExit;
        _storyPlayer.OnPassageDone += StoryPlayerOnOnPassageDone;
        _storyPlayer.OnStateChanged += StoryPlayerOnOnStateChanged;
        _storyPlayer.OnOutput += StoryPlayerOnOnOutput;
        _storyPlayer.OnOutputRemoved += StoryPlayerOnOnOutputRemoved;
        
        if (_storyPlayer.story)
        {
            _storyPlayer.story.Begin();
        }
    }

    private void StoryPlayerOnOnPassageEnter(StoryPassage storyPassage)
    {
    }

    private void StoryPlayerOnOnPassageExit(StoryPassage storyPassage)
    {
    }

    private void StoryPlayerOnOnPassageDone(StoryPassage storyPassage)
    {
    }

    private void StoryPlayerOnOnOutputRemoved(StoryOutput storyOutput)
    {
    }

    private void StoryPlayerOnOnOutput(StoryOutput storyOutput)
    {
    }

    private void StoryPlayerOnOnStateChanged(StoryState storyState)
    {
    }
}
