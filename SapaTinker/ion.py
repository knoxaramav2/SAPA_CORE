from enum import Enum

Ion = Enum(
    'Ion',['Na', 'K', 'Cl', 'Ca', 'L']
)

C_RST_M = 0.05#%[0.05-0.10]
C_RST_H = 0.90#%[0.60-0.90]
C_RST_N = 0.40#%[0.40-0.70]
C_RST_I = -4.15

C_REST = -55.0#mV
C_Vm = -70.0#mV

#Precalculated reversal potentials
C_Cm = 0.01#.0.1#uF/cm^2
C_ENa = 55.17#mV
C_Ek = -72.14#mV
C_El = -49.42#mV

#ion conductivity (millisiemens)
C_GNa = 1.2#mS/cm^2
C_Gk = 0.36#mS/cm^2
C_Gl = 0.003#mS/cm^2