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
Hdd = PhotoImage(file="imgonline-com-ua-Transparent-backgr-kYcUcxjPauJWMGD.png")
interface = PhotoImage(file="imgonline-com-ua-Resize-RbmtiTZkMPDM.png")

# Описание полотна для пульта (поля ввода данных)
canvas = Canvas(root, width=250, height=600,
                bg='blue', bd=0,
                borderwidth=0,
                highlightthickness=0)
canvas.grid(row=0, column=0, pady=0)
canvas.create_image(0, 0, image=back, anchor=NW)
canvas.create_image(10, 450, image=Hdd, anchor=NW)
canvas.create_image(44, 450, image=Hdd, anchor=NW)

# Это Canvas для будущего графика траектории движения тела
canvas2 = Canvas(root, width=600, height=600,
                 bg='blue', bd=0,
                 borderwidth=0,
                 highlightthickness=0)
canvas2.grid(row=0, column=1, pady=0)
canvas2.create_image(0, 0, image=interface, anchor=NW)

# Угол и скорость - выбор параметров (глоб. пер угол и скорость) + коэфф лобового сопр.
##Текстовые таблички
VelText = Label(canvas, text="Скорость, м/с", width=14)
AngText = Label(canvas, text="Угол вылета, °", width=14)
Mass = Label(canvas, text="Уст. скорость, м/с", width=14)
Shag_integr = Label(canvas, text="Шаг интегр, с", width=14)
gravity = Label(canvas, text="Ускорение g, м/с²", width=14)


VelText.place(x=10, y=10)
AngText.place(x=10, y=70)
Mass.place(x=10, y=130)
Shag_integr.place(x=10, y=190)
gravity.place(x=10, y=250)


##Spinboxes
velocity = Spinbox(canvas, from_=0, to=10, width=15)
velocity.place(x=10, y=34)

angle = Spinbox(canvas, from_=0, to=90, width=15)
angle.place(x=10, y=94)

mvelocity = Spinbox(canvas, from_=0, to=300000, width=15)
mvelocity.place(x=10, y=154)

Shag = Spinbox(canvas, from_=0.001, to=10, width=15)
Shag.place(x=10, y=214)

Grav = Spinbox(canvas, from_=0, to=10, width=15)
Grav.place(x=10, y=274)


##Buttons
U = None  ## Чтобы не ломалась программа на кнопке Reset
a = None
Um = None
t = None
g = None


# Информационная панель

InfoPanel = Text(canvas, bg="gray25", bd=2,
                 width=26, height=8, fg="green2")
InfoPanel_window = canvas.create_window(9, 375, anchor=NW, window=InfoPanel)


# Кнопки меню ПУСК
##Выводим главный график на экран
def Pusk_in_vacuum():
    global U
    global a
    U = float(U)
    a = float(a)
    if not (a != None and U != None):
        InfoPanel.insert(END, ">Пуск невозможен" + "\n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert(END, ">Пуск совершён" + "\n")
        InfoPanel.see("end")

        PI = 3.14
        a1 = (PI * float(a)) / 180
        g = 9.8
        L = U * U * math.sin(2 * a1) / g
        H = U * U * (math.sin(a1) ** 2) / (2 * g)
        x = np.linspace(0, L, 100)
        y = x * math.sin(a1) / math.cos(a1) - x * x * 9.8 / 2 / U / U / math.cos(a1) / math.cos(a1)

        fig, ax = plt.subplots(figsize=(6, 5), dpi=100)

        if L > H:
            plt.ylim(0, L)
        else:
            plt.xlim(0, H)
        ax.plot(x, y)
        # print("a = ",a, "  L= ", L, "  H =", H)

        plt.xlabel('Ось x', fontsize=10, color='blue')
        plt.ylabel('Ось y', fontsize=10, color='blue')

        canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
        canvas.draw()
        canvas.get_tk_widget().grid(row=0, column=1, pady=0)

def check_data():
    U = float(velocity.get())
    if U >= 0 and U < 300000:
        InfoPanel.insert(END, ">Скорость U = " + str(U) + " м/с \n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert(END, ">Ошибка:Указано недопустимое значение скорости \n")
        InfoPanel.see("end")
        return

    a = float(angle.get())
    if a >= 0 and a < 90:
        InfoPanel.insert(END, ">Угол a = " + str(a) + "°\n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert(END, ">Ошибка:Указано недопустимое значение угла \n")
        InfoPanel.see("end")
        return

    Um = float(mvelocity.get())
    if Um >= 0:
        InfoPanel.insert(END, ">Скорость Um = " + str(Um) + "м/с \n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert(END, ">Ошибка:Указано недопустимое значение уст. скорости \n")
        InfoPanel.see("end")
        return

    global t
    t = float(Shag.get())
    if t >= 0:
        InfoPanel.insert(END, ">Шаг интегрирования = " + str(t) + "с\n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert(END, ">Ошибка:Указано недопустимое значение шага интегрирования \n")
        InfoPanel.see("end")
        return

    global g
    g = float(Grav.get())
    if g > 0:
        InfoPanel.insert(END, ">Ускорение g = " + str(g) + " м/с²\n")
        InfoPanel.see("end")
    else:
        InfoPanel.insert(END, ">Ошибка:Указано недопустимое значение ускорения g\n")
        InfoPanel.see("end")
        return

    return U, a, Um, t, g


def Pusk_with_coeff():
    global U, a, Um, t, g

    U, a, Um, t, g = check_data()

    selfU = U
    selfa = a
    selfUm = Um
    selft = t
    selfg = g
    
    if U == None or a == None or Um == None or t == None or g == None:
        InfoPanel.insert(END, ">Пуск невозможен" + "\n")
        InfoPanel.see("end")
    else:
        k = 0
        time = 0
        dt = t
        y = 0
        x = 0
        α = a
        Ux = U * math.cos(α * math.pi / 180)
        Uy = U * math.sin(α * math.pi / 180)

        ax = 0
        ay = 0

        # i = 0
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
        while y >= 0:
            time = time + dt
            listt.append(time)
            Ux = Ux + ax * dt
            Uy = Uy + ay * dt
            U = math.sqrt(Ux ** 2 + Uy ** 2)
            listU.append(U)
            α = math.atan(Uy / Ux) * 180 / math.pi

            ax = -g/(Um**2)*U*Ux
            ay = -g/(Um**2)*U*Uy - g

            a = math.sqrt(ax ** 2 + ay ** 2)
            lista.append(a)
            x = x + Ux * dt
            #print(x)
            x = round(x, 3)
            listx.append(x)
            y = y + Uy * dt
            #print(y)
            y = round(y, 3)
            listy.append(y)
            # i = i+1
            # print(i)

        fig, ax = plt.subplots(figsize=(6, 5), dpi=100)

        # print(max(listy), ' ', listx[-1])

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
        # Вернём обратно глобальные переменные
        U = selfU
        a = selfa
        Um = selfUm
        t = selft
        g = selfg

        GraphButton1.config(state=NORMAL)
        GraphButton2.config(state=NORMAL)
        GraphButton3.config(state=NORMAL)
        GraphButton4.config(state=NORMAL)
        GraphButton5.config(state=NORMAL)


PuskButton = Button(canvas, text="Пуск!", bg="lime green",
                    activebackground="Light Yellow2",
                    width=22, height=2, command=Pusk_with_coeff
                    )
PuskButton_window = canvas.create_window(32, 540, anchor=NW, window=PuskButton)



##Кнопки управления графиками
def Pusk2():
    fig, ax = plt.subplots(figsize=(6, 5), dpi=100)

    ax.plot(listt, listU)

    plt.xlabel('Ось t', fontsize=10, color='blue')
    plt.ylabel('Ось U', fontsize=10, color='blue')

    canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
    canvas.draw()
    canvas.get_tk_widget().grid(row=0, column=1, pady=0)


def Pusk3():
    fig, ax = plt.subplots(figsize=(6, 5), dpi=100)

    ax.plot(listt, lista)

    plt.xlabel('Ось t', fontsize=10, color='blue')
    plt.ylabel('Ось |a|', fontsize=10, color='blue')

    canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
    canvas.draw()
    canvas.get_tk_widget().grid(row=0, column=1, pady=0)


def Pusk4():
    fig, ax = plt.subplots(figsize=(6, 5), dpi=100)

    ax.plot(listt, listx)

    plt.xlabel('Ось t', fontsize=10, color='blue')
    plt.ylabel('Ось x', fontsize=10, color='blue')

    canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
    canvas.draw()
    canvas.get_tk_widget().grid(row=0, column=1, pady=0)


def Pusk5():
    fig, ax = plt.subplots(figsize=(6, 5), dpi=100)

    ax.plot(listt, listy)

    plt.xlabel('Ось t', fontsize=10, color='blue')
    plt.ylabel('Ось y', fontsize=10, color='blue')

    canvas = FigureCanvasTkAgg(fig, master=root)  # A tk.DrawingArea.
    canvas.draw()
    canvas.get_tk_widget().grid(row=0, column=1, pady=0)


GraphButton1 = Button(canvas2, text="1", fg='DeepSkyBlue4', bg="snow4",
                      activebackground="Light Yellow2",
                      width=3, height=1, command=Pusk_with_coeff
                      )
GraphButton2 = Button(canvas2, text="2", bg="thistle3", fg='DeepSkyBlue4',
                      activebackground="Light Yellow2",
                      width=3, height=1, command=Pusk2
                      )
GraphButton3 = Button(canvas2, text="3", bg="khaki", fg='DeepSkyBlue4',
                      activebackground="Light Yellow2",
                      width=3, height=1, command=Pusk3
                      )
GraphButton4 = Button(canvas2, text="4", bg="SeaGreen2", fg='DeepSkyBlue4',
                      activebackground="Light Yellow2",
                      width=3, height=1, command=Pusk4
                      )
GraphButton5 = Button(canvas2, text="5", bg="snow", fg='DeepSkyBlue4',
                      activebackground="Light Yellow2",
                      width=3, height=1, command=Pusk5
                      )
GraphLabel = Label(canvas2, text="Выбор графика", bg="white",
                      width=14, height=1
                      )

PuskButton_window = canvas2.create_window(20, 10, anchor=NW, window=GraphButton1)
PuskButton_window = canvas2.create_window(60, 10, anchor=NW, window=GraphButton2)
PuskButton_window = canvas2.create_window(100, 10, anchor=NW, window=GraphButton3)
PuskButton_window = canvas2.create_window(140, 10, anchor=NW, window=GraphButton4)
PuskButton_window = canvas2.create_window(180, 10, anchor=NW, window=GraphButton5)
PuskButton_window = canvas2.create_window(220, 11, anchor=NW, window=GraphLabel)

root.mainloop()


