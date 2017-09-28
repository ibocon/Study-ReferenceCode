#!/usr/bin/python3
import sys
import os

global cache

def main():
    file = open(os.path.join(os.path.dirname(__file__), "test.txt"))
    readline = lambda: file.readline() #lambda: sys.stdin.readline()
    testcase_number = int(readline())

    for i in range(testcase_number):
        board_size = int(readline())
        board = [[int(value) for value in readline().split()] for i in range(board_size)]
        
        if(solution(board, board_size)):
            print("YES")
        else:
            print("NO")

def solution(board, size):
    global cache
    cache = [[ -1 for i in range(size)] for j in range(size)]

    return jump(board, size, 0, 0)

def jump(board, size, y, x):
    if(y >= size or x >= size):
        return False

    if(y == size-1 and x == size-1):
        return True

    if(cache[y][x] != -1):
        return cache[y][x]

    jump_length = board[y][x]
    cache[y][x] = jump(board, size, y + jump_length, x) or jump(board, size, y, x + jump_length)

    return cache[y][x]

if __name__ == '__main__':
    main()