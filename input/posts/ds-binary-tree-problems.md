Title: Binary Related Problems
Published: 10/06/2018
Tags:
  - Problem Solving
  - Algorithms
  - Binary Tree
---
ToDo: split the article later by topic.

## Related Problems List
1. Construct binary Tree from inorder and postorder [leetcode problem link](https://leetcode.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal)

2. cartesian tree problems in [codeforces blog](https://codeforces.com/blog/entry/3767)

3. Given an inorder traversal of a cartesian tree, construct the tree. following [js meetup](https://www.meetup.com/Bay-Area-JavaScript-Interview-Prep-Meetup/events/254210621) works on this on 09-06, here's the [google doc](https://docs.google.com/document/d/1GzSrxwIakPdZYDLD-69MWhrbrfpMavdnqArTlzVYa4w) with work on code from few members.

Woud this help?

    int findInorderPosition(int[] inorder, int value, int start, int end) {
        for (int i = start; i <= end; i++)
        if (inorder[i] == value)
            return i;
        return -1;
    }

dynamic programming find max sum from array, [SO hint](https://stackoverflow.com/q/29236837)

### string problems

codeforces String Problems and Solutions
https://codeforces.com/blog/entry/49938

https://codeforces.com/problemset/problem/126/B
https://codeforces.com/problemset/problem/471/D

segment tree (probably a solution on Battle with you know who) [code link](https://ideone.com/uihDVe)

#### Competitive programming, with Lewin Gan
- Contest 02 on 08-19 [event link](https://www.meetup.com/Competitive-Programming/events/253040202) and solutions discussion [article link](https://paper.dropbox.com/doc/Contest-2-Solutions--AR7af~n02Sq37GkMp1nEQyO7Ag-MoU0f0AH6cXOPZcJZzxOC)

## System Design Problems
1. [Design Twitter (Part 2)](https://fipg.slack.com/files/U5ZM4RSS1/FD0412RB9/Design_Twitter__Part_2_)


### Misc
[Rezaul convolution](https://www3.cs.stonybrook.edu/~rezaul/Fall-2017/CSE548/CSE548-lecture-4.pdf)

Problem set & Analysis from the Finals and Qualification rounds
http://bubblecup.org/Content/Media/Booklet2018.pdf

Why does a = b (mod n) iff a - b is divisible by n?
https://math.stackexchange.com/questions/1856128/why-does-a-b-mod-n-iff-a-b-is-divisible-by-n

may be related to some problem from innoworld
https://codeforces.com/contest/455/problem/D

#### binary indexed tree
codeforces editorials on 
- https://hk.saowen.com/a/06617b9cf1dd01762b591b6dedb0c5bfd4318d4f1bf12cf58cf4621889a884ed
- https://codeforces.com/comments/with/Z38
- https://codeforces.com/blog/entry/60511
- https://codeforces.com/problemset/gymProblem/100741/A
- https://codeforces.com/contest/575/problem/H

