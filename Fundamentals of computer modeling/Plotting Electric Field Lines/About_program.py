from tkinter import * 
from PIL import ImageTk
from PIL import Image
from tkinter import ttk
import License
import News


def start_window():

    def exit_app():
        top.destroy()

    #Разделение окна на Fram`ы
    
    top = Toplevel()
    top.geometry('280x440')
    top.resizable(width=False, height=False)
    top.title("About")

    TopFrame = Frame(top, bg = 'snow3',	highlightthickness = 5,
                     highlightcolor = 'SystemButtonFace')
    TopFrame.pack(fill = BOTH, expand=1)

    
    
    ###Top_label = Label(TopFrame, bg = 'red')
    ###Top_label.pack(fill = BOTH,  expand=1)
    
    BottomFrame = Frame(top, height = 60)
    BottomFrame.pack(expand=0)
    
    #BottomFrame.pack_propagate(False)
    
    closeButton = Button(BottomFrame, text ="Close", width = 6,
                         command = exit_app)
    closeButton.grid(padx = 7, pady = 7)

    #Размещение виджетов в рамках

    ## Логотип
    logo_img = Image.open('Electricity.png')

    maxsize = (58, 58)
    logo_img.thumbnail(maxsize, Image.ANTIALIAS)

    logo_img = ImageTk.PhotoImage(logo_img)
    
    logo_label = Label(TopFrame, image = logo_img, bg = 'snow3')
    logo_label.place(x = 15, y = 20)

    ## Текстовые поля и всё остальное
    
    logo_text = Label(TopFrame, width=14, text = "Электричество\nи магнетизм",
                      font='Arial 15', bg = 'snow3', justify = LEFT)
    logo_text.place(x = 73, y = 22)
    

    about_text = Label(TopFrame, text = "Набор программ визуазизации \nэлектрических явлений",
                      font='Arial 10', justify=LEFT, bg = 'snow3')
    about_text.place(x = 15, y = 80)


    text = Text(TopFrame, width=30, height=9,
            wrap=WORD,
            highlightthickness = 0, highlightcolor = 'snow3'
                )


    text.place(x = 15, y = 140)
    text.insert(END, '''Разработчик: Шенгелай В.М.
email: famy@mail.ua

Руководитель: Милюков В.В. 
email: test@mail.ru

Организация: КФУ им. В.И.Вернадского''')

    text.configure(state=DISABLED)

    separator = ttk.Separator(TopFrame, orient='horizontal') 
    separator.place(x = 15, y =315, width = 246)

    version_label = Label(TopFrame, text = "versoin: 0.1.1", font='Arial 10')
    version_label.place(x = 15, y =320)

    ## Лицензия и новости

    license_= Button(TopFrame, text = "License", width = 10, height = 1,
                     command = License.start_window_license)
    license_.place(x = 50, y =360)

    news_= Button(TopFrame, text = "News", width = 10, height = 1,
                  command = News.start_window_news)
    news_.place(x = 150, y =360)
    
     
    top.mainloop()  

