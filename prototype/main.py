from vpython import *
from random import random as rand
from model import *
from math import radians, sin, cos, log10, pi
from compiler import compiler

scene.title = f'Test'
scene.width = 800
scene.height = 800

RANGE = 50
W = 10
H = 10
Z = 10

def calc_x(v,r): return v
def calc_y(v,r): return tan(v/r)*8
def calc_z(v,r): return sin((v)/(10))*log10(2*(v+.1)/5)
def calc_vec(v,r): return vector(calc_x(v,r),calc_y(v,r),calc_z(v,r))

def calc_v_x(v, r, i): return v
def calc_v_y(v, r, i): return v
def calc_v_z(v, r, i): return 3 + cos(i/r)
def calc_v_vec(v:vector, n, r, i): 
    return vector(calc_v_x(v.x,r,i),calc_v_y(v.y,r,i),calc_v_z(v.z*n,r,i))

nodes :list[box] = []
#dirs = Pointers()
#root = SpatialBranch('root', color.yellow, calc_vec(0, RANGE), calc_vec(1, RANGE))
#guide = SpatialGuide(root)

#ref = curve()
#prev = root

comp = compiler()
insts = comp.compile_file("./scripts/test.sndl")
print(insts)


# def build_main():
#     global prev, guide
#     for i in range(1, RANGE):
#         b = SpatialBranch(f'c_{i}', color.yellow, calc_vec(i, RANGE), prev)
#         guide.insert(b)
#         prev = b

# def build_visual():
#     global prev, guide
#     prev = guide.get_branch(f'c_{RANGE-4}')

#     start = RANGE-4
#     end = RANGE-15
#     diff = start - end

#     for i in range(start, end, -1):
#         guide.insert(SpatialBranch(f'op_{i}', color.red, calc_v_vec(prev.head, 1, diff, i), prev))
#         guide.insert(SpatialBranch(f'on_{i}', color.cyan, calc_v_vec(prev.head, -1, diff, i), prev))
#         prev = prev.tail_branch


# build_main()
# build_visual()

# while True:
#     rate(20)


