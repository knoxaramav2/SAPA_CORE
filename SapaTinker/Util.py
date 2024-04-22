
def attempt(defval, fnc, *args):
    try:
        fnc(*args)
    except:
        return defval

def combine_funcs(*fncs):
    def combine(*args, **argvs):
        for f in fncs: 
            f(args, argvs)
    return combine