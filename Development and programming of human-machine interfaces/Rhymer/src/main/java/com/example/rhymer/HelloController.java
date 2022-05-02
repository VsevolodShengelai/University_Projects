package com.example.rhymer;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.Label;
import javafx.scene.control.TextArea;

import java.io.*;
import java.util.Random;

public class HelloController {
    @FXML
    TextArea inputText;
    @FXML
    TextArea outputText;

    boolean first_time = true;

    int counter;

    String[] word_array;

    public void takeARhym(ActionEvent actionEvent) {
        if (first_time) {
            //Прочитаем файл, чтобы подсчитать количество элементов для массива слов
            try {
                File file = new File("./src/main/resources/full_vocabular.txt");
                //создаем объект FileReader для объекта File
                FileReader fr = new FileReader(file);
                //создаем BufferedReader с существующего FileReader для построчного считывания
                BufferedReader reader = new BufferedReader(fr);
                // считаем сначала первую строку
                String line = reader.readLine();
                counter = 1;
                while (line != null) {
                    //System.out.println(line);
                    // считываем остальные строки в цикле
                    line = reader.readLine();
                    counter++;
                }
                System.out.println(counter);
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }

            word_array = new String[counter];

            //Загрузка словаря в массив строк
            try {
                File file = new File("./src/main/resources/full_vocabular.txt");
                //создаем объект FileReader для объекта File
                FileReader fr = new FileReader(file);
                //создаем BufferedReader с существующего FileReader для построчного считывания
                BufferedReader reader = new BufferedReader(fr);
                // считаем сначала первую строку
                String line = reader.readLine();
                int current_world = 0;
                word_array[0] = line;
                while (line != null) {
                    current_world++;
                    //System.out.println(line);
                    // считываем остальные строки в цикле
                    line = reader.readLine();
                    word_array[current_world] = line;
                }
                System.out.println(current_world);
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            first_time = false;
        }

        word_array[word_array.length-1] = "последнееслово";


        String text = inputText.getText();

        String answer = "";

        String[] strings = text.split("\n");
        for (int i = 0; i < strings.length; i++) {
            //System.out.println(strings[i]);
            String[] parts = strings[i].split(" ");
            String lastWord = parts[parts.length - 1];
            System.out.println(lastWord);

            String endS = lastWord.substring(lastWord.length() - 1);
            String writenEndSymbol;

            if(endS.equals(";") || endS.equals(",") || endS.equals("-")){
                lastWord = lastWord.replace(endS,"");
            }

            if (lastWord.length() >= 3) { //Если к слову вообще возможно подобрать рифму

                //Подсчитаем количество совпадающих по последним трём буквам слов
                int word_counter = 0;
                for (int j = 0; j < word_array.length; j++) {
                    //System.out.println(word_array[j]);
                        if (word_array[j].length() >= 3) {
                            String substring = lastWord.substring(lastWord.length() - 3);
                            String substring1 = word_array[j].substring(word_array[j].length() - 3);
                            if (substring1.equals(substring)) {
                                word_counter += 1;
                            }
                        }
                }
                System.out.println(word_counter);

                String[] candidates = new String[word_counter];

                //Заполняем массив словами
                int candidateCounter = 0;
                for (int j = 0; j < word_array.length; j++) {


                    if (word_array[j].length() >= 3) {
                        String substring = lastWord.substring(lastWord.length() - 3);
                        String substring1 = word_array[j].substring(word_array[j].length() - 3);
                        if (substring1.equals(substring)) {
                            candidates[candidateCounter] = word_array[j];
                            candidateCounter++;
                        }
                    }

                }

                if (candidates.length > 0){
                    //Выбираем слуйчайную рифму из массива
                    Random random = new Random();
                    int num = random.nextInt(candidates.length);
                    String choosenWord = candidates[num];
                    System.out.println(choosenWord);

                /*for (int j = 0; j < candidates.length; j++) {
                    System.out.println(candidates[j]);}*/
                    strings[i] = strings[i].replace(lastWord,choosenWord);
                }
            }
        }
        for(int k = 0; k < strings.length; k++){
            answer = answer+strings[k]+"\n";
        }
        outputText.setText(answer);
    }
}

