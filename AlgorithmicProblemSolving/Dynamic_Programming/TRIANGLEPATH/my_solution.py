import sys
import os

def main():
    file = open(os.path.join(os.path.dirname(__file__), "test.txt"))
    readline = lambda: file.readline() #lambda: sys.stdin.readline()
    testcase_number = int(readline())

    for i in range(testcase_number):
        size = int(readline())
        cache = [[ None for j in range(i+1)] for i in range(size)]
        triangle = [[int(value) for value in readline().split()] for i in range(size)]
        print(solution(cache, triangle, 0, 0))


def solution(cache, triangle, y, x):
    if( y == len(triangle) - 1):
        return triangle[y][x]
    
    if(cache[y][x] == None):
        cache[y][x] = triangle[y][x] + max(solution(cache, triangle, y + 1, x), solution(cache, triangle, y + 1, x + 1))

    return cache[y][x]

if __name__ == '__main__':
    main()