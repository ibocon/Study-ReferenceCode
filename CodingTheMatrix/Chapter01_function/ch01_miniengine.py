# Title: Mini search engine

## Required
def makeInverseIndex(strlist):
    index = dict()
    for (number, document) in enumerate(strlist):
        #processing new doucment from strlist
        for word in upper2lower(document.split()):
            if word in index.keys():
                #add new document number value to existed word key
                index[word].add(number)
            else :
                #new key:value pair
                index[word] = {number}
    return index

def orSearch(inverseIndex, query):
    answer = set()
    for word in query:
        if word in inverseIndex.keys():
            answer = answer | inverseIndex[word]
    return answer

def andSearch(inverseIndex, query):
    answer = set(inverseIndex[query[0]])
    for word in query[1:]:
        if word in inverseIndex.keys():
            answer = answer & inverseIndex[word]
    return answer

## Created
def file2strlist(filedir):
    #make string list from file
    return list(open(filedir))

def upper2lower(wordlist):
    #upper words to lower words
    return [word.lower() for word in wordlist]
