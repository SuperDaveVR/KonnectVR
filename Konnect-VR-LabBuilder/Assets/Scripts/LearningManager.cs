using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LearningManager : MonoBehaviour
{

    private GameObject selectedObject;

    public TextMeshProUGUI explainationText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSelectedObject(GameObject grabbedObject)
    {
        selectedObject = grabbedObject;
    }

    public void processObject()
    {
        switch (selectedObject.tag)
        {
            case "Trabecula":
                explainationText.text = "This is the Trabecula. It is a cancellous or spongy bone tissue that is organized into trabeculae rather than osteons.  Trabeculae form a latticework that has spaces.";
                break;

            case "CompactBoneStructure":
                explainationText.text = "This is the Compact Bone Structure, also known as Circumferential lamellae. This is located deep to the periosteum, form the outer layer of compact bone.  This layer surrounds osteons, the structural and functional units of compact bone.";
                break;

            case "Periosteum":
                explainationText.text = "This is the Periosteum, a compact tissue. This refers to the tissue that covers the surface of bones. The outer layer is the fibrous layer. The fibrous layer contains blood vessels and nerves and is populated by fibroblasts.";
                break;

            case "Lacuna":
                explainationText.text = "This is the Lacuna, a compact tissue. This is a space, like a cave, located between concentric lamellae in an osteon.  Each lacuna contains an osteocyte, a bone cell derived from an osteoblast.";
                break;

            case "HaversianCanal":
                explainationText.text = "This is the Haversian Canal, apart of compact tissues. This is found in the middle of the osteon and contains blood vessels, lymphatic vessels and nerves.  These canals run from the proximal end of long bones toward the distal end.";
                break;

            case "VolkmannCanal":
                explainationText.text = "This is the Volkmann Canal, apart of compact tissues. This connects neighbouring central canals to one another and run perpendicular to them. Perforating canals contain blood vessels, lymphatic vessels and nerves.";
                break;
        }



    }
}
