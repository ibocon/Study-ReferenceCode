import sys
import os

def main():
    file = open(os.path.join(os.path.dirname(__file__), "test.txt"))
    readline = lambda: file.readline() #lambda: sys.stdin.readline()
    testcase_number = int(readline())

    for i in range(testcase_number):
        length = int(readline())
        array = [int(value) for value in readline().split()]
        cache = [None for i in range(length)]
        ans = 1
        for start in range(length):
            ans = max(ans, solution(cache, array, start))
        print(ans)

def solution(cache, array, start):
    if(cache[start] == None):
        count = 1
        for next in range(start + 1, len(array)):
            if(array[start] < array[next]):
                count = max(count, solution(cache, array, next) + 1)

        cache[start] = count

    return cache[start]
if __name__ == '__main__':
    main()