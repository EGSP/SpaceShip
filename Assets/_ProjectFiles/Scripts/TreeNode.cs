using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game
{
    public class TreeNode<TValue>
    {
        [NonSerialized]
        public TreeNode<TValue> Parent;
        
        public readonly LinkedList<TreeNode<TValue>> Childs;

        public readonly TValue Value;

        public TreeNode(TreeNode<TValue> parent,[NotNull] TValue value)
        {
            Parent = parent;
            Childs = new LinkedList<TreeNode<TValue>>();

            if (value == null)
                throw new ArgumentNullException();
            
            Value = value;
        }

        public void Add([NotNull] TValue value)
        {
            Childs.AddLast(new TreeNode<TValue>(this, value));
        }
        
        public void Add([NotNull] TreeNode<TValue> value)
        {
            Childs.AddLast(value);
        }

        [CanBeNull]
        public TreeNode<TValue> Find(Func<TreeNode<TValue>, bool> predicate, TreeNode<TValue> rootNode)
        {
            var queue = new Queue<TreeNode<TValue>>();
            queue.Enqueue(rootNode);
            
            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                if (predicate(next))
                    return next;
                
                foreach (var child in next.Childs)
                    queue.Enqueue(child);
            }

            return null;
        }

        // Left to right (old to new)
        public static IEnumerable<TValue> QueueTraverse(TreeNode<TValue> rootNode)
        {
            var queue = new Queue<TreeNode<TValue>>();
            queue.Enqueue(rootNode);
            
            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                yield return next.Value;
                foreach (var child in next.Childs)
                    queue.Enqueue(child);
            }
        }
        
        public static void Traverse(TreeNode<TValue> rootNode, Action<TreeNode<TValue>> actionOnNode)
        {
            var queue = new Queue<TreeNode<TValue>>();
            queue.Enqueue(rootNode);
            
            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                actionOnNode?.Invoke(next);
                foreach (var child in next.Childs)
                    queue.Enqueue(child);
            }
        }

        public static void ResolveRelations(TreeNode<TValue> rootNode)
        {
            Traverse(rootNode, x =>
            {
                foreach (var child in x.Childs)
                {
                    child.Parent = x;
                }
            });
        }
    }
}