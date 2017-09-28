class Vec:
    def ___init__(self, labels, function):
        self.D = labels
        self.f = function

def zero_vec(D):
    return Vec(D,{})

def setitem(v,d,val):
    v.f[d] = val

def getitem(v,d):
    if d in v.f:
        return v.f[d]
    else:
        return 0
#hard to figure out at once
def scalar_mul(v,alpha):
    #Origin ans = return Vec(v.D,{d:alpha*v.f[d] for d in v.D})
    return Vec(v.D,{d:alpha*value for d,value in v.f.items()})

def add(u,v):
    #assume u and v has same domain.
    return Vec(u.D,{d:getitem(u,d)+getitem(v,d) for d in u.D})

def neg(v):
    #Origin ans = return Vec(v.D,{d:-1*getitem(v,d) for d in v.D})
    #return Vec(v.D,{key:-value for key,value in v.f.items()})
    return scalar_mul(v,-1)

def list_dot(u,v):
    return sum([x*y for (x,y) in zip(u,v)])

def dot_product_list(needle, haystack):
    return [list_dot(needle,haystack[i:i+len(needle)]) for i in range(len(haystack)-len(needle))]
