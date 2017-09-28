#2.7.1
def my_filter(L, num):
    return [x for x in L if x%num!=0]
#2.7.2
def my_lists(L):
    return [[x for x in range(1,y+1)]for y in L]
#2.7.3
def my_function_composition(f, g):
    return {fk:gv for (fk,fv) in zip(f.keys(),f.values()) for (gk,gv) in zip(g.keys(),g.values()) if fv==gk}
#2.7.4
def mySum(L):
    current = 0
    for x in L:
        current = current + x
    return current
#2.7.5
def myProduct(L):
    current = 1
    for x in L:
        current = current * x
    return current
#2.7.6
def myMin(L):
    current = L[0]
    for x in L[1:]:
        if(x<current)
        current = x
    return current
#2.7.7
def myConcat(L):
    current = ""
    for x in L:
        current = current + x
    return current
#2.7.8
def myUnion(L):
    current = set()
    for x in L:
        current = current | x
    return current
#2.7.12
def transform(a,b,L):
    return [a*z+b for z in L]
