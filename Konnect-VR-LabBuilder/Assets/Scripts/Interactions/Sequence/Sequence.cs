/*
 * @author Marty Fitzer
 * @coauthor Hissam Shaikh
 */

using UnityEngine;
using UnityEngine.Events;

namespace KonnectVR.Interactions.Sequence
{
    public class Sequence : MonoBehaviour
    {
        private SequenceEvent[] events;

        private int eventIndex = -1;
        private int EventIndex
        {
            get
            {
                return eventIndex;
            }
            set
            {
                //Index is valid
                if (value >= -1 && value <= events.Length)
                {
                    eventIndex = value;
                    OnEventIndexChange?.Invoke(eventIndex);
                }
            }
        }

        private SequenceEvent currentEvent
        {
            get
            {
                return events[EventIndex];
            }
        }

        public event UnityAction<int> OnEventIndexChange;

        [SerializeField]
        private UnityEvent onSequenceReset;
        public UnityEvent OnSequenceReset
        {
            get => onSequenceReset;
        }

        private void Start()
        {
            events = GetComponentsInChildren<SequenceEvent>();

            //Deactivate all events to start
            foreach (SequenceEvent sequenceEvent in events)
            {
                sequenceEvent.deactivate();
            }

            advanceSequence();
        }

        public void overrideEventIndex(int newEventIndex)
        {
            //Index is valid and is not the current event index
            if (newEventIndex >= -1 && newEventIndex <= events.Length && newEventIndex != EventIndex)
            {
                if (newEventIndex == -1 || newEventIndex == 0) //Go back to first event, reset whole sequence
                {
                    reset();
                }
                else
                {
                    deactivateCurrentEvent();

                    if (newEventIndex > EventIndex) //Force intermediate events to fire
                    {
                        for (int i = EventIndex; i < newEventIndex; i++)
                        {
                            events[i].forceFireEvent();
                        }
                    }
                    else //newEventIndex < EventIndex - Reset intermediate fired events
                    {
                        //Sequence is completed, go back to last event in sequence to avoid index out of range
                        if (EventIndex == events.Length)
                            eventIndex = events.Length - 1; //Not using EventIndex setter to avoid firing index change event more than necessary

                        for (int i = EventIndex; i >= newEventIndex; i--)
                        {
                            //Debug.Log("Resetting event: " + i);
                            events[i].reset();
                        }
                    }

                    EventIndex = newEventIndex;

                    activateCurrentEvent();
                }
            }
        }

        private void advanceSequence()
        {
            //Debug.Log("Advance sequence: " + EventIndex + " -> " + (EventIndex + 1));

            deactivateCurrentEvent();
            
            EventIndex++; //Go to next event

            activateCurrentEvent();
        }

        private void deactivateCurrentEvent()
        {
            if (EventIndex >= 0 && EventIndex < events.Length)
            {
                //Debug.Log("Deactivating event: " + EventIndex);

                currentEvent.OnEventFired.RemoveListener(advanceSequence);
                currentEvent.deactivate();
            }
        }

        private void activateCurrentEvent()
        {
            if (EventIndex < events.Length)
            {
                //Debug.Log("Activating event: " + EventIndex);
                
                currentEvent.OnEventFired.AddListener(advanceSequence);
                currentEvent.activate();
            }
        }

        public void reset()
        {
            Debug.Log("Resetting sequence");

            //Reset active event index
            if (EventIndex < events.Length)
                events[EventIndex].OnEventFired.RemoveListener(advanceSequence);
            EventIndex = -1;

            OnSequenceReset.Invoke();

            foreach (SequenceEvent sequenceEvent in events)
            {
                sequenceEvent.reset();
            }

            advanceSequence();
        }
    }
}
