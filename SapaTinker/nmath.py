from math import exp
from ion import *

#ion exchange rates
def calc_An(v:float):
    res = (
        (0.01*(v+50.0))/
        (1.0-exp((-(v+50.0)/10.0)))
        )

    return res

def calc_Bn(v:float):
    return 0.125*exp(-(v+60.0)/80.0)

def calc_Am(v:float):
    return (
        (0.1*(v+35.0))/
        (1-exp((-(v+35.0))/10.0))
    )

def calc_Bm(v:float):
    return 4.0*exp(-0.0556*(v+60.0))

def calc_Ah(v:float):
    return 0.07*exp(-0.05*(v+60.0))

def calc_Bh(v:float):
    return 1.0/(1.0+exp(-0.1*(v+30.0)))

#deltas
def calc_dndt(v:float, n:float):
    return (calc_An(v)*(1-n))-(calc_Bn(v)*n)

def calc_dmdt(v:float, m:float):
    return (calc_Am(v)*(1-m))-(calc_Bm(v)*m)

def calc_dhdt(v:float, h:float):
    return (calc_Ah(v)*(1-h))-(calc_Bh(v)*h)

def calc_dvdt(i0:float, i1:float, iExt:float=0.0):
    return (.001/C_Cm)*(i0-i1+iExt)

def calc_current(v:float, n:float, m:float, h:float):
    return ( 
        (C_GNa*m**3*h*(v-C_ENa))-
        (C_Gk*n**4*(v-C_Ek))-
        (C_Gl*(v-C_El))
    )
