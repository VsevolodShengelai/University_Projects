from tkinter import *
from matplotlib.backends.backend_tkagg import (
    FigureCanvasTkAgg, NavigationToolbar2Tk)
# Implement the default Matplotlib key bindings.
from matplotlib.backend_bases import key_press_handler
from matplotlib.figure import Figure
import numpy as np
import math
import matplotlib.pyplot as plt

root = Tk()
root.title("")

back = PhotoImage(file="damaged_metal_orig.2.png")
Hdd =PhotoImage(file="imgonline-com-ua-Transparent-backgr-kYcUcxjPauJWMGD.png") 
interface =PhotoImage(file="imgonline-com-ua-Resize-RbmtiTZkMPDM.png")

#Описание полотна для пульта управления
canvas = Canvas(root, width = 250, height = 600,
                bg = 'blue', bd = 0,
                borderwidth = 0,
                highlightthickness = 0) 
canvas.grid(row=0, column=0, pady=0)
canvas.create_image(0, 0, image = back, anchor = NW)
canvas.create_image(10, 450, image = Hdd, anchor = NW)
canvas.create_image(44, 450, image = Hdd, anchor = NW)

#Это Canvas для будущего графика траектории движения тела
canvas2 = Canvas(root, width = 600, height = 600,
                bg = 'blue', bd = 0,
                borderwidth = 0,
                highlightthickness = 0) 
canvas2.grid(row=0, column=1, pady=0)
canvas2.create_image(0, 0, image = interface, anchor = NW)



#Верхний ряд кнопок и индикаторов - наличие питания пульта
Ison = False
def  rwr_pressed():
    global Ison
    Ison = not(Ison)
    if Ison == True:
        canvas.create_oval(
    120, 10, 145, 35, outline="chartreuse3", 
    fill="green2", width=2
)
        canvas.create_oval(
    150, 10, 175, 35, outline="brown2", 
    fill="ivory2", width=2
)
        InfoPanel.insert( END, ">Пульт включён\n")
        InfoPanel.see("end")

    else:
        canvas.create_oval(
    150, 10, 175, 35, outline="brown2", 
    fill="red2", width=2
)
        canvas.create_oval(
    120, 10, 145, 35, outline="chartreuse3", 
    fill="ivory2", width=2
)
        InfoPanel.insert( END, ">Пульт выключен\n")
        InfoPanel.see("end")        
        
pwr_button = Button(canvas, text="PWR", bg = "cyan2",                    	
                    activebackground = "DeepSkyBlue2", 	
                    command = rwr_pressed)
pwr_button_window = canvas.create_window(10, 10, anchor=NW, window=pwr_button)
 
canvas.create_oval(
    120, 10, 145, 35, outline="chartreuse3", 
    fill="ivory2", width=2
)

canvas.create_oval(
    150, 10, 175, 35, outline="brown2", 
    fill="red2", width=2
)


#Угол и скорость - выбор параметров (глоб. пер угол и скорость) + коэфф лобового
  ##Текстовые таблички
VelText = Label(canvas,text="Скорость, м/с", width = 14)
AngText = Label(canvas,text="Угол вылета, °", width = 14)
Mass = Label(canvas,text="Масса тела, кг", width = 14)
Shag_integr = Label(canvas,text="Шаг интегр, с", width = 14)
gravity = Label(canvas,text="Ускорение g, м/с²", width = 14)
coeff = Label(canvas,text="Коэффициент а.с", width = 14)
 
  
VelText. place(x=10,y=60)
AngText. place(x=10,y=120)
Mass. place(x=10,y=180)
Shag_integr. place(x=10,y=240)
gravity. place(x=10,y=300)
coeff. place(x=10,y=360)


  ##Spinboxes
velocity = Spinbox(canvas, from_=0, to=10, width=15)
velocity.place(x=10,y=84)

angle = Spinbox(canvas, from_=0, to=90, width=15)
angle.place(x=10,y=144)

mass = Spinbox(canvas, from_=0, to=300000  , width=15)
mass.place(x=10,y=204)

Shag = Spinbox(canvas, from_=0.001, to=10  , width=15)
Shag.place(x=10,y=264)

Grav = Spinbox(canvas, from_=0, to=10  , width=15)
Grav.place(x=10,y=324)

Coeff = Spinbox(canvas, from_=0, to=10  , width=15)
Coeff.place(x=10,y=384)


  ##Buttons
U = None ## Чтобы не ломалась программа на кнопке Reset
a = None
m = None
t = None
g = None
k = None
def InButton1_pressed():
    global U
    U = float(velocity.get())
    if U>=0 and U < 300000:
        InfoPanel.insert( END, ">Скорость U = "+ str(U)+" м/с \n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert( END, ">Ошибка:Указано недопустимое значение скорости \n")
        InfoPanel.see("end")  
def InButton2_pressed():
    global a
    a = float(angle.get())
    if a >=0 and a < 90:
        InfoPanel.insert( END, ">Угол a = "+ str(a)+"°\n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert( END, ">Ошибка:Указано недопустимое значение угла \n")
        InfoPanel.see("end")
def InButton3_pressed():
    global m
    m = float(mass.get())
    if m >=0:
        InfoPanel.insert( END, ">Macca m = "+ str(m)+"кг\n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert( END, ">Ошибка:Указано недопустимое значение массы \n")
        InfoPanel.see("end")
def InButton4_pressed():
    global t
    t = float(Shag.get())
    if t >=0:
        InfoPanel.insert( END, ">Шаг интегрирования = "+ str(t)+"с\n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert( END, ">Ошибка:Указано недопустимое значение шага интегрирования \n")
        InfoPanel.see("end")
def InButton5_pressed():
    global g
    g = float(Grav.get())
    if g >0:
        InfoPanel.insert( END, ">Ускорение g = "+ str(g)+" м/с²\n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert( END, ">Ошибка:Указано недопустимое значение ускорения g\n")
        InfoPanel.see("end")
def InButton6_pressed():
    global k
    k = float(Coeff.get())
    if k >=0:
        InfoPanel.insert( END, ">Коеффициент а.с. = "+ str(k)+" \n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert( END, ">Ошибка:Указано недопустимое значение ускорения g\n")
        InfoPanel.see("end")
        
def ResetButton1_pressed():
    global U
    U = None
    InfoPanel.insert( END, ">Сброс скорости"+"\n")
    InfoPanel.see("end")
def ResetButton2_pressed():
    global a
    a = None
    InfoPanel.insert( END, ">Сброс угла вылета"+"\n")
    InfoPanel.see("end")
def ResetButton3_pressed():
    global m
    m = None
    InfoPanel.insert( END, ">Сброс массы тела"+"\n")
    InfoPanel.see("end")
def ResetButton4_pressed():
    global t
    t = None
    InfoPanel.insert( END, ">Сброс шага интегрирования"+"\n")
    InfoPanel.see("end")
def ResetButton5_pressed():
    global g
    g = None
    InfoPanel.insert( END, ">Сброс ускорения g"+"\n")
    InfoPanel.see("end")
def ResetButton6_pressed():
    global k
    k = None
    InfoPanel.insert( END, ">Сброс коэффициента а.с."+"\n")
    InfoPanel.see("end")


def Return_InButton_standart(command):
    InButton = Button(canvas, text="IN", bg = "Light Yellow3",                    	
                    activebackground = "Light Yellow2",
                    width = 4, height = 2, command = command
                    )
    return InButton
def Return_ResetButton_standart(command):
    ResetButton = Button(canvas, text="Reset", bg = "Light Yellow3",                    	
                    activebackground = "Light Yellow2",
                    width = 8, height = 2, command = command
                    )
    return ResetButton
def AutoButton6_pressed():
    global k
    k = 0.5
    InfoPanel.insert( END, ">Коеффициент а.с. = "+ str(k)+" \n")
    InfoPanel.see("end")
    
InButton1 = Return_InButton_standart(InButton1_pressed)
InButton2 = Return_InButton_standart(InButton2_pressed)
InButton3 = Return_InButton_standart(InButton3_pressed)
InButton4 = Return_InButton_standart(InButton4_pressed)
InButton5 = Return_InButton_standart(InButton5_pressed)
ResetButton1 = Return_ResetButton_standart(ResetButton1_pressed)
ResetButton2 = Return_ResetButton_standart(ResetButton2_pressed)
ResetButton3 = Return_ResetButton_standart(ResetButton3_pressed)
ResetButton4 = Return_ResetButton_standart(ResetButton4_pressed)
ResetButton5 = Return_ResetButton_standart(ResetButton5_pressed)
AutoButton6 = Button(canvas, text="Auto", bg = "Light Yellow3",                    	
                    activebackground = "Light Yellow2",
                    width = 3, height = 2, command = AutoButton6_pressed 
                    )
InButton6 = Button(canvas, text="IN", bg = "Light Yellow3",                    	
                    activebackground = "Light Yellow2",
                    width = 3, height = 2, command = InButton6_pressed 
                    )
ResetButton6 = Button(canvas, text="Reset", bg = "Light Yellow3",                    	
                    activebackground = "Light Yellow2",
                    width = 4, height = 2, command = ResetButton6_pressed 
                    )

InButton_vel_window = canvas.create_window(120, 62, anchor=NW, window=InButton1)
InButton_ang_window = canvas.create_window(120, 122, anchor=NW, window=InButton2)
InButton_mas_window = canvas.create_window(120, 182, anchor=NW, window=InButton3)
InButton_itr_window = canvas.create_window(120, 242, anchor=NW, window=InButton4)
InButton_grv_window = canvas.create_window(120, 302, anchor=NW, window=InButton5)
ResetButton_vel_window = canvas.create_window(160, 62, anchor=NW, window=ResetButton1)
ResetButton_ang_window = canvas.create_window(160, 122, anchor=NW, window=ResetButton2)
ResetButton_mas_window = canvas.create_window(160, 182, anchor=NW, window=ResetButton3)
ResetButton_itr_window = canvas.create_window(160, 242, anchor=NW, window=ResetButton4)
ResetButton_grv_window = canvas.create_window(160, 302, anchor=NW, window=ResetButton5)
AutoButton_coeff_window = canvas.create_window(120, 362, anchor=NW, window=AutoButton6)
InButton_coeff_window = canvas.create_window(153, 362, anchor=NW, window=InButton6)
ResetButton_coeff_window = canvas.create_window(186, 362, anchor=NW, window=ResetButton6)



#Информационная панель

InfoPanel = Text(canvas, bg = "gray25", bd=2,
                 width = 26, height =5, fg="green2")
InfoPanel_window = canvas.create_window(9, 412, anchor=NW, window=InfoPanel)

#Кнопки ПУСК
    ##Выводим главный график на экран
def Pusk_in_vacuum():
    global U
    global a
    U = float(U)
    a = float(a)
    if not (a != None and U!=None):
        InfoPanel.insert( END, ">Пуск невозможен"+"\n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert( END, ">Пуск совершён"+"\n")
        InfoPanel.see("end")


        PI = 3.14
        a1 = (PI * float(a)) / 180
        g = 9.8
        L = U*U*math.sin(2*a1)/g
        H = U*U*(math.sin(a1)**2)/(2*g)
        x = np.linspace(0, L, 100)
        y = x*math.sin(a1)/math.cos(a1)-x*x*9.8/2/U/U/math.cos(a1)/math.cos(a1)
        
        fig, ax = plt.subplots(figsize=(6,5), dpi=100)

        if L > H:
            plt.ylim(0, L)
        else:
            plt.xlim(0, H)
        ax.plot(x, y)
        #print("a = ",a, "  L= ", L, "  H =", H)

        plt.xlabel('Ось x', fontsize=10, color='blue')
        plt.ylabel('Ось y', fontsize=10, color='blue')


        canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
        canvas.draw()
        canvas.get_tk_widget().grid(row=0, column=1, pady=0)

def Pusk_with_coeff():
    global U, a, m, t, g, k
    selfU = U
    selfa = a
    selfm = m
    selft = t
    selfg = g
    selfk = k
    if U == None or a == None or m == None or t == None or g == None or k == None:
            InfoPanel.insert( END, ">Пуск невозможен"+"\n")
            InfoPanel.see("end")
    else:
        time = 0
        dt = t
        y = 0
        x = 0
        α = a
        Ux = U*math.cos(α*math.pi/180)
        Uy = U*math.sin(α*math.pi/180)
        F = k*U**2
        Fx = F*math.cos((α+180)*math.pi/180)
        Fy = F*math.sin((α+180)*math.pi/180)
        ax = Fx/m
        ay = (Fy/m)-g
        #i = 0
        global listx
        global listy
        global listt
        global listU
        global lista
        listx = []
        listx.append(x)
        listy = []
        listy.append(y)
        listt = []
        listt.append(time)
        listU = []
        listU.append(U)
        lista = []
        lista.append(a)
        while y >=0:
            time = time+dt
            listt.append(time)
            Ux = Ux+ax*dt
            Uy = Uy+ay*dt
            U = math.sqrt(Ux**2+Uy**2)
            listU.append(U)
            α = math.atan(Uy/Ux)*180/math.pi
            F = k*U**2
            Fx = F*math.cos((α+180)*math.pi/180)
            Fy = F*math.sin((α+180)*math.pi/180)
            ax = Fx/m
            ay = (Fy/m) - g
            a = math.sqrt(ax**2+ay**2)
            lista.append(a)
            x = x + Ux*dt
            x = round(x,0) 
            listx.append(x)
            y = y + Uy*dt
            y = round(y,0)
            listy.append(y)
            #i = i+1
            #print(i)
         
        fig, ax = plt.subplots(figsize=(6,5), dpi=100)

        #print(max(listy), ' ', listx[-1])
        
        if listx[-1] > max(listy):
            plt.ylim(0, listx[-1])
        else:
            plt.xlim(0, max(listy))
        ax.plot(listx, listy)


        plt.xlabel('Ось x', fontsize=10, color='blue')
        plt.ylabel('Ось y', fontsize=10, color='blue')


        canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
        canvas.draw()
        canvas.get_tk_widget().grid(row=0, column=1, pady=0)
        #Вернём обратно глобальные переменные
        U = selfU
        a = selfa
        m = selfm
        t = selft
        g = selfg
        k = selfk
        GraphButton1.config(state = NORMAL)
        GraphButton2.config(state = NORMAL)
        GraphButton3.config(state = NORMAL)
        GraphButton4.config(state = NORMAL)
        GraphButton5.config(state = NORMAL) 
    
PuskButton = Button(canvas, text="Пуск с \n учётом а.с.", bg = "lime green",                    	
                    activebackground = "Light Yellow2",
                    width = 10, height = 3, command = Pusk_with_coeff 
                    )
PuskButton_window = canvas.create_window(32, 520, anchor=NW, window=PuskButton)

PuskButton2 = Button(canvas, text="Пуск \n в вакууме", bg = "lime green",                    	
                    activebackground = "Light Yellow2",
                    width = 10, height = 3 , command = Pusk_in_vacuum
                    )
PuskButton_window = canvas.create_window(126, 520, anchor=NW, window=PuskButton2)

  ##Кнопки управления графиками
def Pusk2():
        fig, ax = plt.subplots(figsize=(6,5), dpi=100)

        ax.plot(listt, listU)

        plt.xlabel('Ось t', fontsize=10, color='blue')
        plt.ylabel('Ось U', fontsize=10, color='blue')

        canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
        canvas.draw()
        canvas.get_tk_widget().grid(row=0, column=1, pady=0)
def Pusk3():
        fig, ax = plt.subplots(figsize=(6,5), dpi=100)

        ax.plot(listt, lista)

        plt.xlabel('Ось t', fontsize=10, color='blue')
        plt.ylabel('Ось |a|', fontsize=10, color='blue')

        canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
        canvas.draw()
        canvas.get_tk_widget().grid(row=0, column=1, pady=0)
def Pusk4():
        fig, ax = plt.subplots(figsize=(6,5), dpi=100)

        ax.plot(listt, listx)

        plt.xlabel('Ось t', fontsize=10, color='blue')
        plt.ylabel('Ось x', fontsize=10, color='blue')

        canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
        canvas.draw()
        canvas.get_tk_widget().grid(row=0, column=1, pady=0)
def Pusk5():
        fig, ax = plt.subplots(figsize=(6,5), dpi=100)

        ax.plot(listt, listy)

        plt.xlabel('Ось t', fontsize=10, color='blue')
        plt.ylabel('Ось y', fontsize=10, color='blue')

        canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
        canvas.draw()
        canvas.get_tk_widget().grid(row=0, column=1, pady=0)
    
GraphButton1 = Button(canvas2, text="1",fg = 'DeepSkyBlue4', bg = "snow4",                    	
                    activebackground = "Light Yellow2",
                    width = 3, height = 1, command = Pusk_with_coeff, state =  DISABLED 
                    )
GraphButton2 = Button(canvas2, text="2", bg = "thistle3", fg = 'DeepSkyBlue4',                    	
                    activebackground = "Light Yellow2",
                    width = 3, height = 1, command = Pusk2, state =  DISABLED  
                    )
GraphButton3 = Button(canvas2, text="3", bg = "khaki", fg = 'DeepSkyBlue4',                   	
                    activebackground = "Light Yellow2",
                    width = 3, height = 1, command = Pusk3, state =  DISABLED  
                    )
GraphButton4 = Button(canvas2, text="4", bg = "SeaGreen2", fg = 'DeepSkyBlue4',                  	
                    activebackground = "Light Yellow2",
                    width = 3, height = 1, command = Pusk4, state =  DISABLED  
                    )
GraphButton5 = Button(canvas2, text="5", bg = "snow", fg = 'DeepSkyBlue4',                  	
                    activebackground = "Light Yellow2",
                    width = 3, height = 1, command = Pusk5, state =  DISABLED  
                    )
PuskButton_window = canvas2.create_window(20, 10, anchor=NW, window=GraphButton1)
PuskButton_window = canvas2.create_window(60, 10, anchor=NW, window=GraphButton2)
PuskButton_window = canvas2.create_window(100, 10, anchor=NW, window=GraphButton3)
PuskButton_window = canvas2.create_window(140, 10, anchor=NW, window=GraphButton4)
PuskButton_window = canvas2.create_window(180, 10, anchor=NW, window=GraphButton5)

root.mainloop()
    
    
