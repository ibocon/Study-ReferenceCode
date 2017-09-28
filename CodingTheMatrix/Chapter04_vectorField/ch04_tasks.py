from itertools import product

from matrix.GF2 import one
from matrix.vec import Vec


def lin_comb(vlist, clist):
    return sum([alpha*v for (alpha, v) in zip(clist, vlist)])


def standard(D, one):
    return [Vec(D, {k: one}) for k in D]


#Problem 4.8.1
#문제의 정의가 이상하며, 명확하지가 않다.
def vec_select(veclist, k):
    '''
    >>> D = {'a','b','c'}
    >>> v1 = Vec(D, {'a': 1})
    >>> v2 = Vec(D, {'a': 0, 'b': 1})
    >>> v3 = Vec(D, {        'b': 2})
    >>> v4 = Vec(D, {'a': 10, 'b': 10})
    >>> vec_select([v1,v2,v3,v4],'a') == [Vec({'a'},{'a':1}),Vec({'a'},{'a':0}),Vec({'a'},{'a':0}),Vec({'a'},{'a':10})]
    True
    '''
    selected = list()
    for x in veclist:
        selected += [Vec({k}, {k: x[k]})]
    return selected


def vec_sum(veclist, D):
    '''
    >>> D = {'a','b','c'}
    >>> v1 = Vec(D, {'a': 1})
    >>> v2 = Vec(D, {'a': 0, 'b': 1})
    >>> v3 = Vec(D, {        'b': 2})
    >>> v4 = Vec(D, {'a': 10, 'b': 10})
    >>> vec_sum([v1, v2, v3, v4], D) == Vec(D, {'b': 13, 'a': 11})
    True
    '''
    return Vec(D, {k: sum([x[k] for x in veclist]) for k in D})


def vec_select_sum(D, veclist, k):
    '''
    >>> D = {'a','b','c'}
    >>> v1 = Vec(D, {'a': 1})
    >>> v2 = Vec(D, {'a': 0, 'b': 1})
    >>> v3 = Vec(D, {        'b': 2})
    >>> v4 = Vec(D, {'a': 10, 'b': 10})
    >>> vec_select_sum(D,[v1,v2,v3,v4],'a') == [Vec({'a'},{'a':11})]
    True
    '''
    return vec_select([vec_sum(veclist, D)], k)


#Problem 4.8.2
def scale_vecs(vecdict):
    '''
    >>> v1 = Vec({1,2,3}, {2: 9})
    >>> v2 = Vec({1,2,4}, {1: 1, 2: 2, 4: 8})
    >>> scale_vecs({3: v1, 5: v2}) == [Vec({1,2,3},{2: 3.0}), Vec({1,2,4},{1: 0.2, 2: 0.4, 4: 1.6})]
    True
    '''
    return [vecdict[k]/k for k in vecdict.keys()]


#Problem 4.8.3
def GF2_span(D, L):
    '''
    >>> from ...matrix.GF2 import one
    >>> D = {'a', 'b', 'c'}
    >>> L = [Vec(D, {'a': one, 'c': one}), Vec(D, {'b': one})]
    >>> len(GF2_span(D, L))
    4
    >>> Vec(D, {}) in GF2_span(D, L)
    True
    >>> Vec(D, {'b': one}) in GF2_span(D, L)
    True
    >>> Vec(D, {'a':one, 'c':one}) in GF2_span(D, L)
    True
    >>> Vec(D, {x:one for x in D}) in GF2_span(D, L)
    True
    '''
    return [sum([a*v for (a, v) in zip(i, L)]) for i in product({0, one}, repeat=len(L))] if len(L) !=0 else Vec(D,{})
