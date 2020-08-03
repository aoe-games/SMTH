using UnityEngine;
using Cradle;
using System;

namespace StorylineSystem
{
    /*
     * Light version of TwineTextPlayer created by Richard Lalancette.
     */
	[ExecuteInEditMode]
	public class StoryPlayer : MonoBehaviour
    {
		public Story story;

        public event Action<StoryOutput> OnOutput = delegate { };
        public event Action<StoryOutput> OnOutputRemoved = delegate { };
        public event Action<StoryPassage> OnPassageDone = delegate { };
        public event Action<StoryPassage> OnPassageEnter = delegate { };
        public event Action<StoryPassage> OnPassageExit = delegate { };
        public event Action<StoryState> OnStateChanged = delegate { };

        void Start()
		{
            if (!Application.isPlaying)
            {
                return;
            }

            if (story == null)
            {
                story = GetComponent<Story>();
            }

			if (story == null)
			{
				Debug.LogError("Missing story to play. Add a story script to the StoryPlayer, or assign the Story variable of the StoryPlayer.");
				return;
			}

			story.OnPassageEnter += Story_OnPassageEnter;
            story.OnPassageExit += Story_OnPassageExit;
            story.OnPassageDone += Story_OnPassageDone;
            story.OnStateChanged += Story_OnStateChanged;
            story.OnOutput += Story_OnOutput;
			story.OnOutputRemoved += Story_OnOutputRemoved;
		}

        private void Story_OnPassageEnter(StoryPassage storyPassage)
        {
            OnPassageEnter(storyPassage);

            print("Story_OnPassageEnter: " + storyPassage.Name);
        }

        private void Story_OnPassageExit(StoryPassage storyPassage)
        {
            OnPassageExit(storyPassage);

            print("Story_OnPassageExit: " + storyPassage.Name);
        }

        private void Story_OnPassageDone(StoryPassage storyPassage)
        {
            OnPassageDone(storyPassage);

            print("Story_OnPassageDone: " + storyPassage.Name);
        }

        private void Story_OnStateChanged(StoryState newStoryState)
        {
            OnStateChanged(newStoryState);

            print("Story_onStateChanged: " + newStoryState.ToString());
        }

        private void Story_OnOutput(StoryOutput storyOutput)
        {
            OnOutput(storyOutput);

            // Debug information displayed here.
            if (storyOutput is StoryText)
            {
                print("Story_OnOutput StoryText");
            }
            else if (storyOutput is StoryLink)
            {
                print("Story_OnOutput StoryLink");
            }
            else if (storyOutput is LineBreak)
            {
                print("Story_OnOutput LineBreak");
            }
            else if (storyOutput is StyleGroup)
            {
                print("Story_OnOutput StyleGroup");
            }
        }

        private void Story_OnOutputRemoved(StoryOutput storyOutput)
        {
            OnOutputRemoved(storyOutput);

            // Debug information displayed here.
            if (storyOutput is StoryText)
            {
                print("Story_OnOutputRemoved StoryText");
            }
            else if (storyOutput is StoryLink)
            {
                print("Story_OnOutputRemoved StoryLink");
            }
            else if (storyOutput is LineBreak)
            {
                print("Story_OnOutputRemoved LineBreak");
            }
            else if (storyOutput is StyleGroup)
            {
                print("Story_OnOutputRemoved StyleGroup");
            }
        }

        void OnDestroy()
		{
            if (Application.isPlaying)
            {
                if (story != null)
                {
                    story.OnPassageEnter -= Story_OnPassageEnter;
                    story.OnPassageExit -= Story_OnPassageExit;
                    story.OnPassageDone -= Story_OnPassageDone;
                    story.OnStateChanged -= Story_OnStateChanged;
                    story.OnOutput -= Story_OnOutput;
                    story.OnOutputRemoved -= Story_OnOutputRemoved;
                }
            }
        }

#if UNITY_EDITOR
		void Update()
		{
            if (Application.isPlaying)
            {
                return;
            }
		}
#endif
	}
}