#!/usr/bin/python3
import sys

global fences

def main():
    readline = lambda: sys.stdin.readline()
    testcase_number = int(readline())

    for i in range(testcase_number):
        fence_number = int(readline())
        global fences
        fences = [int(x) for x in readline().split()]
        print(solution(0, len(fences) - 1))


def solution(left, right):
    #base case 판자가 하나인 경우
    if(left == right):
        return fences[left]
        
    #회귀 계산
    mid = int((left + right) / 2)
    ret = max(solution(left, mid), solution(mid+1, right))
    
    #case 계산
    lo = mid
    hi = mid + 1
    height = min(fences[lo], fences[hi])

    ret = max(ret, height * 2)

    while(0 < lo or hi < right):
        if(hi < right and (lo == left or fences[lo-1] < fences[hi+1])):
            hi += 1
            height = min(height, fences[hi])
        else:
            lo -= 1
            height = min(height, fences[lo])

        ret = max(ret, height * (hi - lo + 1))

    return ret


if __name__ == '__main__':
    main()