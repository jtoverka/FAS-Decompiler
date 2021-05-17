using System;
using System.Collections.ObjectModel;
using System.Collections;

namespace AutoLisp_GUI
{
    /// <summary>
    /// Library of functions and event handlers to do routine tasks
    /// </summary>
    public static class UILibrary
    {
        /// <summary>
        /// Move selected elements up the collection
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="collection"></param>
        public static void Up_Button_Click<T>(IList selection, ObservableCollection<T> collection)
        {
            int[] oldIndices = new int[selection.Count];
            int[] newIndices = new int[selection.Count];

            for (int i = 0; i < selection.Count; i++)
                oldIndices[i] = collection.IndexOf((T)selection[i]);

            Array.Sort(oldIndices);

            for (int i = 0; i < selection.Count; i++)
            {
                if (oldIndices[i] > i)
                    newIndices[i] = oldIndices[i] - 1;
                else
                    newIndices[i] = oldIndices[i];
            }
            Array.Sort(newIndices, oldIndices);

            for (int i = 0; i < selection.Count; i++)
            {
                if (oldIndices[i] != newIndices[i])
                    collection.Move(oldIndices[i], newIndices[i]);
            }
        }

        /// <summary>
        /// Move selected elements down the collection
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="collection"></param>
        public static void Down_Button_Click<T>(IList selection, ObservableCollection<T> collection)
        {
            int lastBoxIndex = collection.Count - 1;

            int[] oldIndices = new int[selection.Count];
            int[] newIndices = new int[selection.Count];

            for (int i = 0; i < selection.Count; i++)
                oldIndices[i] = collection.IndexOf((T)selection[i]);

            Array.Sort(oldIndices);

            for (int i = 0, j = selection.Count - 1; i < selection.Count; i++, j--)
            {
                if (oldIndices[i] < lastBoxIndex - j)
                    newIndices[i] = oldIndices[i] + 1;
                else
                    newIndices[i] = oldIndices[i];
            }

            Array.Sort(newIndices, oldIndices);

            for (int i = selection.Count - 1; i >= 0; i--)
            {
                if (oldIndices[i] != newIndices[i])
                    collection.Move(oldIndices[i], newIndices[i]);
            }
        }

        /// <summary>
        /// Move the selected elements to the top of the collection
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="collection"></param>
        public static void Top_Button_Click<T>(IList selection, ObservableCollection<T> collection)
        {
            int[] oldIndices = new int[selection.Count];
            int[] newIndices = new int[selection.Count];

            for (int i = 0; i < selection.Count; i++)
            {
                oldIndices[i] = collection.IndexOf((T)selection[i]);
                newIndices[i] = i;
            }
            Array.Sort(oldIndices);
            Array.Sort(newIndices, oldIndices);

            for (int i = 0; i < selection.Count; i++)
            {
                if (oldIndices[i] != newIndices[i])
                    collection.Move(oldIndices[i], newIndices[i]);
            }
        }

        /// <summary>
        /// Move the selected elements to the bottom of the collection
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="collection"></param>
        public static void Bottom_Button_Click<T>(IList selection, ObservableCollection<T> collection)
        {
            int lastBoxIndex = collection.Count - 1;

            int[] oldIndices = new int[selection.Count];
            int[] newIndices = new int[selection.Count];

            for (int i = 0, j = lastBoxIndex;
                i < selection.Count;
                i++, j--)
            {
                oldIndices[i] = collection.IndexOf((T)selection[i]);
                newIndices[i] = j;
            }
            Array.Sort(oldIndices);
            Array.Sort(newIndices);

            for (int i = selection.Count - 1; i >= 0; i--)
            {
                if (oldIndices[i] != newIndices[i])
                    collection.Move(oldIndices[i], newIndices[i]);
            }
        }
    }
}
