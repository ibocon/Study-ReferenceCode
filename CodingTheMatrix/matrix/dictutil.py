#Chapter01 Lab - Creating module
def dict2list(dct, keylist):
    return [dct[x] for x in keylist]

def list2dict(L, keylist):
    return {a:b for (a,b) in zip(keylist,L)}
    #enumerate(L)
def listrange2dict(L):
    return {a:b for (a,b) in zip(range(len(L)),L)}
