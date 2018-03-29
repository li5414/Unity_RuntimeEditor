using System;
using System.Collections;

namespace Battlehub.RTEditor
{
    public class ArrayEditor : IListEditor
    {
        protected override IList Resize(IList list, int size)
        {
            Array newArray = (Array)Activator.CreateInstance(MemberInfoType, size);
            Array arr = (Array)list;
            if (arr != null)
            {
                for (int i = 0; i < newArray.Length; ++i)
                {
                    newArray.SetValue(arr.GetValue(Math.Min(i, arr.Length - 1)), i);
                }
            }
            return arr;
        }
    }
}

   