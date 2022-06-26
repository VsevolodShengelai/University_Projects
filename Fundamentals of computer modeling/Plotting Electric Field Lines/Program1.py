from tkinter import *
from matplotlib.backends.backend_tkagg import (
    FigureCanvasTkAgg, NavigationToolbar2Tk)
# Implement the default Matplotlib key bindings.
from matplotlib.backend_bases import key_press_handler
from matplotlib.figure import Figure
import matplotlib.pyplot as plt
import numpy as np
import math
import matplotlib.pyplot as plt
from tkinter import ttk



def init(root, back, interface): 
    def destroy():
        ProgramFrame.destroy()
        
    
    ProgramFrame = Frame(root, width = 850, height=600)
    ProgramFrame.place(x=0,y=0)

    #Создание полотна для пульта управления
    canvas = Canvas(ProgramFrame, width = 250, height = 600,
                bg = 'blue', bd = 0,
                borderwidth = 0,
                highlightthickness = 0) 
    canvas.grid(row=0, column=0, pady=0)
    canvas.create_image(0, 0, image = back, anchor = NW)

    ExitButton = Button(text = 'exit', bg = 'brown1', height = 1, width = 16,
                        command = destroy)
    ExitButton_win = canvas.create_window(10, 6, anchor=NW, window=ExitButton)

    #Это Canvas для будущего графика
    canvas2 = Canvas(ProgramFrame, width = 600, height = 600,
                bg = 'blue', bd = 0,
                borderwidth = 0,
                highlightthickness = 0) 
    canvas2.grid(row=0, column=1, pady=0)
    canvas2.create_image(0, 0, image = interface, anchor = NW)




    
    #Текстовые таблички
    CoordlText = Label(canvas,text="Расст. от центра\n до зарядов", width = 16)
    QposText = Label(canvas,text="Величина зар. +, Кл", width = 16)
    QnegText = Label(canvas,text="Величина зар. -, Кл", width = 16)
    ShagText = Label(canvas,text="Шаг dS", width = 16)
    LinesNumText = Label(canvas,text="Количество линий", width = 16)
    RadiusOfDeathZone = Label(canvas,text="Радиус дальней зоны", width = 16)
 
  
    CoordlText. place(x=10,y=60)
    QposText. place(x=10,y=136)
    QnegText. place(x=10,y=196)
    ShagText. place(x=10,y=256)
    LinesNumText. place(x=10,y=316)
    RadiusOfDeathZone. place(x=10,y=376)


    ##Spinboxes
    coord_deaf = StringVar(root)
    coord_deaf.set("8")
    coord = ttk.Spinbox(canvas, from_=0, to=30, width=17, state='readonly',
                    textvariable=coord_deaf)
    coord.place(x=10,y=100)

    Qpos_deaf = StringVar(root)
    Qpos_deaf.set("1")
    Qpos = ttk.Spinbox(canvas, from_=0.0001, to=1000, width=17,
                   textvariable=Qpos_deaf)
    Qpos.place(x=10,y=160)

    Qneg_deaf = StringVar(root)
    Qneg_deaf.set("-1")
    Qneg = ttk.Spinbox(canvas, from_=--0.0001, to=-1000, width=17,
                   textvariable=Qneg_deaf)
    Qneg.place(x=10,y=220)

    Shag_deaf = StringVar(root)
    Shag_deaf.set("0.01")
    Shag = ttk.Spinbox(canvas, from_=0.001, to=1, width=17, increment = 0.01,
                   textvariable=Shag_deaf)
    Shag.place(x=10,y=280)
    
    LinesNum_deaf = StringVar(root)
    LinesNum_deaf.set("8")
    LinesNum = ttk.Spinbox(canvas, from_=0, to=200, width=17,
                       textvariable=LinesNum_deaf)
    LinesNum.place(x=10,y=340)

    DeathRadius = StringVar(root)
    DeathRadius.set("10000")
    DeathRadius = ttk.Spinbox(canvas, from_=0, to=1000000, width=17,
                       textvariable=DeathRadius)
    DeathRadius.place(x=10,y=400)


    def reset_val():

        coord.set(8)
        Qpos.set(1)
        Qneg.set(-1)
        Shag.set(0.1)
        LinesNum.set(8)
        DeathRadius.set(10000)
        
    ##Значения по умолчанию кнопка
    Reset_but = Button(canvas, text="Сброс знач.", bg = "Light Yellow3",                    	
                    activebackground = "Light Yellow2",
                    width = 15, height = 1, command = reset_val)
    Reset_but_window = canvas.create_window(10, 430, anchor=NW, window=Reset_but)
 
 
    #Информационная панель

    InfoPanel = Text(canvas, bg = "gray25", bd=2,
                 width = 26, height =3, fg="green2")
    InfoPanel_window = canvas.create_window(9, 460, anchor=NW, window=InfoPanel)

    #Алгоритм запуска 
    def pusk():
        coord_v = coord.get()
        Qpos_v = Qpos.get()
        Qneg_v = Qneg.get()
        Shag_v = Shag.get()
        LinesNum_v = LinesNum.get()
        DeathRadius_v = DeathRadius.get()

        if coord_v=='' or not(float(coord_v) >=1 and float(coord_v) <= 1000):
            InfoPanel.insert( END, ">Ошибка в выборе расстояния"+"\n")
            InfoPanel.see("end")
            return
        if Qpos_v=='' or not(float(Qpos_v) >=0 and float(Qpos_v) <= 10):
            InfoPanel.insert( END, ">Ошибка в выборе заряда +q"+"\n")
            InfoPanel.see("end")
            return
        if Qneg_v=='' or not(float(Qneg_v) >=-10 and float(Qneg_v) <= 0):
            InfoPanel.insert( END, ">Ошибка в выборе заряда -q"+"\n")
            InfoPanel.see("end")
            return
        if Shag_v=='' or not(float(Shag_v) >0 and float(Shag_v) <= 2):
            InfoPanel.insert( END, ">Ошибка в выборе шага приращения"+"\n")
            InfoPanel.see("end")
            return
        if LinesNum_v=='' or not(int(LinesNum_v)%2 == 0 and int(LinesNum_v) >=0 and int(LinesNum_v) <=300):
            InfoPanel.insert( END, ">Ошибка в выборе числа линий"+"\n")
            InfoPanel.see("end")
            return
        if DeathRadius_v =='' or int(DeathRadius_v) <=0:
            InfoPanel.insert( END, ">Ошибка в выборе радиуса дальней зоны"+"\n")
            InfoPanel.see("end")
            return

        InfoPanel.insert( END, ">Пуск!"+"\n")
        InfoPanel.see("end")

        ## Константы
        k = 9*(10**9)
        dS = float(Shag_v)

        ## Инициализация переменных
        axis_limit = 0.3
        np_vector_array = np.array([[float(coord_v),0], [-float(coord_v), 0]])


        pos_q = float(Qpos_v)
        neg_q = float(Qneg_v)

        charges = [pos_q, neg_q]

        n = int(LinesNum_v)

        ## Получаем точки окружности и корректируем их
        def get_points(radius, number_of_points, correct):
            radians_between_each_point = 2*np.pi/number_of_points
            list_of_points = [[],[]]
            for p in range(0, number_of_points):
                list_of_points[0].append(radius*np.cos(p*radians_between_each_point))
                list_of_points[1].append(radius*np.sin(p*radians_between_each_point))
            for i in range(len(list_of_points[0])):
                list_of_points[0][i] += correct[0]
            for i in range(len(list_of_points[1])):
               list_of_points[1][i] += correct[1]
    
    
            return list_of_points

        ## Алгоритм расчёта вектора напряжённости в заданной точке
        def calc_vect_E(charges, np_r, np_vector_array):
            #print(np_r)
            Sum = 0
            for i in range(0,len(charges)):
                newVect  = np_r - np_vector_array[i]
                #print("newVect: ", newVect)
                vectModule = np.linalg.norm(newVect)**3
                #print("vectModule: ", vectModule)
                Ei = charges[i]*newVect/vectModule
                #print("Ei: ", Ei)
                Sum+= Ei
                #print(Sum)
        
            Sum *= k
            return Sum

        global_array = [[], []]

        #Функция отрисовки одной линии
        def draw_a_line():

            control_mas = []
    
            x_glob = len(global_array)-2
            y_glob = len(global_array)-1

            global_array.append([])
            global_array.append([])

            iter_ = 0
            while True:
                iter_ = iter_+1
                x1 = np_vector_array[1][0]
                y1 = np_vector_array[1][1]

                if ((np_r[0]-x1)**2 + (np_r[1]-y1)**2)**0.5 <=1.4:
                    break
        
                E = calc_vect_E(charges, np_r, np_vector_array)
                #print('E0= ',E[0],'E1= ',E[1])
                Ex = E[0]
                Ey = E[1]
                dx = dS*Ex/np.linalg.norm(E)
                dy = dS*Ey/np.linalg.norm(E)
                #print('dx = ',dx,'|','dy = ',dy,'|','norm_E= ',np.linalg.norm(E))

                np_r[0]+=dx
                np_r[1]+=dy
                #print('np_r[0] = ',np_r[0],'|','np_r[1] = ',np_r[1],'|','norm_E= ',np.linalg.norm(E))
        
                global_array[x_glob].append(np_r[0])
                global_array[y_glob].append(np_r[1])

                control_mas.append(np_r[1])


                #print(DeathRadius_v, type(DeathRadius_v))
                #print(np_r[0])
                if int(np_r[0]) > int(DeathRadius_v) or int(np_r[0]) > int(DeathRadius_v):
                    
                    #Достигли радиуса дальней зоны
                    break

        points = get_points(1, n, correct = np_vector_array[0])
        for i in range(len(points[0])):
            #В алгоритме расчёта вектора напряжённости возникала ошибка, если координата
            #точки лежала на прямой, соединяющей точки-заряды
            np_r = np.array([round(points[0][i],4)+0.00001,round(points[1][i],4)+0.00001],dtype=np.float64)
            print(np_r)
            draw_a_line()




        #np_r = np.array([9,0.0001],dtype=np.float64)


        print(points)

        print(global_array)

        circle1 = plt.Circle(np_vector_array [0], 0.9, color='r', fill=True)
        plt.gca().add_patch(circle1)
        circle2 = plt.Circle(np_vector_array [1], 0.9, color='b', fill=True)
        plt.gca().add_patch(circle2)

        plot = plt.plot(*global_array, color='blue')

        plus_limit = np_vector_array[0][0]+axis_limit*np_vector_array[0][0]
        minus_limit = -np_vector_array[0][0]-axis_limit*np_vector_array[0][0]

        plt.ylim((minus_limit,plus_limit))
        plt.xlim((minus_limit,plus_limit))
        plt.show()
        

    #Кнопка Пуск
    PuskButton = Button(canvas, text="Пуск!", bg = "lime green",                    	
                    activebackground = "Light Yellow2",
                    width = 14, height = 2, command = pusk)
    PuskButton_window = canvas.create_window(64, 520, anchor=NW, window=PuskButton)



'''
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
    
    '''
