def create_voting_dict(strlist):
    for i in range(len(strlist)):
        votDict = dict()
        line = strlist[i].split()
        votDict[line[0]]=[int(line[k]) for k in range(3,len(line))]
    return votDict

def policy_compare(sen_a,sen_b,voting_dict):
    return sum([x*y for (x,y) in zip(voting_dict[sen_a],voting_dict[sen_b])])

def most_similar(sen, voting_dict):
    bestFriend="NoOne"
    score=0
    for name in voting_dict.keys():
        value = policy_compare(sen,name,voting_dict)
        if sen!=name and value>score:
            score=value
            bestFriend=name
    return bestFriend

def least_similar(sen, voting_dict):
    worstEnemy="NoOne"
    score=1000
    for name in voting_dict.keys():
        value = policy_compare(sen,name,voting_dict)
        if value<score:
            score=value
            worstEnemy=name
    return worstEnemy

#democracy = [name for name in [mylist[i].split()[0] for i in range(len(mylist)) if mylist[i].split()[1]=='D']]
def find_average_similarity(sen,sen_set,voting_dict):
    cal = 0
    for name in sen_set:
        cal += policy_compare(sen,name,voting_dict)
    cal /= len(sen_set)
    return cal
#for name in voting_dict.keys():
#    print(name,":",find_average_similarity(name,sen_set,voting_dict))

#misunderstood the problem..
#this method find out different meaning
def find_average_record(sen_set,voting_dict):
    recordlist=list()
    votes=len((voting_dict[sen_set[0]]))
    for i in range(votes):
        record=0
        for name in sen_set:
            record+=voting_dict[name][i]
        recordlist+=[record/votes]
    return recordlist

def bitter_rivals(voting_dict):
    conflict=1000
    rival1="YES"
    rival2="NO"
    for name1 in voting_dict.keys():
        for name2 in voting_dict.keys():
            temp=policy_compare(name1,name2,voting_dict)
            if(temp<conflict):
                rival2=name2
                rival1=name1
                conflict=temp
    print(rival1, "Vs." ,rival2)
