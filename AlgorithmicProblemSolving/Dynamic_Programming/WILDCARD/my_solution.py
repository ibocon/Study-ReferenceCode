import sys
import os

global cache

def main():
    file = open(os.path.join(os.path.dirname(__file__), "test.txt"))
    readline = lambda: file.readline() #lambda: sys.stdin.readline()
    testcase_number = int(readline())

    for i in range(testcase_number):
        pattern = readline().rstrip()
        file_number = int(readline())

        matching_list = list()
        for j in range(file_number):
            filename = readline().rstrip()
            global cache
            cache = [[ None for j in range(len(filename) + 1)] for i in range(len(pattern) + 1)]
            if(check(pattern, filename)):
                matching_list.append(filename)
        
        matching_list.sort()
        for value in matching_list:
            print(value)

def check(pattern, filename, a_point = 0, b_point = 0):

    global cache

    if(cache[a_point][b_point] != None):
        return cache[a_point][b_point]

    if(a_point < len(pattern) and b_point < len(filename) and (pattern[a_point] == '?' or pattern[a_point] == filename[b_point] )):
        return check(pattern, filename, a_point+1, b_point+1)

    if(a_point == len(pattern)):
        cache[a_point][b_point] = (b_point == len(filename))
    elif(pattern[a_point] == '*'):
        if(check(pattern, filename, a_point+1, b_point) or (b_point < len(filename) and check(pattern, filename, a_point, b_point + 1))):
            cache[a_point][b_point] = True
    else:
        cache[a_point][b_point] = False

    return cache[a_point][b_point]

if __name__ == '__main__':
    main()
