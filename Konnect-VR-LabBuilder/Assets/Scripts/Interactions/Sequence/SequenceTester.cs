/*
 * @author Marty Fitzer
 * @coauthor Hissam Shaikh
 */

using KonnectVR.Interactions.Sequence;
using UnityEngine;

[RequireComponent(typeof(Sequence))]
public class SequenceTester : MonoBehaviour
{
    private Sequence sequence;

    [SerializeField]
    private int eventIndex = 0;
    private int previousEventIndex = 0;

    [SerializeField]
    private bool reset = false;

    private void Start()
    {
        sequence = GetComponent<Sequence>();
    }

    private void Update()
    {
        if (previousEventIndex != eventIndex)
        {
            //Debug.Log("Overriding event index");

            sequence.overrideEventIndex(eventIndex);
            previousEventIndex = eventIndex;
        }

        if (reset)
        {
            sequence.reset();
            reset = false;
        }
    }
}
