from tkinter import *
from tkinter import scrolledtext 

def start_window_license():

    top1 = Toplevel()
    top1.geometry('600x380')
    top1.resizable(width=False, height=False)
    top1.title("License")

    text = scrolledtext.ScrolledText(top1, width=30, height=9,
        wrap=WORD, font='TimesNewRoman 10',
        highlightthickness = 0, highlightcolor = 'snow3')

    text.pack(expand=True, fill='both')
    text.insert(END, '''Данное ПО распространяется по Open-Sourse - Лицензии.
Лицензия предоставляет получателям компьютерных программ следующие права:

- свободу запуска программы с любой целью;
- свободу изучения того, как программа работает, и её модификации (предварительным условием для этого является доступ к исходному коду);
- свободу распространения копий как исходного, так и исполняемого кода;
- свободу улучшения программы и выпуска улучшений в публичный доступ (предварительным условием для этого является доступ к исходному коду).

''')
	
    text.configure(state=DISABLED)


    ##Вставлять код до этой строки

    top1.mainloop() 
