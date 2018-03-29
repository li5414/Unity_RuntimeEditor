using System;
using System.Collections;

namespace Battlehub.RTEditor
{
    public class ListEditor : IListEditor
    {
        public ListEditor()
        {

        }


        protected override IList Resize(IList list, int size)
        {
            int delta = size - list.Count;
            bool remove = delta < 0;

            IList newList = (IList)Activator.CreateInstance(MemberInfoType, list);

            Type elementType = MemberInfoType.GetGenericArguments()[0];

            if (remove)
            {
                for(int i = 0; i < -delta; ++i)
                {
                    newList.RemoveAt(newList.Count - 1);
                }
            }
            else
            {
                for(int i = 0; i < delta; ++i)
                {
                    newList.Add(Reflection.GetDefault(elementType));
                }
            }

            return newList;
        }
    }
}

