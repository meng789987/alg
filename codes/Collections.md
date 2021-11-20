
###System.Collections.Generic.ICollection&lt;T&gt;
+ Properties
  - .Count
+ Methods
  - Add(T)
  - Remove(T)
  - Clear()
  - Contains(T)
+ Known derivatives
  - IList&lt;T&gt;
    - List&lt;T&gt;
  - ISet&lt;T&gt;
    - HashSet&lt;T&gt;
    - SortedSet&lt;T&gt;
  - Dictionary&lt;T&gt;
  - SortedDictionary&lt;T&gt;
  - LinkedList&lt;T&gt;
  
###System.Collections.Generic.IList&lt;T&gt;
+ Extra
  - .Item[Int32]
  - IndexOf(T)
  - Insert(Int32, T)
  - RemoveAt(Int32)
  
###System.Collections.Generic.ISet&lt;T&gt;
ISet has some similar methods in LINQ but their names are ending with 'With'.

The primary difference between LINQ set operations and HashSet&lt;T&gt; operations is that LINQ set operations always return a new IEnumerable&lt;T&gt; collection, whereas the HashSet&lt;T&gt; equivalent methods modify the current collection.

LINQ | HashSet
------------- | -------------
Union  | UnionWith
Intersect | IntersectWith
Except | ExceptWith
Distinct | NA

<br/>
###Collections
The constructors of HashSet and Dictionary, and Linq.Distinct() can take an IEqualityComparer&lt;T&gt; object, whose implementation is recommended to derive from  EqualityComparer&lt;T&gt;, and must have correct GetHashCode() and Equals().

The constructors of SortedSet and SortedDictionary,  and BinarySearch() and Sort() in Array and List can take an IComparer&lt;T&gt; object, which can be created from a Lamda Comparison:
+ Comparer&lt;T&gt;.Create(Comparisont&lt;T&gt;)

The Sort() in Array and List can take a Lamda Comparison object.

HashSet | SortedSet | List | Array | LinkedList | Dictionary | SortedDictionary | Stack | Queue
------------- | ------------- | ------------- | ------------- | ------------- | ------------- | -------------
ICollection&lt;T&gt;<br/>&nbsp;.Count<br/>&nbsp;Add(T)<br/>&nbsp;Remove(T)<br/>&nbsp;Clear()<br/>&nbsp;Contains(T)  | ICollection&lt;T&gt;<br/>&nbsp;.Count<br/>&nbsp;Add(T)<br/>&nbsp;Remove(T)<br/>&nbsp;Clear()<br/>&nbsp;Contains(T)  | ICollection&lt;T&gt;<br/>&nbsp;.Count<br/>&nbsp;Add(T)<br/>&nbsp;Remove(T)<br/>&nbsp;Clear()<br/>&nbsp;Contains(T)  | -<br/>&nbsp;.Length<br/>&nbsp;GetLength(dimension )<br/>&nbsp;-<br/>&nbsp;Clear()<br/>&nbsp;-  | ICollection&lt;T&gt;<br/>&nbsp;.Count<br/>&nbsp;Add(T)<br/>&nbsp;Remove(T)<br/>&nbsp;Clear()<br/>&nbsp;Contains(T)  | ICollection&lt;T&gt;<br/>&nbsp;.Count<br/>&nbsp;Add(T)<br/>&nbsp;Remove(T)<br/>&nbsp;Clear()<br/>&nbsp;Contains(T)  | ICollection&lt;T&gt;<br/>&nbsp;.Count<br/>&nbsp;Add(T)<br/>&nbsp;Remove(T)<br/>&nbsp;Clear()<br/>&nbsp;Contains(T)  | -<br/>&nbsp;.Count<br/>&nbsp;Push(T)<br/>&nbsp;Dequeue(T)<br/>&nbsp;Clear()<br/>&nbsp;Contains(T)  |  -<br/>&nbsp;.Count<br/>&nbsp;Push(T)<br/>&nbsp;Pop(T)<br/>&nbsp;Clear()<br/>&nbsp;Contains(T)
- | .Min<br/>.Max<br/><br/><br/><br/><br/><br/><br/><br/> | AddRange(IEnumerable&lt;T&gt;)<br/>Insert(Int32, T)<br/>InsertRange(xx)<br/>RemoveAt(Int32)<br/>RemoveAll(Predicate&lt;T&gt;)<br/>RemoveRange(Int32, Int32)<br/><br/>BinarySearch(xx)<br/>Find(Predicate&lt;T&gt;)<br/>FindIndex(xxx)<br/>FindLast(Predicate&lt;T&gt;)<br/>FindLastIndex(Predicate&lt;T&gt;)<br/>ForEach(Action&lt;T&gt;)<br/>IndexOf(T, Int32, Int32)<br/>LastIndexOf(T, Int32, Int32)<br/><br/>Reverse()<br/>Reverse(Int32, Int32)<br/><br/>Sort()<br/>Sort(Comparison&lt;T&gt;)<br/> | <br/><br/><br/><br/><br/><br/><br/>BinarySearch(xx)<br/>Find(Predicate&lt;T&gt;)<br/>FindIndex(xxx)<br/>FindLast(Predicate&lt;T&gt;)<br/>FindLastIndex(Predicate&lt;T&gt;)<br/>ForEach(Action&lt;T&gt;)<br/>IndexOf(T, Int32, Int32)<br/>LastIndexOf(T, Int32, Int32)<br/><br/>Reverse()<br/>Reverse(Int32, Int32)<br/><br/>Sort()<br/>Sort(Comparison&lt;T&gt;)<br/> | .First<br/>.Last<br/><br/>AddFirst(x)<br/>AddBefore(xx)<br/>AddAfter(xx)<br/>AddLast(x)<br/><br/>Find(T)<br/>FindLast(T)<br/>RemoveFirst()<br/>RemoveLast() | .Keys<br/>.Values<br/><br/>ContainsKey(TKey)<br/>ContainsValue(TValue)<br/>TryGetValue(TKey, TValue)<br/><br/>Add(TKey, TValue)<br/>TryAdd(TKey, TValue) | .Keys<br/>.Values<br/><br/>ContainsKey(TKey)<br/>ContainsValue(TValue)<br/>TryGetValue(TKey, TValue)<br/><br/>Add(TKey, TValue)<br/>TryAdd(TKey, TValue) | Peek() | Peek()


<br/>
###LINQ
+ Any<TSource>(IEnumerable<TSource>)
+ All<TSource>(IEnumerable<TSource>, Func<TSource,Boolean>)
+ 
+ Contains<TSource>(IEnumerable<TSource>, TSource)
+ Contains<TSource>(IEnumerable<TSource>, TSource, IEqualityComparer<TSource>)
+ 
+ Count<TSource>(IEnumerable<TSource>)
+ Count<TSource>(IEnumerable<TSource>, Func<TSource,Boolean>)
+ 
+ First<TSource>(IEnumerable<TSource>)
+ Last<TSource>(IEnumerable<TSource>)
+ Min<TSource>(IEnumerable<TSource>)
+ Max<TSource>(IEnumerable<TSource>)
+ Sum<TSource>(IEnumerable<TSource>, Func<TSource,Decimal>)
+ 
+ ToArray<TSource>(IEnumerable<TSource>)
+ ToList<TSource>(IEnumerable<TSource>)
+ ToHashSet<TSource>(IEnumerable<TSource>)
+ ToDictionary<TSource,TKey>(IEnumerable<TSource>, Func<TSource,TKey>)
+ 
+ Select<TSource,TResult>(IEnumerable<TSource>, Func<TSource,TResult>)
+ Where<TSource>(IEnumerable<TSource>, Func<TSource,Boolean>)
+ 
+ 
+ Append<TSource>(IEnumerable<TSource>, TSource)
+ Concat<TSource>(IEnumerable<TSource>, IEnumerable<TSource>)














