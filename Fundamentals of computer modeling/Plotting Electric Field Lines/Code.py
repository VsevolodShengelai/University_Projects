from tkinter import *
from tkinter import ttk
from PIL import Image, ImageTk
import About_program
import Program1



root = Tk()
root.title("")
root.geometry('850x600')
root.resizable(width=False, height=False)



after_id = ''
temp = 0

def tick():
    global temp, after_id
    after_id =  root.after(60, tick)

    if temp > 150:
        temp = 0

    if temp < 10:
        number = '00' + str(temp)
    elif temp < 100:
        number = '0' + str(temp)
    else:
        number = str(temp)

        
    str_file = "Video/Video_" + number + ".jpg"

    img = ImageTk.PhotoImage(file=str_file)
    fone_label.configure(image = img)
    fone_label.image = img

    button1.configure(image = butt1IMG)
    button2.configure(image = butt2IMG)

    #print(1)

    temp += 1

#Функции главного меню
    
def exit_app():
    root.destroy()

def stop_animation():
    root.after_cancel(after_id)
    global stop_animation
    stop_animation = True

def continue_animation():
    global stop_animation
    if stop_animation:
        tick()
        stop_animation = False

#Функция запуска первой программы
back = ImageTk.PhotoImage(file='damaged_metal_orig.2.png')
interface = ImageTk.PhotoImage(file='imgonline-com-ua-Resize-RbmtiTZkMPDM.png')
def launch():
    Program1.init(root, back, interface)
    
    
#Сделаем главное меню программы

main_menu = Menu(root, borderwidth=0)
root.configure(menu=main_menu)


first_item = Menu(main_menu, tearoff=0)
main_menu.add_cascade(label="File", menu=first_item)
'''first_item.add_command(label="New")'''
first_item.add_command(label="Stop Animation", command=stop_animation)
first_item.add_command(label="Continue Animation", command=continue_animation)
first_item.add_separator()
first_item.add_command(label="Exit", command=exit_app)
 

second_item = Menu(main_menu, tearoff=0)
main_menu.add_cascade(label="Help", menu=second_item)
second_item.add_command(label="About Program", command = About_program.start_window)
second_item.add_separator()
second_item.add_command(label="Documentation")

separator = ttk.Separator(root, orient='horizontal') 
separator.pack(fill=X)

#Основная часть окон

mainFrame = Frame(root, width = 850, height=600)
mainFrame.place(x=0,y=0)
mainFrame.pack_propagate(False)
fone_label = Label(mainFrame, bg = "red")
fone_label.pack(fill = BOTH,  expand=1)

butt1IMG = ImageTk.PhotoImage(file='dva_zaryada.png')
butt2IMG = ImageTk.PhotoImage(file='zar_palochka.png')

#Кнопки-программы 
#fone_label = Label(fone_label, bg = 'azure2')
#fone_label.grid(row=0,column=0, padx = 6)
button1 = Button(fone_label, image = butt1IMG,
                             relief=FLAT, bg = 'azure2', command = launch)
button1.grid(row=0,column=0, padx = 10, pady = 10)
button2 = Button(fone_label, image = butt1IMG,
                             relief=FLAT, bg = 'azure2')
button2.grid(row=0,column=1, padx = 10, pady = 10)


tick()
root.mainloop()
