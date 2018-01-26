using UnityEngine;

namespace Battlehub.RTSaveLoad2
{
    //show wizard during project import
    //wizard would ask to create persistent objects and field mapping objects
    //user could choose accept predefined set of types, 
    //selected/unselect additional types or skip generation.
    
    //case 1 - Fresh Install:
    //wizard will display unity object fields to the left
    //and matched persistent objects fields to the right
    //case 2 - Upgrade: 

    //peristent objects will use primitive types and standard .net types or persistent objects
    //to store saved data

    public class PersistentObject 
    {
        public string name;
        public uint hideFlags;
    }

}
