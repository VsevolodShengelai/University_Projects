from tkinter import *
from tkinter import scrolledtext 

def start_window_news():

    top2 = Toplevel()
    top2.geometry('600x380')
    top2.resizable(width=False, height=False)
    top2.title("License")

    text = scrolledtext.ScrolledText(top2, width=30, height=9,
        wrap=WORD, font='TimesNewRoman 10',
        highlightthickness = 0, highlightcolor = 'snow3')

    text.pack(expand=True, fill='both')
    text.insert(END, '''Что нового в версии 0.1.2
Вышла 20.06.2021
========================================
* Добавлена Программа1

Что нового в версии 0.1.1
Вышла 12.03.2021
========================================
* Доработан лаунчер, исправлены баги с анимацией

''')
	
    text.configure(state=DISABLED)


    ##Вставлять код до этой строки

    top2.mainloop() 
