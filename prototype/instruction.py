from enum import Enum

iCode = Enum('ICODE', [
    #Create
    'SPWN_NRN', 'SPWN_REG',
    #Bridge
    'BRIDGE', 'WEB', 'PAIR',


])

instruct_set : dict[str, ]

class instruction:

    inst        : iCode
    ops         : list[str]

    def __init__(self, code:str, ops:list[str]):
        self.ops = ops
        match code.lower():
            case '': pass
        