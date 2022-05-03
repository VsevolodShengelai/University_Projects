package com.company;

public class MainBruteforce {
    public static void main(String[] args) {

        String[] items = new String[] {"Книга", "Батарейки", "Котелок", "Палатка", "Печенье"};
        int[] weights = {3, 4, 5, 8, 9};
        int[] prices = {10, 60, 40, 70, 60};

        /*
        String[] items = new String[] {"Книга", "Батарейки", "Котелок"};
        int[] weights = {8, 4, 5};
        int[] prices = {1, 6, 4};
         */

        int maxWeight = 12;

        //Посчитаем количество комбинаций
        int count = (int) Math.pow(2, weights.length);

        int maxPrice = 0;
        long maxState = 0;

        for (long state = 0; state < count; state++) {
            System.out.println(state);
            int price = statePrice(state, prices);
            int weight = stateWeight(state, weights);
            if (weight <= maxWeight) {
                if (maxPrice < price) {
                    maxPrice = price;
                    maxState = state;
                }
            }
        }

        System.out.println("Оптимальное содержимое рюкзака:");
        long poverOfTwo = 1;
        for (int i = 0; i < weights.length; i++) {
            if ((poverOfTwo & maxState) > 0) {
                //System.out.println(i + 1);
                System.out.println("No"+ (i + 1) + " " + items[i] + "  Цена " + prices[i] + "  Вес " + weights[i]);

            }
            poverOfTwo <<= 1;
        }
    }

    //Вес набора предметов
    private static int stateWeight(long state, int[] weights) {
        long poverOfTwo = 1;
        int weight = 0;
        for (int i = 0; i < weights.length; i++) {
            if ((poverOfTwo & state) != 0) {
                weight += weights[i];
            }
            poverOfTwo <<= 1;
        }
        return weight;
    }

    //Цена набора предметов
    private static int statePrice(long state, int[] prices) {
        long poverOfTwo = 1;
        int price = 0;
        for (int i = 0; i < prices.length; i++) {
            if ((poverOfTwo & state) != 0) {
                price += prices[i];
            }
            poverOfTwo <<= 1;
        }
        return price;
    }
}
