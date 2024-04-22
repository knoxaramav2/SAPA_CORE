from Data import DataHistory, DataPoint
from Monitor import Monitor
import math
import keyboard
from os import path, makedirs, listdir
import sys

from neuron import *

categories = ["Input"]
network = Network()
data:DataHistory = None

charge = 0.1
time = 0.0
dt = 0.4
pressed = False
is_pulse = False
pulse0 = 0
pulse1 = 0
angle = 0
AMP = .001
dA = math.pi / 200
def input_capture():
    global data, charge, network, time, dt, pressed, is_pulse, pulse0, pulse1, angle, dA

    #charge = 0

    if keyboard.is_pressed('a'): charge -= .01
    if keyboard.is_pressed('d'): charge += .01
    if keyboard.is_pressed('w'): charge += .03
    if keyboard.is_pressed('s'): charge -= .03
    xprss = int(keyboard.is_pressed('+') | (keyboard.is_pressed('-')*2))

    # if pulse1 > pulse0:
    #     pulse0 = pulse1
    #     if pulse0 % 3 == 0: charge *= 2

    if not pressed and (xprss > 0 and xprss < 3): 
        charge += .02*(1-2*(xprss&2!=0))
        dA += (math.pi/50)*(1-2*(xprss&2!=0))
        charge = max(0, charge + .1*(1-2*(xprss&2!=0)))
    # charge = AMP+(AMP * math.sin(angle))
    # angle += dA
    # pressed = xprss != 0
    # charge = 0* 0.1 if pulse == 0 else 0
    data.add_point("Input", "v", DataPoint(charge))
    #data.add_point("Input", "dv", DataPoint(time))
    network.excite(charge)

    #time += dt

def update_network():
    global network, pulse1
    network.update()

    for id in network.list_neurons():
        nrn = network.get_neuron(id)
        #data.add_point(id, DataPoint(nrn.v))
        if id == 'N1': pulse1 = nrn.p
        data.add_point(id, 'n', DataPoint(nrn.n))
        data.add_point(id, 'm', DataPoint(nrn.m))
        data.add_point(id, 'h', DataPoint(nrn.h))
        data.add_point(id, 'p', DataPoint(nrn.p))
        data.add_point(id, 't', DataPoint(nrn.t))
        data.add_point(id, 'v', DataPoint(nrn.v))
        data.add_point(id, 'dv', DataPoint(nrn.dv))
        data.add_point(id, 'i', DataPoint(nrn.input_i))
        data.add_point(id, 'refact', DataPoint(int(nrn.refact)*5))
        nrn.input_i = 0


def update():
    input_capture()
    update_network()
    
def init_network():
    global network

    nrn1 = Neuron("N1")
    nrn2 = Neuron("N2")

    nrn3 = Neuron("N3")
    nrn4 = Neuron("N4")
    nrn5 = Neuron("N5")

    nrn6 = Neuron("N6")
    nrn7 = Neuron("N7")

    nrn8 = Neuron("N8")
    
    nrn3.connect(nrn1, 0.8)
    nrn3.connect(nrn6, 1.0)
    nrn4.connect(nrn1, 0.8)
    nrn4.connect(nrn2, 1.35)
    nrn4.connect(nrn3, 1.0)
    nrn4.connect(nrn5, -1.0)
    nrn5.connect(nrn2, 1.35)
    nrn5.connect(nrn7, 1.0)
    nrn6.connect(nrn4, 1.0)
    nrn7.connect(nrn4, 1.0)
    nrn8.connect(nrn4, 1.0)

    network.insert(nrn1, True)
    network.insert(nrn2, True)
    network.insert(nrn3, False)
    network.insert(nrn4, False)
    network.insert(nrn5, False)
    network.insert(nrn6, False)
    network.insert(nrn7, False)
    network.insert(nrn8, False)

def init_data():
    global data, network, categories
    cats = ['n', 'h', 'm', 'v', 'p', 't', 'dv', 'i', 'refact']

    # for nrn in network.list_neurons():
    #     categories.extend([f'{nrn}' for x in cats])

    #categories.extend(network.list_neurons())
    data = DataHistory(cats, 100)

init_network()
init_data()

monitor = Monitor(data, update, network)
monitor.show()

csv = data.get_csv()
base_dir = path.join(path.dirname(sys.argv[0]), 'logs')
makedirs(base_dir, exist_ok=True)

base_file = 'n8'
files = listdir(base_dir)
file_name = f'{base_file}_1.csv'
i = 2
while file_name in files:
    file_name = f'{base_file}_{i}.csv'
    i += 1

f = open(path.join(base_dir, file_name), 'w')
f.write(csv)
f.close()

print(f'Saved {file_name}')
