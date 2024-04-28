from __future__ import annotations
import math
from ion import *
from nmath import *

DT = .03

class Neuron:

    name  : str
    v: float = C_Vm
    
    n:float = C_RST_N
    h:float = C_RST_H
    m:float = C_RST_M
    #i:float = C_RST_I
    t:float = 0.0
    dv:float = 0.0
    input_i = 0.0
    thresh = -C_ENa
    refact = False

    _ispulse:bool = False
    p:int = 0

    connections : list[tuple[float, Neuron]]

    def connect(self, pred:Neuron, weight:float):
        self.connections.append((weight, pred))

    def is_active(self):
        return self.is_active

    def excite(self, current:float):
        self.input_i = current

    def update(self):
        for w, n in self.connections:
            self.input_i += w*n.t

        #self.input_i = .1
        #if self.name != 'N1': return
        #if self.refact: self.input_i = 0
        #self.input_i = .1

        #print(f'{self.name} V={self.v:.3f} DV={self.dv:.3f} I={self.i:.3f} N={self.n:.3f} M={self.m:.3f} H={self.h:.3f}')

        dn = calc_dndt(self.v, self.n) * DT
        dh = calc_dhdt(self.v, self.h) * DT
        dm = calc_dmdt(self.v, self.m) * DT

        gNa = C_GNa*(self.m**3)*self.h
        gK = C_Gk*(self.n**4)
        gL = C_Gl

        INa = gNa*(self.v-C_ENa)
        Ik = gK*(self.v-C_Ek)
        Il = gL*(self.v-C_El)
        I = INa+Ik+Il
        #print(f'INa:{INa:.2f} | IK:{Ik:.2f} | IL:{Il:.2f} :: {self.input_i} - {INa+Ik+Il} = {self.input_i-(INa+Ik+Il)}')
        dv = ((1/C_Cm)*(self.input_i-I)) * DT

        self.n += dn
        self.h += dh
        self.m += dm
        self.v += dv
        self.dv = dv
        self.t = max(self.m-self.h, 0)

        #Not used in simulation level
        if (not self.refact and self.v >= self.thresh):
            self.refact = True
            self.p += 1
        elif (self.refact and self.v < C_REST):
            self.refact = False

    def __init__(self, name:str):
        self.name = name
        v0 = C_REST
        self.connections = []
        self.m = calc_Am(v0)/(calc_Am(v0)+calc_Bm(v0))
        self.n = calc_An(v0)/(calc_An(v0)+calc_Bn(v0))
        self.h = calc_Ah(v0)/(calc_Ah(v0)+calc_Bh(v0))

class Network:
    __network : dict[str, Neuron] = {}
    __inputs: list[Neuron] = []

    def get_neuron(self, name:str) -> Neuron:
        return self.__network[name]

    def list_neurons(self) -> list[str]:
        return list(self.__network.keys())

    def __init__(self):
        self.__network = {}

    def update(self):
        for nrn in self.__network.values():
            nrn.update()

    def excite(self, charge:float):
        for nrn in self.__inputs:
            nrn.excite(charge)

    def insert(self, neuron:Neuron, as_input:bool):
        self.__network[neuron.name] = neuron
        if as_input: self.__inputs.append(neuron)