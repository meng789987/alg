using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: counting sort, radix sort
 * Time(n), Space(n)
 */
namespace alg.tree
{
    public class BinarySearchTree
    {
        public IList<int> InorderTraversal(TreeNode root)
        {
            var ret = new List<int>();
            InorderTraversalRc(root, ret);
            return ret;
        }

        void InorderTraversalRc(TreeNode root, IList<int> res)
        {
            if (root == null) return;
            InorderTraversalRc(root.Left, res);
            res.Add(root.Value);
            InorderTraversalRc(root.Right, res);
        }

        public IList<int> InorderTraversalStack(TreeNode root)
        {
            var ret = new List<int>();
            var stack = new Stack<TreeNode>();
            var curr = root;

            while (curr != null || stack.Count > 0)
            {
                while (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.Left;
                }
                var node = stack.Pop();
                ret.Add(node.Value); // visit
                curr = node.Right; // move to its right
            }

            return ret;
        }

        /* 
         * Space(1), will modify the tree
         * if curr has no left then visit it, otherwise make curr as its predecessor's right
         * after done, all nodes have only right child with missing some nodes.
         */
        public IList<int> InorderTraversalMorris(TreeNode root)
        {
            var ret = new List<int>();
            var stack = new Stack<TreeNode>();
            var curr = root;

            while (curr != null)
            {
                if (curr.Left == null)
                {
                    ret.Add(curr.Value); // visit
                    curr = curr.Right;
                }
                else
                {
                    var pre = curr.Left;
                    while (pre.Right != null) pre = pre.Right; // find rightmost
                    pre.Right = curr;
                    var tmp = curr.Left;
                    curr.Left = null; // modify the tree
                    curr = tmp;
                }
            }

            return ret;
        }

        public IList<int> PreorderTraversaStack(TreeNode root)
        {
            var ret = new List<int>();
            var stack = new Stack<TreeNode>();
            if (root != null) stack.Push(root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                ret.Add(node.Value); // visit
                if (node.Right != null) stack.Push(node.Right);
                if (node.Left != null) stack.Push(node.Left);
            }

            return ret;
        }

        /*
         * tag: dp
         * Time(n^2), Space(n)
         * dp[n] = sum(dp[i]*dp[n-i-1]), i=[1..n]
         * select i as root
         * Given n, return the amount of unique BST's (binary search trees) that store values 1 ... n.
         */
        int UniqueTrees(int n)
        {
            var dp = new int[n + 1];
            dp[0] = dp[1] = 1;
            for (int len = 2; len <= n; len++)
            {
                for (int i = 0; i < len; i++)
                    dp[len] += dp[i] * dp[len - i - 1];
            }
            return dp[n];
        }

        public IList<int> LevelorderTraversal(TreeNode root)
        {
            var ret = new List<int>();
            var queue = new Queue<TreeNode>();
            if (root != null) queue.Enqueue(root);

            while (queue.Count > 0)
            {
                for (int i = queue.Count; i > 0; i--)
                {
                    var node = queue.Dequeue();
                    ret.Add(node.Value); // visit
                    if (node.Left != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);
                }
            }

            return ret;
        }

        public class TreeNode
        {
            public int Value;
            public TreeNode Left, Right;
            public TreeNode(int v) { Value = v; }
            public TreeNode(TreeNode root)
            {
                Value = root.Value;
                if (root.Left != null) Left = new TreeNode(root.Left);
                if (root.Right != null) Right = new TreeNode(root.Right);
            }
        }

        public void Test()
        {
            var root = new TreeNode(1)
            {
                Left = new TreeNode(2),
                Right = new TreeNode(3)
                {
                    Right = new TreeNode(4)
                }
            };
            var exp = new List<int> { 2, 1, 3, 4 };
            Console.WriteLine(exp.SequenceEqual(InorderTraversal(root)));
            Console.WriteLine(exp.SequenceEqual(InorderTraversalStack(root)));
            Console.WriteLine(exp.SequenceEqual(InorderTraversalMorris(new TreeNode(root))));

            exp = new List<int> { 1, 2, 3, 4 };
            Console.WriteLine(exp.SequenceEqual(PreorderTraversaStack(root)));

            exp = new List<int> { 1, 2, 3, 4 };
            Console.WriteLine(exp.SequenceEqual(LevelorderTraversal(root)));
        }
    }
}
