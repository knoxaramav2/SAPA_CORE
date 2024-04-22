from __future__ import annotations
from enum import Enum
from random import randint
from typing import Callable
import traceback

class Color(Enum):
    WHITE       = (255, 255, 255)
    BLACK       = (0,0,0)
    
    RED         = (255, 0, 0)
    GREEN       = (0, 255, 0)
    BLUE        = (0, 0, 255)

    ORANGE      = (211, 84, 0)
    PURPLE      = (108, 52, 131)
    YELLOW      = (241, 196, 15)
    CYAN        = (0, 255, 255)
    TEAL        = (0, 128, 128)
    MAROON      = (128, 0, 0)
    CRIMSON     = (220, 20, 60)
    
    LIGHT_GREEN = (60, 214, 68)
    PINK        = (255, 20, 147)
    DARK_GREY   = (75, 75, 75)
    DARK_RED    = (95, 6, 6)
    DARK_PURPLE = (43, 16, 99)
    DARK_BROWN  = (67, 47, 0)
    
    def random() -> tuple[int,int,int]:
        r = randint(0, 255)
        g = randint(0, 255)
        b = randint(0, 255)
        return (r, g, b)


class DataPoint:
    
    value   : float

    def __init__(self, value:float):
        self.value = value

catclrs = [
    Color.RED.value,
    Color.BLACK.value,
    Color.BLUE.value,
    Color.ORANGE.value,
    Color.PINK.value,
    Color.YELLOW.value,
    Color.PURPLE.value,
    Color.CYAN.value,
    Color.GREEN.value,
]

class DataHistory:
    name            : str
    __max_mem       : int
    __data          : dict[str, dict[str, list[DataPoint]]] = {}
    __min           : int
    __max           : int
    __colors        : dict[str, tuple[int,int,int]] = {}
    __categories    : list[str] = []

    __all_data      : dict[str, dict[str, list[float]]] = {}

    #callbacks
    __on_add_callback        : Callable = None

    def list_keys(self) -> list[str]:
        return list(self.__data.keys())

    def get_from_all_idx(self, id:str, category:str, idx:int) -> float:
        if id not in self.__all_data: return []
        val = self.__all_data[id][category]
        if idx >= len(val): return None
        ret = val[idx]
        return ret

    def get_last(self, id:str, category:str):
        val = self.get(id, category)
        return 0 if len(val) == 0 else val[-1].value

    def get(self, id:str, category:str):
        if id not in self.__data: return []
        return self.__data[id][category]

    def get_categories(self) -> list[str]:
        return self.__categories

    def get_category_values(self, id:str, category:str) -> list[DataPoint]:
        return self.__data[id][category]

    def assign_on_add(self, fnc:Callable):
        self.__callback = fnc

    def __register_id(self, id:str):
        self.__data[id] = {}
        self.__all_data[id] = {}
        for catid in self.__categories:
            self.__data[id][catid] = []
            self.__all_data[id][catid] = []

    def add_point(self, id:str, category:str, point:DataPoint):
        self.__min = min(self.__min, point.value)
        self.__max = max(self.__max, point.value)

        if id not in self.__data:
            self.__register_id(id)

        vals:list[DataPoint] = self.get(id, category)
        self.__all_data[id][category].append(point.value)
        vals.append(point)
        dm = len(vals) - self.__max_mem
        if dm > 0:
            del vals[0:dm]

        if self.__on_add_callback != None:
            self.__on_add_callback()
                
    def last_values(self) -> list[DataPoint]:
        ret:list[DataPoint] = []
        for val in self.__data.values():
            ret.append(val)

        return ret

    def get_category_info(self) -> list[tuple[str, tuple[int,int,int]]]:
        return self.__colors

    def get_color(self, catid:str):
        return self.__colors[catid]

    def min_max(self, diff:int=0) -> tuple[int, int]:
        return (self.__min-diff, self.__max+diff)

    def max_size(self) -> int: return self.__max_mem

    def __init__(self, categories:list[str], history:int):
        self.__min = -80
        self.__max = 50
        self.__max_mem = history
        self.__categories = categories
        for i in range(len(categories)):
            tag = categories[i]
            self.__colors[tag]=catclrs[i]
     

    def get_csv(self) -> str:

        ln = 10000
        dt = 0.4
        categories = self.get_categories()
        nrns = []
        nrns.extend([x for x in self.__data.keys() if x != 'Input'])
        #build header
        header = 'time,Input'
        for nrn in nrns:
            header += ',' + ','.join([f'{nrn}_{x}' for x in categories])
        
        lines = []
        i=0

        while True:
            try:
                values = []
                [values.extend([self.get_from_all_idx(n, c, i) for c in categories]) for n in nrns]
                if None in values: break
                line = f'{dt*i:.2f},{self.get_from_all_idx('Input', 'v', i)},' + ','.join(f'{x:.4f}' for x in values)
                lines.append(line)
                i += 1
            except Exception as e: 
                traceback.print_exc()
                break
        ret = header + '\n' + '\n'.join(lines)
        return ret


