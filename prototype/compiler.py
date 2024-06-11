from instruction import *
import re
import os

Lexeme = Enum('lexeme', [
    'OP_PARAN', 'CL_PARAN',
    'OP_BRACE', 'CL_BRACE',
    'OP_BRACK', 'CL_BRACK',
    'ELIPSE', 'DOT',
    'SYM', 'STR', 'NUM',
    'BRIDGE', 'WEB', 'PAIR',
    'ID_REF', 'COMMA', 'LAMBDA',
    'BSLASH', 'FSLASH',
    
    #Keywords
    'NAMESPACE', 'INPUTS', 'OUTPUTS',
    'SPECIFY', 'AS',

])

sym_table : dict[str, Lexeme] = {
    '('     : Lexeme.OP_PARAN,      ')'     : Lexeme.CL_PARAN, 
    '{'     : Lexeme.OP_BRACE,      '}'     : Lexeme.CL_BRACE, 
    '['     : Lexeme.OP_BRACK,      ']'     : Lexeme.CL_BRACK,

    '...'   : Lexeme.ELIPSE,        '.'     : Lexeme.DOT,
    '->'    : Lexeme.BRIDGE,        '=>'    : Lexeme.WEB,           '&>'    : Lexeme.PAIR,
    '$'     : Lexeme.ID_REF,        ','     : Lexeme.COMMA,         ':'     : Lexeme.LAMBDA, 
    '/'     : Lexeme.BSLASH,        '\\'    : Lexeme.FSLASH, 

    'ns'    : Lexeme.NAMESPACE,     'inputs': Lexeme.INPUTS,        'outputs': Lexeme.OUTPUTS,
    'specify': Lexeme.SPECIFY,      'as'    : Lexeme.AS
}

class compiler:

    __lineNo        : int

    def __init__(self):
        pass

    def __identify(self, tkn:str) -> tuple[str, Lexeme]:
        tkn = tkn.strip()
        lx = Lexeme.SYM

        if tkn in sym_table:
            lx = sym_table[tkn]

        return (tkn, lx)

    def __resolve_special(self, c:str) -> str:
        match c:
            case 't': c = '\t'
            case 'n': c = '\n'

        return c
        
    def __get_sym(self, term:str):
        pass

    def __get_num(self, term:str):
        pass

    def __get_str(self, term:str):
        pass

    def __tokenize(self, line:str, encl:list[Lexeme]) -> list[tuple[Lexeme, str]]:
        ret     : list[tuple[str, Lexeme]] = []
        ops     : list[tuple[str, Lexeme]] = []
        lIdx = 0

        i = -1
        lsz = len(line)
        line += '\0'

        closures = []
        op_closures = '([{'
        cl_closures = '}])'

        item = None

        while i < lsz:
            i += 1
            c = line[i]

            if c in op_closures:
                item = (c, sym_table[c])
                ops.append(item)
                ret.append(item)
        
        return ret


    def compile_file(self, path) -> list[instruction]:
        self.__lineNo = 0
        path = os.path.join(os.getcwd(), path)

        tokens  : list[list[str, Lexeme]] = []
        encl    : list[Lexeme] = []
        raw = open(path, 'r+').readlines()
        for line in raw:
            self.__lineNo += 1
            tkns = self.__tokenize(line, encl)
            if len(tkns) > 0:
                tokens.append(tkns)
            
        insts = [instruction]

        return insts