using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Art;
using Art.ArtPersonFunctions;
using UnityEditor;
using UnityEngine;

public class ArtPersonLibrary : MonoBehaviour
{
    [SerializeField] List<ArtPersonData> artPersonData;
    Dictionary<Art.ArtPersonType, ArtPersonData> artPersonDataDictionary = new Dictionary<Art.ArtPersonType, ArtPersonData>();
    private void Start()
    {
        artPersonDataDictionary = artPersonData.ToDictionary((data => data.ArtPersonType ));
    }
    
  

    public Art.ArtPerson CreateArtPerson(Art.ArtPersonType artPersonType)
    {
        Art.ArtPerson artPerson = null;
        Art.ArtFunctionBase artPersonFunction = null;
        ArtPersonData artPersonData = artPersonDataDictionary[artPersonType];
        switch (artPersonType)
        {
            case Art.ArtPersonType.None:
                return null;
            case Art.ArtPersonType.TheBlue:
                artPersonFunction = new TheBlue();
                break;
            case Art.ArtPersonType.Repainter:
                artPersonFunction = new Repainter();
                break;
            case ArtPersonType.LoudNeighbors:
                artPersonFunction = new LoudNeighbors();
                break;
            case ArtPersonType.DrawingAssistant:
                artPersonFunction = new DrawingAssistant();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(artPersonType), artPersonType, null);
        }

        artPersonFunction.SetEffectData(artPersonData);

        artPerson = new Art.ArtPerson(artPersonFunction);
        artPerson.SetArtPersonData(artPersonData);
        
        return artPerson;
    }

    
}
