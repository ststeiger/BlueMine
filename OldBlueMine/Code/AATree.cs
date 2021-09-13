/*
 * https://stackoverflow.com/questions/1193477/fast-algorithm-to-quickly-find-the-range-a-number-belongs-to-in-a-set-of-ranges
 * https://raw.githubusercontent.com/Corey-M/Misc/master/Collections/Collections/AATree.cs
 * AATree https://github.com/Corey-M/Misc
 * 
 * Copyright (c) 2013 Corey Murtagh
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *
 */

/*
 * Attributions:
 * 
 * Code for this class was adapted from the following sources:
 * 
 * 1.	Author:		Aleksey Demakov
 *		Title:		Balanced Search Trees Made Simple (in C#)
 *		Source:		Aleksey Demakov's Web Corner
 *		URL:		http://demakov.com/snippets/aatree.html
 *		Licence:	Unknown
 *		(Archived by WebCite® at http://www.webcitation.org/6JAzpyrdW)
 *		
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;

namespace CoreyM.Collections
{
    public class AATree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        [DebuggerDisplay("{key}\t->{value} [{level}, {Parent.key}]")]
        public class Node
        {
            // node internal data
            internal int level;
            public Node Parent { get; internal set; }
            internal Node left;
            internal Node right;

            // user data
            internal TKey key;
            internal TValue value;

            // constuctor for regular nodes (that all start life as leaf nodes)
            internal Node(Node parent, TKey key, TValue value)
            {
                Contract.Requires(parent != null);
                this.level = 1;
                this.Parent = parent;
                this.left = null;
                this.right = null;
                this.key = key;
                this.value = value;
            }

            internal KeyValuePair<TKey, TValue> KeyValuePair
            {
                get { return new KeyValuePair<TKey, TValue>(key, value); }
            }

            internal Node LeftMost
            {
                get
                {
                    if (level < 1)
                        return null;
                    Node curr = this;
                    while (curr.left != null)
                        curr = curr.left;
                    return curr;
                }
            }

            internal Node RightMost
            {
                get
                {
                    if (level < 1)
                        return null;
                    Node curr = this;
                    while (curr.right != null)
                        curr = curr.right;
                    return curr;
                }
            }

            internal Node next
            {
                get
                {
                    if (right == null)
                    {
                        Node node = this;
                        while (true)
                        {
                            if (node.Parent == null)
                                return null;
                            if (node == node.Parent.left)
                                return node.Parent;
                            node = node.Parent;
                        }
                    }
                    else
                        return right.LeftMost;
                }
            }

            internal Node Previous
            {
                get
                {
                    if (left == null)
                    {
                        Node node = this;
                        while (true)
                        {
                            if (node.Parent == null)
                                return null;
                            if (node == node.Parent.right)
                                return node.Parent;
                            node = node.Parent;
                        }
                    }
                    else
                        return left.RightMost;
                }
            }
        }

        private IComparer<TKey> KeyComparer;
        private IComparer<TValue> ValueComparer;

        Node root;

        private AATree(IComparer<TKey> keyComparer, IComparer<TValue> valueComparer)
        {
            KeyComparer = keyComparer;
            ValueComparer = valueComparer;
        }

        public AATree()
            : this(Comparer<TKey>.Default, null)
        { }

        public AATree(IComparer<TValue> valComparer)
            : this(null, valComparer)
        { }

        public AATree(IComparer<TKey> keyComparer)
            : this(keyComparer, null)
        { }

        internal int Compare(Node l, Node r)
        {
            if (ValueComparer != null)
                return ValueComparer.Compare(l.value, r.value);

            return (KeyComparer ?? Comparer<TKey>.Default).Compare(l.key, r.key);
        }

        internal int Compare(TKey l, Node r)
        {
            return (KeyComparer ?? Comparer<TKey>.Default).Compare(l, r.key);
        }

        internal int Compare(TValue l, Node r)
        {
            return (ValueComparer ?? Comparer<TValue>.Default).Compare(l, r.value);
        }

        internal int Compare(TKey lk, TValue lv, Node r)
        {
            if (ValueComparer != null)
                return ValueComparer.Compare(lv, r.value);
            return (KeyComparer ?? Comparer<TKey>.Default).Compare(lk, r.key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (root == null)
                yield break;

            int n = 0;

            Node curr = root.LeftMost;
            do
            {
                yield return curr.KeyValuePair;
                curr = curr.next;
                ++n;
                if (n > 10000)
                    break;
            } while (curr != null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)this).GetEnumerator();
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return this.Select(kv => kv.Key);
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return this.Select(kv => kv.Value);
            }
        }

        /// <summary>Conditional rotate?</summary>
        /// <param name="node"></param>
        private void Skew(ref Node node)
        {
            if (node.left != null && node.level == node.left.level)
            {
                // [a L> b R> c] => [b R> a L> c]
                Node a = node;
                Node b = node.left;
                Node c = b.right;

                Node p = a.Parent;

                // swap a and b
                b.Parent = p;
                b.right = a;

                a.Parent = b;
                a.left = c;

                if (c != null)
                    c.Parent = a;

                node = b;
            }
        }

        private void Split(ref Node node)
        {
            if (node.right != null && node.right.right != null && node.right.right.level == node.level)
            {
                // [a R> b L> c] => [b L> a R> c]
                Node a = node;
                Node b = node.right;
                Node c = b.left;
                Node p = a.Parent;

                // swap a and b
                b.Parent = p;
                b.left = a;

                a.Parent = b;
                a.right = c;

                if (c != null)
                    c.Parent = a;

                if (b != null)
                    b.level++;
                node = b;
            }
        }

        private bool Insert(ref Node node, TKey key, TValue value, Node parent = null, int depth = 1)
        {
            Contract.Requires(depth < 100);

            if (node == null)
            {
                node = new Node(parent, key, value);
                CheckNode(node);
                return true;
            }

            //int compare = key.CompareTo(node.key);
            int compare = Compare(key, value, node);
            if (compare < 0)
            {
                if (!Insert(ref node.left, key, value, node, depth + 1))
                {
                    return false;
                }
            }
            else if (compare > 0)
            {
                if (!Insert(ref node.right, key, value, node, depth + 1))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            Skew(ref node);
            Split(ref node);

            return true;
        }

        private void CheckAll()
        {
            Node c = root == null ? null : root.LeftMost;
            while (c != null)
            {
                CheckNode(c);
                c = c.next;
            }
        }

        private void CheckNode(Node n)
        {
            if (n == null)
                return;
            Debug.Assert(n.Parent == null || n.Parent.left == n || n.Parent.right == n);
            Debug.Assert(n.left == null || n.left.Parent == n);
            Debug.Assert(n.right == null || n.right.Parent == n);

            if (ValueComparer != null)
            {
                Debug.Assert(n.left == null || ValueComparer.Compare(n.left.value, n.value) < 0);
                Debug.Assert(n.right == null || ValueComparer.Compare(n.right.value, n.value) > 0);
            }

            if (n.left != null)
            {
                Debug.Assert(n.left.left == null || n.left.left.Parent == n.left);
                Debug.Assert(n.left.right == null || n.left.right.Parent == n.left);
            }
            if (n.right != null)
            {
                Debug.Assert(n.right.left == null || n.right.left.Parent == n.right);
                Debug.Assert(n.right.right == null || n.right.right.Parent == n.right);
            }
        }

        private bool Delete(Node node)
        {
            //CheckNode(node);
            if (node == null)
                return false;

            int c = Count;

            Node p = node.Parent;

            if (node.left == null && node.right == null)
            {
                Console.Write(".");
                // Node is a leaf node.
                if (p != null)
                {
                    if (p.left == node)
                        p.left = null;
                    else
                        p.right = null;
                }
                else if (node == root)
                    root = null;

                node.Parent = node.left = node.right = null;
                return true;
            }

            if (node.left == null)
            {
                // Promote right
                Console.Write("<");

                if (p != null)
                {
                    if (p.left == node)
                        p.left = node.right;
                    else
                        p.right = node.right;
                }
                else if (node == root)
                    root = node.right;

                node.right.Parent = p;

                return true;
            }

            if (node.right == null)
            {
                // Promote left
                Console.Write(">");
                if (p != null)
                {
                    if (p.left == node)
                        p.left = node.left;
                    else
                        p.right = node.left;
                }
                else if (node == root)
                    root = node.left;

                node.left.Parent = p;

                return true;
            }

            // pick smallest child node from right and move key/value to this node
            Node least = node.right.LeftMost;
            Node leastp = least.Parent;

            // Move least right child's value here
            node.key = least.key;
            node.value = least.value;

            // now delete the small node
            if (node.right == least)
            {
                node.right = least.right;
                if (node.right != null)
                    node.right.Parent = node;
            }
            else if (least.right != null)
            {
                if (leastp.left == least)
                    leastp.left = least.right;
                else
                    leastp.right = least.right;

                least.right.Parent = leastp;
                least.right = null;
            }
            else
            {
                Debug.Assert(leastp.left == least);
                leastp.left = null;
            }

            return true;
        }

        public int Count { get { return this.Count(); } }

        private Node Search(Node node, TValue value)
        {
            if (node == null)
                return null;

            int compare = Compare(value, node);
            if (compare < 0)
                return Search(node.left, value);
            else if (compare > 0)
                return Search(node.right, value);

            return node;
        }

        private Node nSearch(Node node, TValue value)
        {
            Node c = root == null ? null : root.LeftMost;
            while (c != null)
            {
                if (Compare(value, c) == 0)
                    return c;
                c = c.next;
            }
            return null;
        }

        private Node Search(Node node, TKey key)
        {
            if (node == null)
                return null;

            //int compare = key.CompareTo(node.key);
            int compare = Compare(key, node);
            if (compare < 0)
            {
                return Search(node.left, key);
            }
            else if (compare > 0)
            {
                return Search(node.right, key);
            }
            else
            {
                return node;
            }
        }

        public bool Add(TKey key, TValue value)
        {
            return Insert(ref root, key, value);
        }

        public bool Remove(TKey key)
        {
            // return Delete(ref root, key);
            Node n = Search(root, key);
            if (n == null || KeyComparer.Compare(key, n.key) != 0)
                return false;
            return Delete(n);
        }

        public bool Remove(TValue val)
        {
            //return Delete(ref root, val);
            Node n = Search(root, val);
            if (n == null || ValueComparer.Compare(val, n.value) != 0)
                return false;

            bool res = Delete(n);
            return res;
        }

        public TValue this[TKey key]
        {
            get
            {
                Node node = Search(root, key);
                return node == null ? default(TValue) : node.value;
            }
            set
            {
                Node node = Search(root, key);
                if (node == null)
                {
                    Add(key, value);
                }
                else
                {
                    node.value = value;
                }
            }
        }

        public TValue First
        {
            get
            {
                if (root == null)
                    return default(TValue);
                return root.LeftMost.value;
            }
        }


    }


}
