package com.company;

import java.lang.reflect.Array;
import java.util.Arrays;
import java.util.Comparator;

public class MainGreedy {
    public static void main(String[] args) {
        final Item item1 = new Item(3,10,"Книга");
        final Item item2 = new Item(4,60,"Батарейки");
        final Item item3 = new Item(5,40,"Котелок");
        final Item item4 = new Item(8,70,"Палатка");
        final Item item5 = new Item(9,60,"Печенье");

        final Item[] items = {item1, item2, item3, item4, item5};

        //Сначала мы должны отсортировать предметы по их удельному весу
        //Получаем удельную ценность каждого предмета и сортируем их в обратном порядке - от большего к меньшему
        //Сортировка O(N * log(N))
        //Используется алгоритм быстрой сортировки (сортировака Хоара)
        Arrays.sort(items, Comparator.comparingDouble(Item::valuePerUnitOfWeight).reversed());

        System.out.println(Arrays.toString(items));
        System.out.println();

        final int W = 12;

        int weightSoFar = 0; //аакумулируем текущий вес
        double valueSoFar = 0;
        int currentItem = 0;

        while (currentItem < items.length && weightSoFar!=W){
            if (weightSoFar + items[currentItem].getWeight() < W){
                //берём объект целиком
                valueSoFar += items[currentItem].getValue();
                weightSoFar += items[currentItem].getWeight();
                System.out.println(items[currentItem]);
            }
            currentItem++;
        }

        System.out.println("Ценность наилучшего набора предметов: " + valueSoFar);
        System.out.println("Вес наилучшего набора предметов: " + weightSoFar);

    }
}

class Item {
    private int weight;
    private int value;
    private  String name;

    public Item(int weight, int value, String name) {
        this.weight = weight;
        this.value = value;
        this.name = name;
    }

    public int getWeight() {
        return weight;
    }

    public int getValue() {
        return value;
    }

    public String getName() {
        return name;
    }

    public double valuePerUnitOfWeight () {
        return value / (double) weight;
    }

    public String toString(){
        return "{"+ name + ", Вес:" + weight + ", Стоимость:" + value + "}";
    }
}