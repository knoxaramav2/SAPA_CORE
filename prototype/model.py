from __future__ import annotations
from vpython import vector, curve, color

class SpatialBranch:

    name        : str
    clr         : color
    head        : vector
    tail        : vector


    head_branches : list[SpatialBranch]
    tail_branch   : SpatialBranch

    branch_master : dict[str, SpatialBranch] = {}

    sb_curve     : curve

    def __register_branch(self, name:str, branch:SpatialBranch):
        if name in self.branch_master:
            raise Exception(f'Duplicate name: {name}')
        self.branch_master[name] = branch

    def __init__(self, name:str, clr:color,
        head:vector, parent:SpatialBranch|vector) -> None:

        self.name = name
        self.clr = clr
        self.tail = parent if isinstance(parent, vector) else parent.head
        self.head = head

        self.head_branches = []

        if isinstance(parent, SpatialBranch):
            parent.head_branches.append(self)
        self.tail_branch = parent

        self.__register_branch(name, self)
        self.sb_curve = curve(pos=[self.tail, self.head], color=clr, radius=.05)

    def extend_from(self, name:str, head:vector) ->SpatialBranch:
        branch = SpatialBranch(name, self.clr, self.tail, head, self)
        self.head_branches.append(branch)
        return branch
    
    def extend(self, branch:SpatialBranch):
        branch.tail = self.head
        self.head_branches.append(branch)
        branch.tail_branch = self

class SpatialGuide:
    root        : SpatialBranch
    curr_branch : SpatialBranch

    def __init__(self, branch:SpatialBranch) -> None:
        self.root = branch
        self.curr_branch = branch

    def get_branch(self, name:str) -> SpatialBranch|None:
        if name not in self.root.branch_master: return None
        return self.root.branch_master[name]    

    def set_branch(self, name:str) -> bool:
        if name not in self.root.branch_master:
            return False
        self.curr_branch = self.root.branch_master[name]
        return True

    def insert(self, branch:SpatialBranch):
        self.curr_branch.extend(branch)
        self.curr_branch = branch

class Pointers:

    origin      : vector
    arrows      : list[curve]

    def __init__(self) -> None:
        self.origin = vector(0, 0, 0)
        self.arrows = [
            curve(self.origin, vector(10, 0, 0), color=color.red, radius=0.035),
            curve(self.origin, vector(0, 10, 0), color=color.orange, radius=0.035),
            curve(self.origin, vector(0, 0, 10), color=color.green, radius=0.035)
                       ]
