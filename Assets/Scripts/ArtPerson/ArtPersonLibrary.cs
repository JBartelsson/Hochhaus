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

    Dictionary<Art.ArtPersonType, ArtPersonData> artPersonDataDictionary =
        new Dictionary<Art.ArtPersonType, ArtPersonData>();

    private void Start()
    {
        artPersonDataDictionary = artPersonData.ToDictionary((data => data.ArtPersonType));
    }


    public Art.ArtPerson CreateArtPerson(Art.ArtPersonType artPersonType)
    {
        Art.ArtPerson artPerson = null;
        Art.ArtFunctionBase artPersonFunction = null;
        ArtPersonData artPersonData = artPersonDataDictionary[artPersonType];
        Debug.Log($"ART PERSON TYPE TO STRING: " + artPersonType.ToString());

        string className = "Art.ArtPersonFunctions." + artPersonType.ToString();
        Type artPersonDataType = Type.GetType(className, true);
        Debug.Log($"ART PERSON TYPE: " + className);
        try
        {
            artPersonFunction = (Art.ArtFunctionBase)(Activator.CreateInstance(artPersonDataType));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        artPersonFunction.SetEffectData(artPersonData);

        artPerson = new Art.ArtPerson(artPersonFunction);
        artPerson.SetArtPersonData(artPersonData);

        return artPerson;
    }
}