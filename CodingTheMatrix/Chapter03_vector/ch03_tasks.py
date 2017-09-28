def add2(v,w):
    return [v[0]+w[0],v[1]+w[1]]
def addn(v,w):
    return [x+y for (x,y) in zip(v,w)]
def scalar_vector_mult(alpha, v):
    return [alpha*x for x in v]
def segment(pt1, pt2):
    return [[x*pt1[0]+(1-x)*pt2[0],x*pt1[1]+(1-x)*pt2[1]] for x in [0.01*i for i in range(101)]]
