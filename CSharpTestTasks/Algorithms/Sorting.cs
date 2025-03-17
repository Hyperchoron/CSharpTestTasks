namespace CSharpTestTasks.Algorithms
{
    internal interface ISortMethod<T>
    {
        void Sort(T[] array);
    }

    internal class QuickSort : ISortMethod<char>
    {
        public void Sort(char[] array) => Quicksort(array, 0, array.Length - 1);

        private void Quicksort(char[] chars, int first, int last)
        {
            if (first >= last || first < 0) return;

            var p = QuicksortPartition(chars, first, last);

            Quicksort(chars, first, p - 1);
            Quicksort(chars, p + 1, last);
        }

        private int QuicksortPartition(char[] chars, int first, int last)
        {
            char pivot = chars[last];
            char temp;

            int i = first;
            for (int j = first; j < last; j++)
            {
                if (chars[j] <= pivot)
                {
                    temp = chars[i];

                    chars[i] = chars[j];
                    chars[j] = temp;

                    i++;
                }
            }

            temp = chars[i];

            chars[i] = chars[last];
            chars[last] = temp;

            return i;
        }
    }

    internal class TreeSort : ISortMethod<char>
    {
        public void Sort(char[] array)
        {
            BinaryTree searchTree = new();

            foreach (char c in array)
            {
                Insert(searchTree, c);
            }

            var sortedList = new List<char>();

            InOrder(sortedList, searchTree);

            sortedList.CopyTo(array);
        }

        private class BinaryTree
        {
            public BinaryTree? LeftSubTree { get; set; } = null;
            public BinaryTree? RightSubTree { get; set; } = null;

            public char? Node { get; set; } = null;
        }

        private void Insert(BinaryTree searchTree, char item)
        {
            if (searchTree.Node is null)
            {
                searchTree.Node = item;
            }
            else
            {
                if (item < searchTree.Node)
                {
                    searchTree.LeftSubTree ??= new BinaryTree();

                    Insert(searchTree.LeftSubTree, item);
                }
                else
                {
                    searchTree.RightSubTree ??= new BinaryTree();

                    Insert(searchTree.RightSubTree, item);
                }
            }
        }

        private void InOrder(List<char> chars, BinaryTree searchTree)
        {
            if (searchTree.Node is null)
                return;

            if (searchTree.LeftSubTree is not null)
                InOrder(chars, searchTree.LeftSubTree);

            chars.Add(searchTree.Node ?? '#');

            if (searchTree.RightSubTree is not null)
                InOrder(chars, searchTree.RightSubTree);
        }
    }
}
