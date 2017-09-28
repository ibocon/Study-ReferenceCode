import sys
import os

def main():
    file = open(os.path.join(os.path.dirname(__file__), "test.txt"))
    readline = lambda: file.readline() #lambda: sys.stdin.readline() #제출할 때는 표준입출력으로 전환하자!
    testcase_number = int(readline())

    for i in range(testcase_number):
        member_sex = list(readline().replace('\n', ''))
        fan_sex = list(readline().replace('\n', ''))
        print(solution(member_sex, fan_sex))

def solution(member_sex, fan_sex):
    #print("Member: {0}".format(member_sex))
    ret = 0
    male_position = list()
    member_number = len(member_sex)
    fan_number = len(fan_sex)

    for i in range(member_number):
        if(member_sex[i] == "M"):
            male_position.append(i)

    for j in range(fan_number - member_number + 1):
        target = fan_sex[j : j + member_number]
        result = [target[pos] == "M" for pos in male_position]
        if(True not in result):
            ret += 1

    return ret

if __name__ == '__main__':
    main()