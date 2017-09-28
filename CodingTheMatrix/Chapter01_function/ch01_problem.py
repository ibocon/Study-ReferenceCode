#1.8.1
def increments(L):
    return [x+1 for x in L]
#1.8.2
def cubes(L):
    return [x*x*x for x in L]
#1.8.3
def tuple_sum(A,B):
    return [(a+b,c+d) for ((a,c),(b,d)) in zip(A,B)]
#1.8.4
def inv_dict(d):
    return {value:key for (value,key) in zip(d.values(),d.keys())}
#1.8.5
def row(p,n):
    return [p+k for k in range(n)]
