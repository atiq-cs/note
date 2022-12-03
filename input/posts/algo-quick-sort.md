Title: Quick Sort
Published: 09/14/2017
Tags:
  - Problem Solving
  - Algorithms
  - Sorting
---
Based on the idea that we can partition an array around a pivot (chosen element of the array), we can put the smaller (than pivot) elements on the left side and place greater elements on the right side of the pivot, then, we keep doing the same thing for the left side and the right side. More details of how this algorithm works is on "How it works" section.

![quick sort with pivot figure][1]

Fig: fundamental concept of Quick-Sort, partitioning

**Quick sort**, *one of the fundamental sorting algorithms of computer science sorts an array efficiently on average*.

A few important things about Quick Sort,

- expected running time θ(n lg n)
- sorts in place (does not use additional memory)
- works well even in virtual environment
- worst case does not depend on any particular input

## Description of the Algorithm
Steps,

 - Divide
 - Conquer
 - Combine

### Divide
Partition of the array `A[pr..r]` into 2 parts, `A[p .. q-1]` and `A[q-1 .. r]`. Index `q` comes from the partitioning procedure.

Pivot can be chosen in a number of ways,

- The last index as pivot (C.L.R.S - Intro to Algo)
- middle element as pivot: [Gayle Mcdowell's youtube video on quick sort](https://www.youtube.com/watch?v=SLauY6PpjW4)
- first index or some other index (can be randomized)

### Conquer
We start with partitioning the array. We sort 2 sub-arrays recursively by calling quick sort on partitions.

### Combine
Each recursive call to 'quick-sort' sorts the particular segment of the array. Once the last recursive call returns, the array is sorted. Due to that, no action is required to be taken on combine step.

## How it works
The idea is that we choose a pivot element from the array. We put all elements less than the pivot to the left side and we put all elements greater than the pivot item to the right side. We consider the left side and the right side as 2 sub-arrays where we apply the same procedure. After recursively applying the mechanism in the end, the array is sorted.

### Worst Case of Quick Sort
Worst case happens if every call of partition the chosen pivot creates a partition with (n-1) element and 1 element. This can be the case when an array is sorted in reverse order.

    For example an array with 5 elements,
     5 4 3 2 1

    Steps can be visualized below,
    1. QuickSort(A, 0, 4)

    Partition procedure chooses A[4] = 1 as pivot. Nothing is smaller than 1 so the
    iteration/traversing element does nothing. In the end, it swaps A[4] and A[0]. 
    At this stage, the array looks like,
     1 4 3 2 5

    and the procedure returns 0. After partition function returns two calls are spawned,
     QuickSort(A, 0, -1)
     QuickSort(A, 1, 4)

    2. QuickSort(A, 0, -1) does nothing
    3. QuickSort(A, 1, 4) has following input,
     4 3 2 5
    
    its partition procedure chooses A[4] as pivot and returns 4. All elements swap
    with themselves; array stays unchanged.

    It spawns two quick sorts.
     QuickSort(A, 1, 3)
     QuickSort(A, 5, 4)

    4. QuickSort(A, 1, 3) has input as,
     4 3 2
    
    Partition does a swap in the end and leaves 2 in index 1. It returns 1.
    it looks like following then,
     2 3 4
     
    It spawns two quick sort calls.
     QuickSort(A, 1, 0)
     QuickSort(A, 2, 3)
     
    5. QuickSort(A, 5, 4) does nothing and the array is sorted.
    6. QuickSort(A, 1, 0) takes no action.
    7. QuickSort(A, 2, 3) Partition takes index 3 as pivot. Array stays
    unchanged. 3 is returned by Partition. 2 calls are spawned,
     QuickSort(A, 2, 2)
     QuickSort(A, 4, 3)
    
    
    Worst case of quick sort depends on the partitioning. As we can see index
    returned by partition function are,
     0, 4, 1, 3, 2

It means instead pivot dividing the array into two parts it has traversed through every index of the array.
Therefore, we get O(N) for each call of quick sort. pivot is selected n times.
This gives overall O(n^2) time for quick sort.

The Generic Algorithm written in book, Intro to Algo by C.L.R.S can be applied to sort a part of an array specifying starting index and the ending index. Usually when I write a sorting algorithm we apply to the entire array calling like this,

    QuickSort(A);

Usually in C# or Java languages there is a Length attribute of the array, C has `sizeof`. Another example input,

    5
    1 4 2 4 3


### Primary References
1. From the book, [Introduction to Algorithm - C.L.R.S](https://dl.acm.org/citation.cfm?id=1614191)
 - Performance analysis on Section# 7.2
 - Random Sampling on Section# 7.3
 - Worst case analysis on Section# 7.4
2. CS Stony Brook Dr Rezaul's Lecture
3. Cracking the Coding Interview, 3rd ed. - Gayle Mcdowell


  [1]: https://github.com/atiq-cs/atiqcs-wp-com/raw/master/images/2017/09/QuickSort-Partitioning.png
