Title: Heap
Published: 07/18/2018
Tags:
  - Problem Solving
  - Algorithms
  - Data Structure
  - Heap
---
### What is Heap?
Heap is a binary tree representation.

Intro to Algo by C.L.R.S (page 159) can be applied part that I had trouble is the derivation.

- n element heap has height of `lg n`
- at most `n / (2^(h+1))` nodes of any height h

(i draw some examples which worked)
  
changed the limit to get better approx for `O()`

Leaf nodes are (in 0 based index),

    n/2, n/2+1, n/2+2, n/2+3 ... ... ... n

Question: Why does the book (CLRS) use 1 based index where programming languages are based on 0?
A common standard among books for pseudocode.

### delete element operation
The idea is similar to `Extract-Min`. We replace `i` with `A[size-1]` then, we heapify. Applied that this idea on to solve hackkerrank qHeap.
  
Deriving the tighter bound I mean in some pages in book.

A problem that uses counting sort but probably hard (Codeforces Round #182 (Div. 1)),
http://codeforces.com/contest/301/problem/D


Quoting https://arxiv.org/ftp/cs/papers/0007/0007043.pdf,

### Min-Max Fine Heaps
heap, introduced by Carlsson[2], using bit in each node to indicate its larger child, to improve the
performance of Min-Max Heap. We call it Min-Max Fine Heap. Also a technique similar to the one used
by Gonnet and Munro[5] for traditional heaps will be employed for better performance. In the next sections
we shall present the structure of the heap, algorithm for carrying out the standard operations on deq

Hash table runtime complexity (insert, search and delete)
https://stackoverflow.com/questions/9214353/hash-table-runtime-complexity-insert-search-and-delete

Show that second smallest of n elements can be found with `n + logn -2` in worst case.. 

Heap during finding the minimum???
https://www.careercup.com/question?id=354885

** on my heap sort or heap implementation verification **
https://www.hackerrank.com/contests/hw1/challenges/heap-sort
https://www.hackerrank.com/challenges/qheap1/problem

checking if anybody got accepted with dijkstra short reach
github.com hackerrank [dijkstrashortreach cpp implementation](https://github.com/kamomil/Hackerrank/blob/master/dijkstrashortreach.cpp)


### References

1. Page# 159 the book, [Introduction to Algorithm - C.L.R.S](https://dl.acm.org/citation.cfm?id=1614191)
2. Stony Brook Univ. Lecture - [Dijkstra’s SSSP & Fibonacci Heaps](http://www3.cs.stonybrook.edu/~rezaul/Spring-2015/CSE548/CSE548-lectures-19-20.pdf)