using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Culture { English, Cornish, Scottish, Irish, Welsh, Faroese, French, Norweigan, Swedish, Danish, Dutch}
enum CountryName { England, Cornwall, Scotland, NorthernIreland, Ireland, Wales, Faroe, France, Aquitane, Brittany, Norway, Sweden, Denmark, Netherlands}

public class Nation {
    Color color;
    Culture culture;
    CountryName countryName;
}
