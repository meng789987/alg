
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

LINQ  HashSet
-------------  -------------
Union   UnionWith
Intersect  IntersectWith
Except  ExceptWith
Distinct  NA

br
###Collections
The constructors of HashSet and Dictionary, and Linq.Distinct() can take an IEqualityComparer&lt;T&gt; object, whose implementation is recommended to derive from  EqualityComparer&lt;T&gt;, and must have correct GetHashCode() and Equals().

The constructors of SortedSet and SortedDictionary,  and BinarySearch() and Sort() in Array and List can take an IComparer&lt;T&gt; object, which can be created from a Lamda Comparison
+ Comparer&lt;T&gt;.Create(Comparisont&lt;T&gt;)

The Sort() in Array and List can take a Lamda Comparison object.

HashSet  SortedSet  List  Array  LinkedList  Dictionary  SortedDictionary  Stack  Queue
-------------  -------------  -------------  -------------  -------------  -------------  -------------
ICollection&lt;T&gt;br&nbsp;.Countbr&nbsp;Add(T)br&nbsp;Remove(T)br&nbsp;Clear()br&nbsp;Contains(T)   ICollection&lt;T&gt;br&nbsp;.Countbr&nbsp;Add(T)br&nbsp;Remove(T)br&nbsp;Clear()br&nbsp;Contains(T)   ICollection&lt;T&gt;br&nbsp;.Countbr&nbsp;Add(T)br&nbsp;Remove(T)br&nbsp;Clear()br&nbsp;Contains(T)   -br&nbsp;.Lengthbr&nbsp;GetLength(dimension )br&nbsp;-br&nbsp;Clear()br&nbsp;-   ICollection&lt;T&gt;br&nbsp;.Countbr&nbsp;Add(T)br&nbsp;Remove(T)br&nbsp;Clear()br&nbsp;Contains(T)   ICollection&lt;T&gt;br&nbsp;.Countbr&nbsp;Add(T)br&nbsp;Remove(T)br&nbsp;Clear()br&nbsp;Contains(T)   ICollection&lt;T&gt;br&nbsp;.Countbr&nbsp;Add(T)br&nbsp;Remove(T)br&nbsp;Clear()br&nbsp;Contains(T)   -br&nbsp;.Countbr&nbsp;Push(T)br&nbsp;Dequeue(T)br&nbsp;Clear()br&nbsp;Contains(T)    -br&nbsp;.Countbr&nbsp;Push(T)br&nbsp;Pop(T)br&nbsp;Clear()br&nbsp;Contains(T)
-  .Minbr.Maxbrbrbrbrbrbrbrbr  AddRange(IEnumerable&lt;T&gt;)brInsert(Int32, T)brInsertRange(xx)brRemoveAt(Int32)brRemoveAll(Predicate&lt;T&gt;)brRemoveRange(Int32, Int32)brbrBinarySearch(xx)brFind(Predicate&lt;T&gt;)brFindIndex(xxx)brFindLast(Predicate&lt;T&gt;)brFindLastIndex(Predicate&lt;T&gt;)brForEach(Action&lt;T&gt;)brIndexOf(T, Int32, Int32)brLastIndexOf(T, Int32, Int32)brbrReverse()brReverse(Int32, Int32)brbrSort()brSort(Comparison&lt;T&gt;)br  brbrbrbrbrbrbrBinarySearch(xx)brFind(Predicate&lt;T&gt;)brFindIndex(xxx)brFindLast(Predicate&lt;T&gt;)brFindLastIndex(Predicate&lt;T&gt;)brForEach(Action&lt;T&gt;)brIndexOf(T, Int32, Int32)brLastIndexOf(T, Int32, Int32)brbrReverse()brReverse(Int32, Int32)brbrSort()brSort(Comparison&lt;T&gt;)br  .Firstbr.LastbrbrAddFirst(x)brAddBefore(xx)brAddAfter(xx)brAddLast(x)brbrFind(T)brFindLast(T)brRemoveFirst()brRemoveLast()  .Keysbr.ValuesbrbrContainsKey(TKey)brContainsValue(TValue)brTryGetValue(TKey, TValue)brbrAdd(TKey, TValue)brTryAdd(TKey, TValue)  .Keysbr.ValuesbrbrContainsKey(TKey)brContainsValue(TValue)brTryGetValue(TKey, TValue)brbrAdd(TKey, TValue)brTryAdd(TKey, TValue)  Peek()  Peek()


br
###LINQ
+ AnyTSource(IEnumerableTSource)
+ AllTSource(IEnumerableTSource, FuncTSource,Boolean)
+ 
+ ContainsTSource(IEnumerableTSource, TSource)
+ ContainsTSource(IEnumerableTSource, TSource, IEqualityComparerTSource)
+ 
+ CountTSource(IEnumerableTSource)
+ CountTSource(IEnumerableTSource, FuncTSource,Boolean)
+ 
+ FirstTSource(IEnumerableTSource)
+ LastTSource(IEnumerableTSource)
+ MinTSource(IEnumerableTSource)
+ MaxTSource(IEnumerableTSource)
+ SumTSource(IEnumerableTSource, FuncTSource,Decimal)
+ 
+ ToArrayTSource(IEnumerableTSource)
+ ToListTSource(IEnumerableTSource)
+ ToHashSetTSource(IEnumerableTSource)
+ ToDictionaryTSource,TKey(IEnumerableTSource, FuncTSource,TKey)
+ 
+ SelectTSource,TResult(IEnumerableTSource, FuncTSource,TResult)
+ WhereTSource(IEnumerableTSource, FuncTSource,Boolean)
+ 
+ 
+ AppendTSource(IEnumerableTSource, TSource)
+ ConcatTSource(IEnumerableTSource, IEnumerableTSource)














