from __future__ import annotations
from functools import partial
from random import randint
import tkinter as tk
from tkinter import IntVar, Tk, Button, Checkbutton, Frame, Label, Canvas, StringVar, Radiobutton, Entry
from Data import *
from neuron import Network
   
class Monitor:
    
    __root              : Tk
    __data              : DataHistory
    __speed             : int = 5
    __input_callback    : Callable = None
    __graph             : Canvas
    __data_lines        : dict
    __labels            : dict[str, StringVar]
    __network           : Network
    __selected          : str = None
    __pause             : bool = False
    __step              : bool = False

    def __toTkClr(self, color:tuple[int, int, int]) -> str:
        return "#%02x%02x%02x" % color

    def __update_graph_line(self, sid:str):
        pass

    def __update(self):
        if self.__pause and not self.__step: return
        self.__step = False

        categories = self.__data.get_categories()
        self.__input_callback()
        width = self.__graph.winfo_reqwidth()
        height = self.__graph.winfo_reqheight()
        grph = self.__graph

        min_max = self.__data.min_max(10)
        my = height / 2
        diff_y = min_max[1]-min_max[0]
        z_point = (min_max[1]/diff_y)*height

        zero_idx = self.__data_lines['zero']
        zero_coords = self.__graph.coords(zero_idx)
        zero_coords[1] = z_point
        zero_coords[3] = z_point
        grph.coords(zero_idx, zero_coords)

        self.__labels['Input'].set(f"{self.__data.get_last('Input', 'v'):.3f}")

        sid = self.__selected
        if sid != None:
            for catid in categories:
                idx = self.__data_lines[catid]
                coords = self.__graph.coords(idx)
                data = self.__data.get_category_values(sid, catid)
                sz = len(data)
                val = 0 if len(data) == 0 else data[-1].value
                nttl = f'{val:.5f}'
                self.__labels[catid].set(nttl)
                mult = 10 if catid in ['n','m','h'] else .001 if catid == 'p' else 1
                for i in range(0, sz):
                    y = ((min_max[1]-(data[i].value*mult))/diff_y)*height
                    coords[(i*2)+1] = y
                grph.coords(idx, coords)

        self.__root.after(self.__speed, self.__update)

    def __play_pause(self):
        self.__pause = not self.__pause
        if not self.__pause or self.__step:
            self.__root.after(self.__speed, self.__update)

    def __step_frame(self):
        if not self.__pause: self.__pause = True
        self.__step = True
        if not self.__pause or self.__step:
            self.__root.after(self.__speed, self.__update)

    def show(self):
        self.__root.after(100, self.__update)
        self.__root.update()
        self.__root.mainloop()
    
    def __hook_vars(self, name:str):
        self.__selected = name
        for cat in self.__data.get_categories():
            val = self.__data.get_last(name, cat)
            self.__labels[cat].set(val)

    def __init_graph(self):
        window_width = 1200
        window_height = 800
        graph_width = 800
        data_width = window_width - graph_width
        subdata_height = window_height//2
        my = window_height // 2
        self.__data_lines = {}
        self.__labels = {}
        categories = self.__data.get_categories()

        graph_window = Frame(self.__root, width=window_width, height=window_height)
        graph = Canvas(graph_window, width=graph_width, height=window_height, bg='black')
        data_bar = Frame(graph_window, width=data_width, height=window_height, bg='gray27')
        self.__graph = graph

        ctrl_bar = Frame(data_bar, width=data_width, height=subdata_height, bg='gray61')
        nids = self.__network.list_neurons()

        btn_bar = Frame(ctrl_bar)
        inlbl = Frame(btn_bar)
        invar = StringVar(inlbl, value='0.0')
        self.__labels['Input'] = invar
        Label(inlbl, text='Input: ').pack(anchor='w')
        Label(inlbl, textvariable=invar).pack(anchor='e')
        inlbl.pack(fill='x')
        Button(btn_bar, text='|>', command=self.__play_pause).pack(side='left')
        Button(btn_bar, text='->', command=self.__step_frame).pack(side='left')
        nvar = IntVar(ctrl_bar, value=0)
        ctrl_bar.radvar = nvar
        for i in range(len(nids)):
            nid = nids[i]
            rb = Radiobutton(ctrl_bar, text=nid, variable=nvar, value=i, command=partial(self.__hook_vars, nid))
            rb.pack()

        info_bar = Frame(data_bar, width=data_width, height=subdata_height)
        self.__data_lines['zero']=graph.create_line(0, my, window_width, my, fill='white', width=2)
        dx = graph_width // (self.__data.max_size()-1)
        for category in categories:
            color = self.__data.get_color(category)
            dat_row = Frame(info_bar, width=data_width, height=10)
            Label(dat_row, text=f'{category}::', foreground=self.__toTkClr(color)).pack(side='left')
            strvar = StringVar(dat_row, value=self.__data.get(self.__selected, category))
            lbl = Label(dat_row, textvariable=strvar)
            lbl.pack(side='right', fill='x')
            self.__labels[category] = strvar
            dat_row.pack()

            lines = {}
            self.__data_lines[category] = lines
            coords = []
            for i in range(self.__data.max_size()):
                coords.extend([i*dx, my])
            idx = graph.create_line(coords, fill=self.__toTkClr(color), width=3)
            self.__data_lines[category] = idx
    
        btn_bar.pack(fill='both')
        ctrl_bar.pack(side='top', fill='both')
        info_bar.pack(side='bottom', fill='both')
        graph.pack(side='left')
        data_bar.pack(side='right')
        graph_window.pack()

        if len(nids) > 0: self.__hook_vars(nids[0])

    def __init_window(self):
        self.__root = Tk()
        frm = Frame(self.__root, width=1200)
        self.__init_graph()
        frm.pack()

    def __init__(self, data:DataHistory, input_cb:Callable, network:Network):
        self.__data = data
        self.__input_callback = input_cb
        self.__network = network

        self.__init_window()





