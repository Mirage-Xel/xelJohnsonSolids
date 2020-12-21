using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using rnd = UnityEngine.Random;
public class JohnsonSolids: MonoBehaviour {
    public GameObject[] solids;
    public KMSelectable[] upArrows;
    public KMSelectable[] downArrows;
    public KMSelectable j;
    public KMSelectable submit;
    public TextMesh[] displays;
    int chosenSolidIndex;
    int[] displayIndices = new int[2];
    string[] solidNames = new string[] { "Square Pyramid", "Pentagonal Pyramid", "Triangular Cupola", "Square Cupola", "Pentagonal Cupola", "Pentagonal Rotunda", "Elongated Triangular Pyramid", "Elongated Square Pyramid", "Elongated Pentagonal Pyramid", "Gyroelongated Square Pyramid", "Gyroelongated Pentagonal Pyramid", "Triangular Bipyramid", "Pentagonal Bipyramid", "Elongated Triangular Bipyramid", "Elongated Square Bipyramid", "Elongated Pentagonal Bipyramid", "Gyroelongated Square Bipyramid", "Elongated Triangular Cupola", "Elongated Square Cupola", "Elongated Pentagonal Cupola", "Elongated Pentagonal Rotunda", "Gyroelongated Triangular Cupola", "Gyroelongated Square Cupola", "Gyroelongated Pentagonal Cupola", "Gyroelongated Pentagonal Rotunda", "Gyrobifastigium", "Triangular Orthobicupola", "Square Orthobicupola", "Square Gyrobicupola", "Pentagonal Orthobicupola", "Pentagonal Gyrobicupola", "Pentagonal Orthocupolarontunda", "Pentagonal Gyrocupolarotunda", "Pentagonal Orthobirotunda", "Elongated Triangular Orthobicupola", "Elongated Triangular Gyrobicupola", "Elongated Square Gyrobicupola", "Elongated Pentagonal Orthobicupola", "Elongated Pentagonal Gyrobicupola", "Elongated Pentagonal Orthocupolarotunda", "Elongated Pentagonal Gyrocupolarotunda", "Elongated Pentagonal Orthobirotunda", "Elongated Pentagonal Gyrobirotunda", "Gyroelongated Triangular Bicupola", "Gyroelongated Square Bicupola", "Gyroelongated Pentagonal Bicupola", "Gyroelongated Pentagonal Cupolarotunda", "Gyroelongated Pentagonal Birotunda", "Augmented Triangular Prism", "Biaugmented Triangular Prism", "Triaugmented Triangular Prism", "Augmented Pentagonal Prism", "Biaugmented Pentagonal Prism", "Augmented Hexagonal Prism", "Parabiaugmented Hexagonal Prism", "Metabiaugmented Hexagonal Prism", "Triaugmented Hexagonal Prism", "Augmented Dodecahedron", "Parabiaugmented Dodecahedron", "Metabiaugmented Dodecahedron", "Triaugmented Dodecahedron", "Metabidiminished Icosahedron", "Tridiminished Icosahedron", "Augmented Tridiminished Icosahedron", "Augmented Truncated Tetrahedron", "Augmented Truncated Cube", "Biaugmented Truncated Cube", "Augmented Truncated Dodecahedron", "Parabiaugmented Truncated Dodecahedron", "Metabiaugmented Truncated Dodecahedron", "Triaugmented Truncated Dodecahedron", "Gyrate Rhombicosidodecahedron", "Parabigyrate Rhombicosidodecahedron", "Metabigyrate Rhombicosidodecahedron", "Trigyrate Rhombicosidodecahedron", "Diminished Rhombicosidodecahedron", "Paragyrate Diminished Rhombicosidodecahedron", "Metagyrate Diminished Rhombicosidodecahedron", "Bigyrate Diminished Rhombicosidodecahedron", "Parabidiminished Rhombicosidodecahedron", "Metabidiminished Rhombicosidodecahedron", "Gyrate Bidiminished Rhombicosidodecahedron", "Tridiminished Rhombicosidodecahedron", "Snub Disphenoid", "Snub Square Antiprism", "Sphenocorona", "Augmented Sphenocorona", "Sphenomegacorona", "Hebesphenomegacorona", "Disphenocingulum", "Bilunabirotunda", "Triangular Hebesphenorotunda" };
    public KMBombModule module;
    public KMAudio sound;
    int moduleId;
    static int moduleIdCounter = 1;
    bool solved;
    void Awake()
    {
        moduleId = moduleIdCounter++;
        for (int i = 0; i < 2; i++)
        {
            int j = i;
            upArrows[i].OnInteract += delegate { PressArrow(true, j); return false; };
            downArrows[i].OnInteract += delegate { PressArrow(false, j); return false; };
        }
        j.OnInteract += delegate { j.AddInteractionPunch(); sound.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform); solids[chosenSolidIndex].SetActive(!solids[chosenSolidIndex].activeSelf); return false; };
        submit.OnInteract += delegate { PressSubmit(); return false; };
        chosenSolidIndex = rnd.Range(0, 92);
        solids[chosenSolidIndex].SetActive(true);
        Debug.LogFormat("[Johnson Solids #{0}] The chosen solid is J{1}, which is the {2}.", moduleId, chosenSolidIndex + 1, solidNames[chosenSolidIndex]);
    }
    // Use this for initialization
    void PressArrow(bool arrow, int index) {
        if (!solved)
        {
            sound.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
            if (arrow)
            {
                upArrows[index].AddInteractionPunch();
                displayIndices[index]++;
                if (displayIndices[index] == 10)
                {
                    displayIndices[index] = 0;
                }
                displays[index].text = displayIndices[index].ToString();
            }
            else
            {
                downArrows[index].AddInteractionPunch();
                displayIndices[index]--;
                if (displayIndices[index] == -1)
                {
                    displayIndices[index] = 9;
                }
                displays[index].text = displayIndices[index].ToString();
            }
        }
    }
	
	// Update is called once per frame
	void PressSubmit () {
		if (!solved)
        {
            sound.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
            submit.AddInteractionPunch();
            Debug.LogFormat("[Johnson Solids #{0}] You submitted J{1}.", moduleId, displays[0].text + displays[1].text);
            if (int.Parse(displays[0].text + displays[1].text) == chosenSolidIndex + 1)
            {
                solved = true;
                sound.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                module.HandlePass();
                Debug.LogFormat("[Johnson Solids #{0}] That was correct. Module solved.", moduleId);
            }
            else
            {
                Debug.LogFormat("[Johnson Solids #{0}] That was incorrect. Strike!", moduleId);
                module.HandleStrike();
                solids[chosenSolidIndex].SetActive(false);
                chosenSolidIndex = rnd.Range(0, 92);
                solids[chosenSolidIndex].SetActive(true);
                Debug.LogFormat("[Johnson Solids #{0}] The chosen solid is J{1}, which is the {2}.", moduleId, chosenSolidIndex + 1, solidNames[chosenSolidIndex]);
            }
        }
	}
#pragma warning disable 414
    private string TwitchHelpMessage = @"use e.g. '!{0} submit J00' to submit your solid. use '!{0} press j' to press the J.";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand (string command)
    {
        int uselessVariable;
        command = command.ToLowerInvariant();
        string[] cmdArray = command.Split(' ');
        if (command == "press j")
        {
            yield return new WaitForSeconds(0.25f);
            j.OnInteract();
        }
        if (cmdArray.Length != 2 || !(cmdArray[0] == "submit") || cmdArray[1].Length != 3 || cmdArray[1][0] != 'j' || !int.TryParse(cmdArray[1][1].ToString(), out uselessVariable) || !int.TryParse(cmdArray[1][2].ToString(), out uselessVariable))
        {
            yield return "sendtochaterror Invalid command.";
            yield break;
        }
        else
        {
            while (displays[0].text != cmdArray[1][1].ToString())
            {
                yield return new WaitForSeconds(0.25f);
                upArrows[0].OnInteract();
            }
            while(displays[1].text != cmdArray[1][2].ToString())
            {
                yield return new WaitForSeconds(0.25f);
                upArrows[1].OnInteract();
            }
            yield return new WaitForSeconds(0.25f);
            submit.OnInteract();
        }

    }
}
