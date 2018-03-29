namespace Battlehub.RTCommon
{

    public delegate void BeginDragEventHandler();
    public delegate void DropEventHandler();

    public static class DragDrop 
    {
        public static event BeginDragEventHandler BeginDrag;
        public static event DropEventHandler Drop;

        public static UnityEngine.Object[] DragItems
        {
            get;
            private set;
        }

        public static void Reset()
        {
            DragItems = null;
        }

        public static UnityEngine.Object DragItem
        {
            get
            {
                if (DragItems == null || DragItems.Length == 0)
                {
                    return null;
                }

                return DragItems[0];
            }
        }


        public static void RaiseBeginDrag(UnityEngine.Object[] dragItems)
        {
            DragItems = dragItems;

            if (BeginDrag != null)
            {
                BeginDrag();
            }
        }

        public static void RaiseDrop()
        {
            if(Drop != null)
            {
                Drop();
            }
            DragItems = null;
        }
    }

}
